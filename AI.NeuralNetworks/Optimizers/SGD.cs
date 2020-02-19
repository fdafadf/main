//#define MOMENTUM

using System;
using System.Collections.Generic;
using System.Linq;
using Games.Utilities;

namespace AI.NeuralNetwork
{
    // Stochastic Gradient Descent
    public class SGD : Optimizer
    {
        public SGD(Network network, double learningRate) : base(network, learningRate)
        {
            gradient = network.Layers.Select(layer => new double[layer.Neurons.Length]).ToArray();
        }

        public override double[] Optimize(double[] features, double[] labels)
        {
            double[] output = Network.Evaluate(features);
            CalculateOutputLayerGradient(output, labels);

            for (int i = gradient.Length - 2; i >= 0; i--)
            {
                CalculateHiddenLayerGradient(layerIndex: i);
            }

            for (int layerIndex = 0; layerIndex < gradient.Length; layerIndex++)
            {
                UpdateLayerWeights(layerIndex, features, LearningRate);
            }

            return output;
        }

        private void CalculateOutputLayerGradient(double[] output, double[] labels)
        {
            double[] layerGradient = gradient[gradient.Length - 1];
            IFunction activationFunction = Network.Layers.Last().ActivationFunction;

            for (int i = 0; i < layerGradient.Length; i++)
            {
                double error = -(labels[i] - output[i]);
                layerGradient[i] = error * activationFunction.Derivative(output[i]);

                //if (double.IsNaN(layerGradient[i]) || double.IsInfinity(layerGradient[i]))
                //{
                //    throw new System.Exception();
                //}
            }
        }

        private void CalculateHiddenLayerGradient(int layerIndex)
        {
            double[] layerGradient = gradient[layerIndex];
            double[] layerOutput = Network.LastEvaluation[layerIndex];
            double[] nextLayerGradient = gradient[layerIndex + 1];
            Neuron[] nextLayerNeurons = Network.Layers[layerIndex + 1].Neurons;
            IFunction activationFunction = Network.Layers[layerIndex].ActivationFunction;

            for (int i = 0; i < layerGradient.Length; i++)
            {
                double error = 0;

                for (int j = 0; j < nextLayerNeurons.Length; j++)
                {
                    error += nextLayerNeurons[j].Weights[i] * nextLayerGradient[j];
                }

                layerGradient[i] = error * activationFunction.Derivative(layerOutput[i]); // * (1.0 / nextLayerNeurons.Length);

                //if (double.IsNaN(layerGradient[i]) || double.IsInfinity(layerGradient[i]))
                //{
                //    throw new System.Exception();
                //}
            }
        }

        protected virtual void UpdateLayerWeights(int layerIndex, double[] features, double learningRate)
        {
            Layer layer = Network.Layers[layerIndex];
            Neuron[] layerNeurons = layer.Neurons;
            double[] layerGradient = gradient[layerIndex];
            double[] layerInput = layerIndex == 0 ? features : Network.LastEvaluation[layerIndex - 1];

            for (int neuronIndex = 0; neuronIndex < layerNeurons.Length; neuronIndex++)
            {
                double[] weights = layerNeurons[neuronIndex].Weights;

                for (int i = 0; i < weights.Length - 1; i++)
                {
                    weights[i] -= LearningRate * layerGradient[neuronIndex] * layerInput[i];
                    // L2
                    //weights[i] -= learningRate * layerGradient[neuronIndex] * layerInput[i] + weights[i] * 0.5;
                }

                weights[weights.Length - 1] -= learningRate * layerGradient[neuronIndex] * Neuron.Bias;

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
            return $"[{Network}] {Network.Layers[0].ActivationFunction} SGD({LearningRate:f4})";
        }
    }
}
