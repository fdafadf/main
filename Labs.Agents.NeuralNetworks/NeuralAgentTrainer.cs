using AI.NeuralNetworks;
using Labs.Agents.Forms;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentTrainerConfiguration
    {
        public int ViewRadius { get; set; } = 3;
        public int[] LayersSizes { get; set; } = new int[] { 84, 42, 21, 7, 1 };
        public int TrainingIterations { get; set; } = 50000;
        public int TrainingSeed { get; set; } = 0;
        public int TestingIterations { get; set; } = 1000;
        public int TestingInterval { get; set; } = 100;
        public int NetworkSeed { get; set; } = 0;
        public string Space { get; set; }
    }

    public class NeuralAgentTrainer
    {
        SimulationModelConfiguration SimulationModel;
        ISpaceTemplateFactory SpaceTemplateFactory;
        NeuralSimulationPluginFactory PluginFactory;
        string NetworkName;
        int lines;

        public NeuralAgentTrainer(NeuralAgentTrainerConfiguration configuration)
        {
            NetworkName = $"Auto-R{configuration.ViewRadius}-{string.Join("_", configuration.LayersSizes)}";
            var networkFile = Workspace.Instance.GetNetworkFile(NetworkName);
            var createNetwork = true;

            if (networkFile.Exists)
            {
                var overwrite = MessageBox.Show($"Do you want to reset existing network '{NetworkName}'?", "Training", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                createNetwork = overwrite == DialogResult.Yes;
            }

            if (createNetwork)
            {
                var networkDefinition = new AgentNetworkDefinition(NetworkName, configuration.ViewRadius, configuration.LayersSizes);
                var network = new AgentNetwork(networkDefinition, new He(configuration.NetworkSeed));
                Workspace.Instance.SaveNetwork(NetworkName, network, overwrite: true);
            }

            var space = Workspace.Instance.Spaces.GetByName(configuration.Space);
            var simulationModel = new SimulationModelConfiguration();
            simulationModel.AgentDestructionModel.RemoveDestoryed = true;
            var pluginFactory = new NeuralSimulationPluginFactory("", NetworkName, configuration.TrainingSeed);
            pluginFactory.TrainingAnalyticsConfiguration.IterationLimit = configuration.TestingIterations;
            pluginFactory.TrainingAnalyticsConfiguration.Interval = configuration.TestingInterval;
            pluginFactory.TrainingConfiguration.IterationLimit = configuration.TrainingIterations;

            SimulationModel = simulationModel;
            SpaceTemplateFactory = space;
            PluginFactory = pluginFactory;
        }

        public void Train()
        {
            var trainingPlugin = PluginFactory.CreateTrainingPlugin();
            var trainingModelConfiguration = SimulationModel.Clone();
            trainingModelConfiguration.IterationLimit = PluginFactory.TrainingConfiguration.IterationLimit;
            var trainingSimulation = SimulationTemplate.CreateSimulation(trainingPlugin, "Training", SpaceTemplateFactory, trainingModelConfiguration);

            if (PluginFactory.TrainingAnalyticsConfiguration.Enabled)
            {
                var testingNetworkFile = new FileInfo("Testing.model");
                var testingBestNetworkFile = new FileInfo("Testing-Best.model");
                trainingPlugin.Network.Save(testingNetworkFile);
                var testingBestReward = 0.0;
                var testingBestReachedGoals = 0;
                var testingModelConfiguration = SimulationModel.Clone();
                testingModelConfiguration.IterationLimit = PluginFactory.TrainingAnalyticsConfiguration.IterationLimit;
                var chartForm = new ChartForm("Neural Agent Reinforcement Learning");

                Task.Run(() =>
                {
                    try
                    {
                        while (trainingSimulation.Iterate())
                        {
                            Console.Title = trainingSimulation.ToString();

                            if (trainingSimulation.Iteration % PluginFactory.TrainingAnalyticsConfiguration.Interval == 0)
                            {
                                trainingPlugin.Network.Save(testingNetworkFile);
                                var testingSeed = Guid.NewGuid().GetHashCode();
                                var testingPlugin = new NeuralSimulationPlugin(testingNetworkFile, null, testingSeed);
                                var testingSimulation = SimulationTemplate.CreateSimulation(testingPlugin, "Testing", SpaceTemplateFactory, testingModelConfiguration);
                                testingSimulation.Initialise();
                                while (testingSimulation.Iterate()) ;

                                foreach (var serie in testingSimulation.Results.Series)
                                {
                                    chartForm.InvokeAction(() => chartForm.AddPoint(serie.Key, serie.Value.Last()));
                                }

                                var predictions = string.Join(", ", testingPlugin.Predictions.Select(p => $"{p,4}"));

                                if (lines % 20 == 0)
                                {
                                    var headers = testingSimulation.Results.ToHeaderStrings();
                                    Console.WriteLine($"      {headers[0]}  Predictions (-,N,S,E,W)");
                                    Console.WriteLine($"      {headers[1]}  ============================");
                                }

                                if (testingPlugin.TotalReward - testingBestReward > -0.00001)
                                {
                                    testingBestReward = testingPlugin.TotalReward;
                                    testingBestReachedGoals = testingSimulation.TotalReachedGoals;
                                    trainingPlugin.Network.Save(testingBestNetworkFile.Ensure());
                                    Console.WriteLine($"Saved {testingSimulation.Results.ToDataString()}  {predictions}");
                                }
                                else
                                {
                                    Console.WriteLine($"      {testingSimulation.Results.ToDataString()}  {predictions}");
                                }

                                lines++;
                            }
                        }

                        string message = $"Do you want to update network '{PluginFactory.Network}'?\r\n\r\nTotal Reached Goals: {testingBestReachedGoals}\r\nTotal Reward: {testingBestReward}";

                        if (MessageBox.Show(message, "Training Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            new AgentNetwork(testingBestNetworkFile).Save(trainingPlugin.NetworkFile);
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                });

                //Application.Run(chartForm);
                chartForm.ShowDialog();
            }
        }

        public void Simulate()
        {
            var simultionPlugin = PluginFactory.CreatePlugin();
            SimulationModel.IterationLimit = 1000;
            SimulationTemplate.CreateSimulationForm(simultionPlugin, "", SpaceTemplateFactory, SimulationModel, 50).Show();
        }

        public void SaveAsTemplate(int trainingSeed = 0)
        {
            var pluginName = NetworkName;
            var plugin = Workspace.Instance.SimulationPlugins.FirstOrDefault(p => p.Name.Equals(pluginName));

            if (plugin == null)
            {
                plugin = new NeuralSimulationPluginFactory(pluginName, NetworkName, trainingSeed);
                Workspace.Instance.SimulationPlugins.Add(plugin);
            }

            var templateDefinitionName = $"{NetworkName}_{SpaceTemplateFactory.Name}";
            var templateDefinition = Workspace.Instance.SimulationTemplates.FirstOrDefault(t => t.Name.Equals(templateDefinitionName));

            if (templateDefinition == null)
            {
                templateDefinition = new SimulationTemplateDefinition(Workspace.Instance);
                templateDefinition.Space = SpaceTemplateFactory.Name;
                templateDefinition.SimulationPlugin = NetworkName;
                templateDefinition.Name = templateDefinitionName;
                Workspace.Instance.SimulationTemplates.Add(templateDefinition);
            }
        }
    }
}
