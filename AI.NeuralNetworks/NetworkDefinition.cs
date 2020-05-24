using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class NetworkDefinition
    {
        public int InputSize { get; set; }
        public List<NetworkLayerDefinition> Layers { get; set; } = new List<NetworkLayerDefinition>();

        public NetworkDefinition(int inputSize, IEnumerable<NetworkLayerDefinition> layers)
        {
            InputSize = inputSize;
            Layers.AddRange(layers);
        }
        
        public NetworkDefinition(FunctionName activationFunction, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        {
            InputSize = inputSize;

            foreach (var layerSize in hiddenLayerSizes.Concat(new [] { outputSize }))
            {
                Layers.Add(new NetworkLayerDefinition(activationFunction, layerSize));
            }
        }
    }
}
