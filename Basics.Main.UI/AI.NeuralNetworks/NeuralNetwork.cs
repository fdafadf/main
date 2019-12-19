using System.Collections.Generic;
using System.Linq;

namespace Basics.AI.NeuralNetworks
{
    public class NeuralNetwork
    {
        List<NeuralNetworkLayer> Layers = new List<NeuralNetworkLayer>();
        List<double[]> LayersOutput = new List<double[]>();

        public void AddLayer(int inputSize, int outputSize)
        {
            NeuralNetworkLayer layer = new NeuralNetworkLayer(inputSize, outputSize);
            LayersOutput.Add(new double[outputSize]);
            Layers.Add(layer);
        }

        public double[] Evaluate(double[] input)
        {
            double[] layerInput = input;

            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Evaluate(layerInput, LayersOutput[i]);
                layerInput = LayersOutput[i];
            }

            return LayersOutput.Last();
        }
    }
}
