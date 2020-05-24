using AI.NeuralNetworks;
using Labs.Agents.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetworkDefinition
    {
        public const FunctionName DefaultActivationFunction = FunctionName.LeakyReLU;

        public int ViewRadius { get; set; }
        [TypeConverter(typeof(CsvTypeConverter))]
        public List<NetworkLayerDefinition> Layers { get; set; }
        public FunctionName LastLayerActivationFunction { get; set; } = DefaultActivationFunction;
        string name;

        public AgentNetworkDefinition(string name, int viewRadius, params int[] layersSizes)
        {
            this.name = name;
            ViewRadius = viewRadius;
            Layers = layersSizes.Select(layerSize => new NetworkLayerDefinition(DefaultActivationFunction, layerSize)).ToList();
        }

        public AgentNetworkDefinition()
        {
            ViewRadius = 5;
            Layers = new List<NetworkLayerDefinition>();
            Layers.Add(new NetworkLayerDefinition(DefaultActivationFunction, 114));
            Layers.Add(new NetworkLayerDefinition(DefaultActivationFunction, 228));
            Layers.Add(new NetworkLayerDefinition(DefaultActivationFunction, 114));
            Layers.Add(new NetworkLayerDefinition(DefaultActivationFunction, 57));
        }

        public string Name 
        {
            get
            {
                return name ?? ToString();
            }
            set
            {
                name = value;
            }
        }

        public override string ToString()
        {
            var inputCoder = new AgentNetworkInputCoder(ViewRadius);
            return $"{Layers.First().ActivationFunction}, {ViewRadius}, [{inputCoder.EncodedSize},{string.Join(",", Layers.Select(l => l.Size))},1]";
        }

        public string ToToolTipString()
        {
            var inputCoder = new AgentNetworkInputCoder(ViewRadius);
            string[] lines =
            {
                $"Activation Function: {Layers.First().ActivationFunction}",
                $"View Radius: {ViewRadius}",
                $"Layers: {inputCoder.EncodedSize},{string.Join(",", Layers.Select(l => l.Size))},1",
            };
            return string.Join("\r\n", lines);
        }
    }
}
