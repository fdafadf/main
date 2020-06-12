using AI.NeuralNetworks;
using Labs.Agents.Forms;
using System;
using System.Linq;
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
            labForm.Spaces.AddContextMenuItem("&Train Neural Agent", TrainOnSpace);
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
                var networkFile = Workspace.Instance.GetNetworkFile(pluginFactory.Network);
                var network = new AgentNetwork(networkFile);
                network.InitializeLayers(0);
                network.Save(networkFile);
            }
        }

        private void TrainOnSpace(ListViewItem item)
        {
            if (item.Tag is ISpaceTemplateFactory spaceFactory)
            {
                using (var gridForm = new PropertyGridForm("Training"))
                {
                    var trainerSettings = new NeuralAgentTrainerConfiguration();
                    trainerSettings.Space = spaceFactory.Name;
                    gridForm.PropertyGrid.SelectedObject = trainerSettings;

                    if (gridForm.ShowDialog() == DialogResult.OK)
                    {
                        var trainer = new NeuralAgentTrainer(trainerSettings);
                        trainer.Train();
                        trainer.Simulate();
                        trainer.SaveAsTemplate();
                    }
                }
            }
        }

        private void Train(ListViewItem item)
        {
            if (item.Tag is SimulationTemplate simulationTemplate)
            {
                if (simulationTemplate.Definition.GetSimulationPluginFactory() is NeuralSimulationPluginFactory pluginFactory)
                {
                    //var trainer = new NeuralAgentTrainer(simulationTemplate.Definition.Model, simulationTemplate.SpaceTemplateFactory, pluginFactory);
                    //trainer.Train();
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
