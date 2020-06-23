using AI.NeuralNetworks;
using Labs.Agents.Forms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentTrainer : IDisposable
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
        DirectoryInfo TrainingOuputDirectory;
        StringBuilder Log = new StringBuilder();

        public NeuralAgentTrainer(NeuralAgentTrainerConfiguration configuration)
        {
            NetworkName = $"Auto-R{configuration.ViewRadius}-{string.Join("_", configuration.LayersSizes)}";
            InitialiseNetwork(NetworkName, configuration);
            TrainingPluginConfiguration = configuration.TrainingPluginConfiguration;
            TrainingSimulationConfiguration = configuration.TrainingSimulationConfiguration;
            TrainingProgressTrackerConfiguration = configuration.TrainingProgressTrackerConfiguration;
            TestingSimulationConfiguration = configuration.TrainingProgressSimulationConfiguration;
            SpaceTemplateFactory = Workspace.Instance.Spaces.GetByName(configuration.Space);
            TrainingOuputDirectory = Workspace.Instance.NeuralTrainingsDirectory.Ensure().CreateSubdirectory($"{NetworkName}-{DateTime.Now:yyyy_MM_dd-HH_mm}");
            WriteLine("Training Plugin Configuration");
            WriteLine("-----------------------------");
            WriteLine(TrainingPluginConfiguration.ConvertToJson());
            WriteLine("");
            WriteLine("Training Simulation Configuration");
            WriteLine("---------------------------------");
            WriteLine(TrainingSimulationConfiguration.ConvertToJson());
            WriteLine("");
            WriteLine("Training Progress Tracker Configuration");
            WriteLine("---------------------------------------");
            
            WriteLine(TrainingProgressTrackerConfiguration.ConvertToJson());
            WriteLine("");
            WriteLine("Training Simulation Configuration");
            WriteLine("---------------------------------");
            WriteLine(TestingSimulationConfiguration.ConvertToJson());
            WriteLine("");
        }

        public void Train()
        {
            var trainingNetwork = Workspace.Instance.GetNetworkFile(NetworkName);
            var trainingPlugin = new NeuralSimulationPlugin(trainingNetwork, TrainingPluginConfiguration, 0);
            var trainingSimulation = SimulationTemplate.CreateSimulation(trainingPlugin, "Training", SpaceTemplateFactory, TrainingSimulationConfiguration);

            if (TrainingProgressTrackerConfiguration.Enabled)
            {
                var testingNetworkFile = TrainingOuputDirectory.GetFile("Last.model");
                var testingBestNetworkFile = TrainingOuputDirectory.GetFile("Best.model");
                trainingPlugin.Network.Save(testingNetworkFile);
                var testingBestReward = 0.0;
                var testingBestReachedGoals = 0;
                var chartForm = new ChartForm("Neural Agent Reinforcement Learning");

                Task.Run(() =>
                {
                    try
                    {
                        DateTime startTime = DateTime.Now;
                        WriteLine($"Training Start: {startTime}");

                        while (trainingSimulation.Iterate())
                        {
                            Console.Title = trainingSimulation.ToString();

                            if (trainingSimulation.Iteration % TrainingProgressTrackerConfiguration.Interval == 0)
                            {
                                Test(trainingPlugin, testingNetworkFile, testingBestNetworkFile, ref testingBestReward, ref testingBestReachedGoals, chartForm);
                                lines++;
                            }
                        }

                        WriteLine($"Training Finished: {DateTime.Now}");
                        WriteLine($"Training Time: {DateTime.Now - startTime}");

                        chartForm.InvokeAction(() =>
                        {
                            Bitmap chartScreenshot = new Bitmap(chartForm.Width, chartForm.Height);
                            chartForm.DrawToBitmap(chartScreenshot, new Rectangle(0, 0, chartForm.Width, chartForm.Height));
                            var chartScreenshotFile = TrainingOuputDirectory.GetFile("Progress.png");
                            chartScreenshot.Save(chartScreenshotFile.FullName);
                        });

                        if (testingBestReward > 0)
                        {
                            string message = $"Do you want to update network '{NetworkName}'?\r\n\r\nTotal Reached Goals: {testingBestReachedGoals}\r\nTotal Reward: {testingBestReward}";

                            if (MessageBox.Show(message, "Training Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                new AgentNetwork(testingBestNetworkFile).Save(trainingPlugin.NetworkFile);
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        WriteLine(exception.ToString());
                    }
                });

                chartForm.ShowDialog();
            }
        }

        public void Simulate()
        {
            var simulationNetwork = Workspace.Instance.GetNetworkFile(NetworkName);
            var simulationPlugin = new NeuralSimulationPlugin(simulationNetwork, null, 0);
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

        public void Dispose()
        {
            TrainingOuputDirectory.GetFile("Log.txt").SetContent(Log.ToString());
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
                WriteLine($"      {headers[0]}  Predictions (-,N,S,E,W)");
                WriteLine($"      {headers[1]}  ============================");
            }

            if (testingPlugin.TotalReward - testingBestReward > -0.00001)
            {
                testingBestReward = testingPlugin.TotalReward;
                testingBestReachedGoals = testingSimulation.TotalReachedGoals;
                trainingPlugin.Network.Save(testingBestNetworkFile.Ensure());
                WriteLine($"Saved {testingSimulation.Results.ToDataString()}  {predictions}");
            }
            else
            {
                WriteLine($"      {testingSimulation.Results.ToDataString()}  {predictions}");
            }
        }

        private void WriteLine(string text)
        {
            Console.WriteLine(text);
            Log.AppendLine(text);
        }
    }
}
