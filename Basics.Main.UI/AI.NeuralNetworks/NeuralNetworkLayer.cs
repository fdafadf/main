using System;
using System.Linq;
using System.Text;

namespace Basics.AI.NeuralNetworks
{
    public class NeuralNetworkLayer
    {
        public double[] Input;
        public readonly double[] Output;
        public readonly Neuron2[] Neurons;

        public NeuralNetworkLayer(double[] input, double[] output, IActivationFunction activationFunction, Random random)
        {
            Input = input;
            Output = output;
            Neurons = new Neuron2[output.Length];

            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i] = new Neuron2(activationFunction, input.Length, random);
            }
        }

        public void Evaluate()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Output[i] = Neurons[i].Evaluate(Input);
            }
        }

        //public override string ToString()
        //{
        //    StringBuilder builder = new StringBuilder();
        //
        //    for (int i = 0; i < Neurons.Length; i++)
        //    {
        //        builder.Append("{");
        //        builder.Append(string.Join(",", Neurons[i].Weights.Select(w => w.ToString("F4"))));
        //        builder.Append("}");
        //    }
        //
        //    return builder.ToString();
        //}

        public double[][] GetWeights()
        {
            return Neurons.Select(neuron => neuron.Weights).ToArray();
        }

        public void SetWeights(double[][] weights)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].SetWeights(weights[i]);
            }
        }

        public void CalculateError(double[] trainingOutput)
        {
            for (int n = 0; n < Neurons.Length; n++)
            {
                double delta = -(trainingOutput[n] - Neurons[n].Output);
                Neurons[n].Error = delta * Neurons[n].Derivative;
            }
        }

        public void CalculateError(NeuralNetworkLayer nextLayer)
        {
            Neuron2[] nextLayerNeurons = nextLayer.Neurons;

            for (int n = 0; n < Neurons.Length; n++)
            {
                Neurons[n].Error = 0;

                for (int nn = 0; nn < nextLayerNeurons.Length; nn++)
                {
                    Neurons[n].Error += nextLayerNeurons[nn].Weights[n] * nextLayerNeurons[nn].Error;
                }

                Neurons[n].Error *= Neurons[n].Derivative;
            }
        }
    }
}
