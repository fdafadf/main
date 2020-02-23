using AI.NeuralNetwork;
using System;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class AdaGrad : Optimizer
    {
        protected Gradient gradient;
        protected Gradient accumulation;

        public AdaGrad(Network network, double learningRate) : base(network, learningRate)
        {
            gradient = new Gradient(network);
            accumulation = new Gradient(network);
        }

        public override double[] Evaluate(double[] features, double[] labels)
        {
            double[] output = Network.Evaluate(features);
            Accumulate();
            return output;
        }

        private void Accumulate()
        {
            for (int i = 0; i < gradient.Values.Length; i++)
            {
                double[][] g = gradient.Accumulation[i];
                double[] a = accumulation.Values[i];

                for (int j = 0; j < g.Length; j++)
                {
                    a[j] += g[j].Sum() * g[j].Sum();
                }
            }
        }

        public override void Update(int batchSize)
        {
            for (int layerIndex = 0; layerIndex < gradient.Values.Length; layerIndex++)
            {
                UpdateLayerWeights(layerIndex, batchSize);
            }

            gradient.Clear();
            accumulation.Clear();
        }

        protected virtual void UpdateLayerWeights(int layerIndex, double batchSize)
        {
            Layer layer = Network.Layers[layerIndex];
            Neuron[] layerNeurons = layer.Neurons;
            double[] layerAccumulation = accumulation.Values[layerIndex];

            for (int n = 0; n < layerNeurons.Length; n++)
            {
                double[] layerGradient = gradient.Accumulation[layerIndex][n];
                double[] weights = layerNeurons[n].Weights;
                double r = Math.Sqrt(layerAccumulation[n]);

                if (r == 0)
                {
                    for (int i = 0; i < weights.Length; i++)
                    {
                        weights[i] -= layerGradient[i];
                    }
                }
                else
                {
                    for (int i = 0; i < weights.Length; i++)
                    {
                        weights[i] -= (LearningRate / r) * layerGradient[n];
                    }
                }
            }
        }

        public override string ToString()
        {
            return $"[{Network}] {Network.Layers[0].ActivationFunction} AdaGrad({LearningRate:f4})";
        }
    }
}
