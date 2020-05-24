using Labs.Agents.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralSimulationPluginFactory : ISimulationPluginFactory
    {
        public string Name { get; set; }
        [DisplayName("Training Enabled")]
        public bool TrainingEnabled { get; set; }
        [DisplayName("Training Configuration")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AgentNetworkTrainingConfiguration TrainingConfiguration { get; set; }
        [TypeConverter(typeof(DropDownStringConverter))]
        public string Network { get; set; }
        public int Seed { get; set; }

        public NeuralSimulationPluginFactory(string name, string network, int seed)
        {
            Name = name;
            Network = network;
            Seed = seed;
            TrainingConfiguration = new AgentNetworkTrainingConfiguration();
            
        }

        // TODO: otypować mocniej
        public SimulationPlugin CreatePlugin()
        {
            var network = Workspace.Instance.GetNetworkFile(Network);
            return new NeuralSimulationPlugin(network, TrainingEnabled ? TrainingConfiguration : null, Seed);
        }

        public IEnumerable<string> GetNetworks()
        {
            return Workspace.Instance.NetworkNames;
        }

        public override string ToString()
        {
            string networkDescription = Workspace.Instance.GetNetworkDescription(Network);
            return TrainingEnabled ? $"{networkDescription} {TrainingConfiguration}" : $"{networkDescription}";
        }
    }
}
