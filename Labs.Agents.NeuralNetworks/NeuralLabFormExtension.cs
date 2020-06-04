using AI.NeuralNetworks;
using Labs.Agents.Forms;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralLabFormExtension : LabFormExtension<NeuralSimulationPluginFactory>
    {
        public NeuralLabFormExtension(ILabForm labForm, Workspace workspace) : base(labForm, workspace)
        {
            AddNewAgentMenuItem("&Neural Agent", menuItemNewAgentOnClick);
            labForm.Agents.AddContextMenuItem("&Randomise Agent Network", RandomiseAgentNetwork, IsNeuralSimulationPlugin);
            labForm.Simulations.AddContextMenuItem("&Train", Train, IsNeuralSimulationTemplate);

            //ToolStripMenuItem menuItemNewNetwork = new ToolStripMenuItem();
            //menuItemNewNetwork.Name = "menuItemNewNetwork";
            //menuItemNewNetwork.Size = new System.Drawing.Size(98, 22);
            //menuItemNewNetwork.Text = "&New";
            //menuItemNewNetwork.Click += new System.EventHandler(this.menuItemNewNetwork_Click);
            //
            //ToolStripMenuItem networkToolStripMenuItem = new ToolStripMenuItem();
            //networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //menuItemNewNetwork});
            //networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            //networkToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            //networkToolStripMenuItem.Text = "&Neural Network";
            //LabForm.MenuAgentDrivers.Items.Add(networkToolStripMenuItem);
        }

        private void RandomiseAgentNetwork(ListViewItem item)
        {
            if (item.Tag is NeuralSimulationPluginFactory pluginFactory)
            {
                Workspace.Instance.GetNetworkFile(pluginFactory.Network).Random(0);
            }
        }

        private void Train(ListViewItem item)
        {
            if (item.Tag is SimulationTemplate simulationTemplate)
            {
                if (simulationTemplate.Definition.GetSimulationPluginFactory() is NeuralSimulationPluginFactory pluginFactory)
                {
                    //var seed = 0;
                    //var space = Workspace.Instance.Spaces.GetByName("Empty_Small.png");
                    //var temporaryFile = new FileInfo("Test");
                    //var destructionModel = new AgentDestructionModel() { RemoveDestoryed = true };
                    //var networkDefinition = new AgentNetworkDefinition("Train", 1, 18, 9, 1);
                    //var network = new AgentNetwork(networkDefinition, new He(seed));
                    //network.Save(temporaryFile);
                    //var networkFile = new AgentNetworkFile(temporaryFile);
                    //var trainingSettings = new AgentNetworkTrainingConfiguration() { LearningRate = 0.001, Momentum = 0.04 };
                    //var trainingPlugin = new NeuralSimulationPlugin(networkFile, trainingSettings, seed);
                    //var trainingIterationLimit = 50000;
                    //var trainingSimulation = new SimulationModel1<NeuralSimulationPlugin, NeuralAgent>(space, trainingPlugin, "Neural", trainingIterationLimit, AgentsCollisionModel.Destroy, destructionModel, seed);
                    //var testingIterationLimit = 1000;
                    //var testingReachedGoalsBest = 10;

                    var trainingPlugin = pluginFactory.CreateTrainingPlugin();
                    var trainingModelConfiguration = simulationTemplate.Definition.Model.Clone();
                    trainingModelConfiguration.IterationLimit = pluginFactory.TrainingConfiguration.IterationLimit;
                    var trainingSimulation = SimulationTemplate.CreateSimulation(trainingPlugin, "Training", simulationTemplate.SpaceTemplateFactory, trainingModelConfiguration);
                    
                    if (pluginFactory.TrainingAnalyticsConfiguration.Enabled)
                    {
                        var testingFile = new FileInfo("Testing.model");
                        var testingBestFile = new FileInfo("Testing-Best.model");
                        trainingPlugin.Network.Save(testingFile);
                        var testingNetworkFile = new AgentNetworkFile(testingFile);
                        var testingBestReward = 0.0;
                        var testingBestReachedGoals = 0;
                        var testingModelConfiguration = simulationTemplate.Definition.Model.Clone();
                        testingModelConfiguration.IterationLimit = pluginFactory.TrainingAnalyticsConfiguration.IterationLimit;
                        var chartForm = new ChartForm("Neural Agent Reinforcement Learning");
                        chartForm.Show();

                        Task.Run(() =>
                        {
                            while (trainingSimulation.Iterate())
                            {
                                Console.Title = trainingSimulation.ToString();

                                if (trainingSimulation.Iteration % pluginFactory.TrainingAnalyticsConfiguration.Interval == 0)
                                {
                                    trainingPlugin.Network.Save(testingFile);
                                    var testingSeed = Guid.NewGuid().GetHashCode();
                                    var testingPlugin = new NeuralSimulationPlugin(testingNetworkFile, null, testingSeed);
                                    var testingSimulation = SimulationTemplate.CreateSimulation(testingPlugin, "Testing", simulationTemplate.SpaceTemplateFactory, testingModelConfiguration);
                                    testingSimulation.Initialise();
                                    while (testingSimulation.Iterate()) ;

                                    foreach (var serie in testingSimulation.Results.Series)
                                    {
                                        chartForm.InvokeAction(() => chartForm.AddPoint(serie.Key, serie.Value.Last()));
                                    }

                                    var predictions = string.Join(", ", testingPlugin.Predictions.Select(p => $"{p,4}"));
                                    Console.WriteLine($"{testingSimulation.Results} Predictions: {predictions}");

                                    if (testingPlugin.TotalReward > testingBestReward)
                                    {
                                        testingBestReward = testingPlugin.TotalReward;
                                        testingBestReachedGoals = testingSimulation.TotalReachedGoals;
                                        trainingPlugin.Network.Save(testingBestFile.Ensure());
                                        Console.WriteLine("Saved as best");
                                    }
                                }
                            }

                            string message = $"Do you want to update network '{pluginFactory.Network}'?\r\n\r\nTotal Reached Goals: {testingBestReachedGoals}\r\nTotal Reward: {testingBestReward}";

                            if (MessageBox.Show(message, "Training Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                trainingPlugin.NetworkFile.Save(new AgentNetwork(testingBestFile));
                            }
                        });
                    }
                }
            }
        }

        private bool IsNeuralSimulationPlugin(ListViewItem item)
        {
            return item.Tag is NeuralSimulationPluginFactory;
        }

        private bool IsNeuralSimulationTemplate(ListViewItem item)
        {
            return (item.Tag as SimulationTemplate)?.Definition.GetSimulationPluginFactory() is NeuralSimulationPluginFactory;
        }

        private void menuItemNewNetwork_Click(object sender, EventArgs e)
        {
            ShowNewNetworkDialog();
        }

        private void menuItemNewAgentOnClick(object sender, EventArgs e)
        {
            using (var form = new PropertyGridForm("New Agent"))
            {
                var pluginFactory = new NeuralSimulationPluginFactory(string.Empty, string.Empty, 0);
                form.PropertyGrid.GetToolStrip().AddButton("New Network", ShowNewNetworkDialog);
                form.PropertyGrid.SelectedObject = pluginFactory;
                form.FormClosing += (s, closingEvent) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            Assert.NotNullOrWhiteSpace(pluginFactory.Name, "Property Name is required.");
                            Assert.NotNullOrWhiteSpace(pluginFactory.Network, "Property Network is required.");
                            Assert.Unique(pluginFactory.Name, Workspace.Instance.SimulationPlugins.Select(driver => driver.Name), "Name already exists.");
                            Workspace.Instance.SimulationPlugins.Add(pluginFactory);
                            Add(pluginFactory);
                        }
                        catch (Exception exception)
                        {
                            closingEvent.Cancel = true;
                            MessageBox.Show(exception.Message, "New Neural Agent", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                form.ShowDialog();
            }
        }

        private void ShowNewNetworkDialog()
        {
            using (var form = new PropertyGridForm("New Network"))
            {
                var networkDefinition = new AgentNetworkDefinition(null, 2, 26, 12);
                form.PropertyGrid.SelectedObject = networkDefinition;
                form.FormClosing += (s, closingEvent) =>
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        try
                        {
                            var network = new AgentNetwork(networkDefinition, new He(0));
                            Workspace.Instance.Add(networkDefinition.Name, network);
                        }
                        catch (Exception exception)
                        {
                            closingEvent.Cancel = true;
                            MessageBox.Show(exception.Message, "New Network", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                };
                form.ShowDialog();
            }
        }

        protected override void Add(NeuralSimulationPluginFactory pluginFactory)
        {
            var item = new ListViewItem(pluginFactory.Name);
            item.SubItems.Add("Neural");
            item.SubItems.Add(pluginFactory.ToString());
            item.Tag = pluginFactory;
            LabForm.Agents.AddWithAutoResize(item);
        }
    }
}
