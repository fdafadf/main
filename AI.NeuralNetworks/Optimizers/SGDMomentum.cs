using System;
using System.Linq;
using System.Numerics;

namespace AI.NeuralNetworks
{
    //public static class Extensions
    //{
    //    public static Vector<T> Clone<T>(this Vector<T> self) where T : struct
    //    {
    //        T[] array = new T[Vector<T>.Count];
    //        self.CopyTo()
    //        Vector<T> result = new Vector<T>();
    //    }
    //}

    // Stochastic Gradient Descent with Momentum
    public class SGDMomentum : SGD
    {
        public double Momentum { get; }
        readonly double[][][] previousWeights;

        public SGDMomentum(Network network, double learningRate, double momentum) : base(network, learningRate)
        {
            Momentum = momentum;
            previousWeights = network.Layers.Select(layer => layer.Neurons.Select(neuron => neuron.Weights.Clone() as double[]).ToArray()).ToArray();
        }

        protected override void UpdateLayerWeights(int layerIndex, int batchSize)
        {
            Neuron[] layerNeurons = Network.Layers[layerIndex].Neurons;
            //double[] layerInput = layerIndex == 0 ? input : Network.LastEvaluation[layerIndex - 1];
            double[][] layerPreviousWeights = previousWeights[layerIndex];

            for (int neuronIndex = 0; neuronIndex < layerNeurons.Length; neuronIndex++)
            {
                double[] weights = layerNeurons[neuronIndex].Weights;
                double[] layerGradient = gradient.Accumulation[layerIndex][neuronIndex];
                double[] neuronPreviousWeights = layerPreviousWeights[neuronIndex];
                //double eg = LearningRate * layerGradient[neuronIndex];

                for (int i = 0; i < weights.Length - 1; i++)
                {
                    double previousWeight = weights[i];
                    weights[i] -= LearningRate * layerGradient[i] - Momentum * (weights[i] - neuronPreviousWeights[i]);
                    neuronPreviousWeights[i] = previousWeight;
                }

                double previousWeight2 = weights[weights.Length - 1];
                weights[weights.Length - 1] -= LearningRate * layerGradient[weights.Length - 1] - Momentum * (weights[weights.Length - 1] - neuronPreviousWeights[weights.Length - 1]);
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
