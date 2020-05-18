using System.Runtime.CompilerServices;

namespace AI.NeuralNetworks
{
    public class Neuron
    {
        public const double Bias = 1;
        public double[] Weights { get; }

        public Neuron(double[] weights)
        {
            Weights = weights;
        }

        private Neuron(int inputSize)
        {
            Weights = new double[inputSize + 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sum(double[] input)
        {
            double sum = Weights[input.Length] * Bias;

            for (int i = 0; i < input.Length; i++)
            {
                sum += Weights[i] * input[i];
            }

            return sum;
        }
    }
}
