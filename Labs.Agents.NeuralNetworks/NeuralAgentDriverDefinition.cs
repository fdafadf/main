using Labs.Agents.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentDriverDefinition : ISimulationPluginFactory
    {
        public string Name { get; set; }
        [DisplayName("Training Enabled")]
        public bool TrainingEnabled { get; set; }
        [DisplayName("Training Configuration")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AgentNetworkTrainingConfiguration TrainingConfiguration { get; set; }
        public int Seed { get; set; }
        public string Description { get; private set; }
        string network;

        public NeuralAgentDriverDefinition(string name, string network, int seed)
        {
            Name = name;
            Network = network;
            Seed = seed;
            TrainingConfiguration = new AgentNetworkTrainingConfiguration();
            
        }

        [TypeConverter(typeof(DropDownStringConverter))]
        public string Network 
        { 
            get
            {
                return network;
            }
            set
            {
                network = value;

                if (string.IsNullOrWhiteSpace(network) == false)
                {
                    Description = Workspace.Instance.LoadNetwork(network).ToString();
                }
            }
        }

        // TODO: otypować mocniej
        public SimulationPlugin CreatePlugin()
        {
            var network = Workspace.Instance.LoadNetwork(Network);
            return new NeuralAgentDriver(network, TrainingConfiguration, Seed);
        }

        public IEnumerable<string> GetNetworks()
        {
            return Workspace.Instance.NetworkNames;
        }
    }
}
