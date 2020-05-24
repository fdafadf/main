using Games.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI.NeuralNetworks
{
    public static class NetworkSerializer
    {
        public static void SaveLayers(BinaryWriter writer, IEnumerable<Layer> layers)
        {
            writer.Write(layers.Count());
            writer.Write(layers.First().Neurons.First().Weights.Length - 1);

            foreach (var layer in layers)
            {
                SaveLayer(writer, layer);
            }
        }

        private static void SaveLayer(BinaryWriter writer, Layer layer)
        {
            writer.Write((int)layer.ActivationFunctionName);
            writer.Write(layer.Neurons.Length);

            for (int i = 0; i < layer.Neurons.Length; i++)
            {
                writer.WriteDoubleArray(layer.Neurons[i].Weights);
            }
        }

        public static IEnumerable<Layer> LoadLayers(BinaryReader reader)
        {
            int numberOfLayers = reader.ReadInt32();
            int inputSize = reader.ReadInt32();
            List<Layer> layers = new List<Layer>();

            for (int i = 0; i < numberOfLayers; i++)
            {
                var layer = LoadLayer(reader, inputSize);
                layers.Add(layer);
                inputSize = layer.Neurons.Length;
            }

            return layers;
        }

        private static Layer LoadLayer(BinaryReader reader, int inputSize)
        {
            FunctionName activationFunctionName = reader.ReadEnum<FunctionName>();
            int layerSize = reader.ReadInt32();
            List<Neuron> neurons = new List<Neuron>();

            for (int i = 0; i < layerSize; i++)
            {
                double[] weights = reader.ReadDoubleArray(inputSize + 1);
                neurons.Add(new Neuron(weights));
            }

            return new Layer(neurons, activationFunctionName);
        }
    }
}
