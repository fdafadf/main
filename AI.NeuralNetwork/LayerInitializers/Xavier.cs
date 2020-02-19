using System;

namespace AI.NeuralNetwork
{
    public class Xavier : ILayerInitializer
    {
        GaussianRandom random;
        Random random2;

        public Xavier()
        {
            random = new GaussianRandom();
            random2 = new Random(0);
        }

        public Xavier(int seed)
        {
            random = new GaussianRandom(seed);
            random2 = new Random(seed);
        }

        public void Initialize(Layer layer)
        {
            double a = Math.Sqrt(2.0 / (layer.Neurons.Length + layer.Neurons[0].Weights.Length));
            
            foreach (var neuron in layer.Neurons)
            {
                for (int i = 0; i < neuron.Weights.Length; i++)
                {
                    neuron.Weights[i] = (random.NextDouble() * a) - (a * 0.5);
                }
            }
        }
    }
}
