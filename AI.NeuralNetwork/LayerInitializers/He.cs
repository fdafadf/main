using System;

namespace AI.NeuralNetwork
{
    public class He : ILayerInitializer
    {
        Random random;

        public He()
        {
            random = new Random(0);
        }

        public He(int seed)
        {
            random = new Random(seed);
        }

        public void Initialize(Layer layer)
        {
            double std = Math.Sqrt(2.0) / Math.Sqrt(layer.Neurons[0].Weights.Length);
            double bound = Math.Sqrt(3.0) * std;

            foreach (var neuron in layer.Neurons)
            {
                for (int i = 0; i < neuron.Weights.Length; i++)
                {
                    neuron.Weights[i] = random.NextDouble() * 2 * bound - bound;
                }
            }
        }
    }
}
