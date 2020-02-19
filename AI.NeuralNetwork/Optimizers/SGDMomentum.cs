//#define MOMENTUM

using System;
using System.Linq;

namespace SimpleNeuralNetwork
{
    // Stochastic Gradient Descent with Momentum
    public class SGDMomentum : SGD
    {
        public double Momentum { get; }
        double[][][] previousWeights;

        public SGDMomentum(Network network, double learningRate, double momentum) : base(network, learningRate)
        {
            Momentum = momentum;
            previousWeights = network.Layers.Select(layer => layer.Neurons.Select(neuron => neuron.Weights.Clone() as double[]).ToArray()).ToArray();
        }

        protected override void UpdateLayerWeights(int layerIndex, double[] input, double learningRate)
        {
            Neuron[] layerNeurons = Network.Layers[layerIndex].Neurons;
            double[] layerGradient = gradient[layerIndex];
            double[] layerInput = layerIndex == 0 ? input : Network.LastEvaluation[layerIndex - 1];
            double[][] layerPreviousWeights = previousWeights[layerIndex];

            for (int neuronIndex = 0; neuronIndex < layerNeurons.Length; neuronIndex++)
            {
                double[] weights = layerNeurons[neuronIndex].Weights;
                double[] neuronPreviousWeights = layerPreviousWeights[neuronIndex];

                for (int i = 0; i < weights.Length - 1; i++)
                {
                    double previousWeight = weights[i];
                    weights[i] -= learningRate * layerGradient[neuronIndex] * layerInput[i] - Momentum * (weights[i] - neuronPreviousWeights[i]);
                    // L2
                    //weights[i] -= learningRate * layerGradient[neuronIndex] * layerInput[i] - Momentum * (weights[i] - neuronPreviousWeights[i]) + weights[i] * 0.5;
                    neuronPreviousWeights[i] = previousWeight;
                }

                double previousWeight2 = weights[weights.Length - 1];
                weights[weights.Length - 1] -= (learningRate * layerGradient[neuronIndex] * Neuron.Bias) - Momentum * (weights[weights.Length - 1] - neuronPreviousWeights[weights.Length - 1]);
                neuronPreviousWeights[weights.Length - 1] = previousWeight2;

                //for (int i = 0; i < weights.Length; i++)
                //{
                //    if (double.IsNaN(weights[i]) || double.IsInfinity(weights[i]))
                //    {
                //        throw new System.Exception();
                //    }
                //
                //    if (weights[i] == 0)
                //    {
                //        Console.WriteLine("DEAD");
                //    }
                //}
            }
        }

        public override string ToString()
        {
            return $"[{Network}] {Network.Layers[0].ActivationFunction} SGDM({LearningRate:f4}, {Momentum:f2})";
        }
    }
}
