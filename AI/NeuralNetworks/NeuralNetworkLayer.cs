using System;
using System.Linq;
using System.Text;

namespace AI.NeuralNetworks
{
    public class NeuralNetworkLayer
    {
        public double[] Input;
        public readonly double[] Output;
        public readonly Neuron[] Neurons;

        public NeuralNetworkLayer(double[] input, double[] output, IActivationFunction activationFunction, Random random)
        {
            Input = input;
            Output = output;
            Neurons = new Neuron[output.Length];

            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i] = new Neuron(activationFunction, input.Length, random);
            }
        }

        public void Evaluate()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Output[i] = Neurons[i].Evaluate(Input);
            }
        }

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
            Neuron[] nextLayerNeurons = nextLayer.Neurons;

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
