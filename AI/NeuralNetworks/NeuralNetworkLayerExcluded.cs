using Basics.Main.UI;
using System;
using System.Linq;
using System.Text;

namespace Basics.AI.NeuralNetworks
{
    public class NeuralNetworkLayer
    {
        public INeuron[] Neurons;

        public NeuralNetworkLayer(int inputSize, int layerSize, Random weightRandomizer)
        {
            Neurons = new INeuron[layerSize];

            for (int i = 0; i < layerSize; i++)
            {
                double bias = weightRandomizer.NextDouble();
                double[] weights = weightRandomizer.Fill(new double[inputSize]);
                Neurons[i] = new Perceptron(weights, bias, ActivationFunctions.Sigmoid);
            }
        }

        public void Evaluate(double[] input, double[] output)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                output[i] = Neurons[i].Sum(input);
            }
        }

        public void Evaluate(double[] input, double[] output, double[] outputDerivative)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                output[i] = Neurons[i].Evaluate(input, out outputDerivative[i]);
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Neurons.Length; i++)
            {
                builder.Append("{");
                builder.Append(string.Join(",", Neurons[i].Weights.Select(w => w.ToString("F4"))));
                builder.Append("}");
            }

            return builder.ToString();
        }
    }
}
