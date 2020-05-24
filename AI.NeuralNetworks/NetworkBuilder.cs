using Games.Utilities;
using System.Collections.Generic;

namespace AI.NeuralNetworks
{
    public class NetworkBuilder
    {
        public static Network Build(NetworkDefinition definition, ILayerInitializer initializer)
        {
            return new Network(BuildLayers(definition.Layers, initializer, definition.InputSize));
        }

        private static IEnumerable<Layer> BuildLayers(IEnumerable<NetworkLayerDefinition> definitions, ILayerInitializer initializer, int inputSize)
        {
            foreach (var definition in definitions)
            {
                yield return BuildLayer(definition, initializer, inputSize);
                inputSize = definition.Size;
            }
        }

        private static Layer BuildLayer(NetworkLayerDefinition definition, ILayerInitializer initializer, int inputSize)
        {
            var layerNeurons = new Neuron[definition.Size];
            layerNeurons.Initialize(() => new Neuron(new double[inputSize + 1]));
            var layer = new Layer(layerNeurons, definition.ActivationFunction);
            initializer.Initialize(layer);
            return layer;
        }

        //private static Network Build(FunctionName activationFunction, ILayerInitializer initializer, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        //{
        //    return new Network(BuildLayers(activationFunction, initializer, inputSize, outputSize, hiddenLayerSizes));
        //}
        //
        //private static IEnumerable<Layer> BuildLayers(FunctionName activationFunction, ILayerInitializer initializer, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        //{
        //    List<Layer> layers = new List<Layer>();
        //    
        //    for (int i = 0; i < hiddenLayerSizes.Length; i++)
        //    {
        //        int layerInputSize = i == 0 ? inputSize : hiddenLayerSizes[i - 1];
        //        var layerNeurons = new Neuron[hiddenLayerSizes[i]];
        //        layerNeurons.Initialize(() => new Neuron(new double[layerInputSize + 1]));
        //        var layer = new Layer(layerNeurons, activationFunction);
        //        initializer.Initialize(layer);
        //        layers.Add(layer);
        //    }
        //
        //    var lastLayerNeurons = new Neuron[outputSize];
        //    lastLayerNeurons.Initialize(() => new Neuron(new double[hiddenLayerSizes.Last() + 1]));
        //    var lastLayer = new Layer(lastLayerNeurons, activationFunction);
        //    initializer.Initialize(lastLayer);
        //    layers.Add(lastLayer);
        //    return layers;
        //}
    }
}
