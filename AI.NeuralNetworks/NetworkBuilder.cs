using Games.Utilities;
using System.Collections.Generic;

namespace AI.NeuralNetworks
{
    public class NetworkBuilder
    {
        public static Network Build(NetworkDefinition definition, ILayerInitializer initializer)
        {
            var network = new Network(BuildLayers(definition.Layers, definition.InputSize));
            network.InitializeLayers(initializer);
            return network;
        }

        private static IEnumerable<Layer> BuildLayers(IEnumerable<NetworkLayerDefinition> definitions, int inputSize)
        {
            foreach (var definition in definitions)
            {
                yield return BuildLayer(definition, inputSize);
                inputSize = definition.Size;
            }
        }

        private static Layer BuildLayer(NetworkLayerDefinition definition, int inputSize)
        {
            var layerNeurons = new Neuron[definition.Size];
            layerNeurons.InitializeArray(() => new Neuron(new double[inputSize + 1]));
            var layer = new Layer(layerNeurons, definition.ActivationFunction);
            return layer;
        }
    }
}
