using Labs.Agents.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralSimulationPluginFactory : ISimulationPluginFactory
    {
        public string Name { get; set; }
        //[DisplayName("Training Enabled")]
        //public bool TrainingEnabled { get; set; }
        [DisplayName("Training Configuration")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AgentNetworkTrainingConfiguration TrainingConfiguration { get; set; }
        [DisplayName("Training Analytics")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public TrainingProgressTrackerConfiguration TrainingAnalyticsConfiguration { get; set; }
        [TypeConverter(typeof(DropDownStringConverter))]
        public string Network { get; set; }
        public int TrainingSeed { get; set; }

        public NeuralSimulationPluginFactory(string name, string network, int trainingSeed)
        {
            Name = name;
            Network = network;
            TrainingSeed = trainingSeed;
            TrainingConfiguration = new AgentNetworkTrainingConfiguration();
            TrainingAnalyticsConfiguration = new TrainingProgressTrackerConfiguration();
        }

        // TODO: otypować mocniej
        public SimulationPlugin CreatePlugin()
        {
            var network = Workspace.Instance.GetNetworkFile(Network);
            return new NeuralSimulationPlugin(network, null, TrainingSeed);
        }

        public NeuralSimulationPlugin CreateTrainingPlugin()
        {
            var network = Workspace.Instance.GetNetworkFile(Network);
            return new NeuralSimulationPlugin(network, TrainingConfiguration, TrainingSeed);
        }

        public IEnumerable<string> GetNetworks()
        {
            return Workspace.Instance.NetworkNames;
        }

        public override string ToString()
        {
            string networkDescription = Workspace.Instance.GetNetworkDescription(Network);
            return $"{networkDescription}";
        }
    }
}
