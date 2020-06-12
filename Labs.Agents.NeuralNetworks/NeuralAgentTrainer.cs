using AI.NeuralNetworks;
using Labs.Agents.Forms;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentTrainerConfiguration
    {
        [Category("General")]
        public int ViewRadius { get; set; } = 3;
        [Category("General")]
        public int[] LayersSizes { get; set; } = new int[] { 84, 42, 21, 7, 1 };
        [Category("General")]
        public string Space { get; set; }
        [Category("General")]
        public int NetworkSeed { get; set; } = 0;
        [Category("Shortcuts")]
        [DisplayName("TrainingSimulationConfiguration.IterationLimit")]
        public int TrainingIterations
        {
            get
            {
                return TrainingSimulationConfiguration.IterationLimit;
            }
            set
            {
                TrainingSimulationConfiguration.IterationLimit = value;
            }
        }
        [Category("Shortcuts")]
        [DisplayName("TrainingSimulationConfiguration.Seed")]
        public int? TrainingSeed
        {
            get
            {
                return TrainingSimulationConfiguration.Seed;
            }
            set
            {
                TrainingSimulationConfiguration.Seed = value;
            }
        }
        [Category("Shortcuts")]
        [DisplayName("TrainingProgressTrackerConfiguration.IterationLimit")]
        public int TestingIterations
        {
            get
            {
                return TrainingProgressTrackerConfiguration.IterationLimit;
            }
            set
            {
                TrainingProgressTrackerConfiguration.IterationLimit = value;
            }
        }
        [Category("Shortcuts")]
        [DisplayName("TrainingProgressTrackerConfiguration.Interval")]
        public int TestingInterval
        {
            get
            {
                return TrainingProgressTrackerConfiguration.Interval;
            }
            set
            {
                TrainingProgressTrackerConfiguration.Interval = value;
            }
        }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AgentNetworkTrainingConfiguration TrainingPluginConfiguration { get; set; }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public SimulationModelConfiguration TrainingSimulationConfiguration { get; set; }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public TrainingProgressTrackerConfiguration TrainingProgressTrackerConfiguration { get; set; }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public SimulationModelConfiguration TrainingProgressSimulationConfiguration { get; set; }

        public NeuralAgentTrainerConfiguration()
        {
            TrainingPluginConfiguration = new AgentNetworkTrainingConfiguration();
            TrainingSimulationConfiguration = new SimulationModelConfiguration();
            TrainingProgressTrackerConfiguration = new TrainingProgressTrackerConfiguration();
            TrainingProgressSimulationConfiguration = new SimulationModelConfiguration();
            TrainingSimulationConfiguration.AgentDestructionModel.RemoveDestoryed = true;
            TrainingSimulationConfiguration.IterationLimit = 50000;
            TrainingProgressSimulationConfiguration.AgentDestructionModel.RemoveDestoryed = true;
        }
    }

    public class NeuralAgentTrainer
    {
        public static void InitialiseNetwork(string networkName, NeuralAgentTrainerConfiguration configuration)
        {
            var networkFile = Workspace.Instance.GetNetworkFile(networkName);
            var createNetwork = true;

            if (networkFile.Exists)
            {
                var overwrite = MessageBox.Show($"Do you want to reset existing network '{networkName}'?", "Training", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                createNetwork = overwrite == DialogResult.Yes;
            }

            if (createNetwork)
            {
                var networkDefinition = new AgentNetworkDefinition(networkName, configuration.ViewRadius, configuration.LayersSizes);
                var network = new AgentNetwork(networkDefinition, new He(configuration.NetworkSeed));
                Workspace.Instance.SaveNetwork(networkName, network, overwrite: true);
            }
        }

        AgentNetworkTrainingConfiguration TrainingPluginConfiguration;
        SimulationModelConfiguration TrainingSimulationConfiguration;
        TrainingProgressTrackerConfiguration TrainingProgressTrackerConfiguration;
        SimulationModelConfiguration TestingSimulationConfiguration;
        ISpaceTemplateFactory SpaceTemplateFactory;
        string NetworkName;
        int lines;

        public NeuralAgentTrainer(NeuralAgentTrainerConfiguration configuration)
        {
            NetworkName = $"Auto-R{configuration.ViewRadius}-{string.Join("_", configuration.LayersSizes)}";
            InitialiseNetwork(NetworkName, configuration);
            TrainingPluginConfiguration = configuration.TrainingPluginConfiguration;
            TrainingSimulationConfiguration = configuration.TrainingSimulationConfiguration;
            TrainingProgressTrackerConfiguration = configuration.TrainingProgressTrackerConfiguration;
            TestingSimulationConfiguration = configuration.TrainingProgressSimulationConfiguration;
            SpaceTemplateFactory = Workspace.Instance.Spaces.GetByName(configuration.Space);
        }

        public void Train()
        {
            var trainingNetwork = Workspace.Instance.GetNetworkFile(NetworkName);
            var trainingPlugin = new NeuralSimulationPlugin(trainingNetwork, TrainingPluginConfiguration, 0);
            var trainingSimulation = SimulationTemplate.CreateSimulation(trainingPlugin, "Training", SpaceTemplateFactory, TrainingSimulationConfiguration);

            if (TrainingProgressTrackerConfiguration.Enabled)
            {
                var testingNetworkFile = new FileInfo("Testing.model");
                var testingBestNetworkFile = new FileInfo("Testing-Best.model");
                trainingPlugin.Network.Save(testingNetworkFile);
                var testingBestReward = 0.0;
                var testingBestReachedGoals = 0;
                var chartForm = new ChartForm("Neural Agent Reinforcement Learning");

                Task.Run(() =>
                {
                    try
                    {
                        while (trainingSimulation.Iterate())
                        {
                            Console.Title = trainingSimulation.ToString();

                            if (trainingSimulation.Iteration % TrainingProgressTrackerConfiguration.Interval == 0)
                            {
                                Test(trainingPlugin, testingNetworkFile, testingBestNetworkFile, ref testingBestReward, ref testingBestReachedGoals, chartForm);
                                lines++;
                            }
                        }

                        string message = $"Do you want to update network '{NetworkName}'?\r\n\r\nTotal Reached Goals: {testingBestReachedGoals}\r\nTotal Reward: {testingBestReward}";

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

                chartForm.ShowDialog();
            }
        }

        public void Simulate()
        {
            var simulationNetwork = Workspace.Instance.GetNetworkFile(NetworkName);
            var simulationPlugin = new NeuralSimulationPlugin(simulationNetwork, TrainingPluginConfiguration, 0);
            SimulationTemplate.CreateSimulationForm(simulationPlugin, "", SpaceTemplateFactory, TestingSimulationConfiguration, 50).Show();
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

        private void Test(NeuralSimulationPlugin trainingPlugin, FileInfo testingNetworkFile, FileInfo testingBestNetworkFile, ref double testingBestReward, ref int testingBestReachedGoals, ChartForm chartForm)
        {
            trainingPlugin.Network.Save(testingNetworkFile);
            var testingSeed = Guid.NewGuid().GetHashCode();
            var testingPlugin = new NeuralSimulationPlugin(testingNetworkFile, null, testingSeed);
            var testingSimulation = SimulationTemplate.CreateSimulation(testingPlugin, "Testing", SpaceTemplateFactory, TestingSimulationConfiguration);
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
        }
    }
}
