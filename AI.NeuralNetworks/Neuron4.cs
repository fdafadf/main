using System.Numerics;
using System.Runtime.CompilerServices;

namespace AI.NeuralNetworks
{
    public class Neuron4
    {
        public Vector4[] Weights { get; }

        public Neuron4(int inputSize)
        {
            Weights = new Vector4[inputSize + 1];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sum(Vector4[] input)
        {
            double result = 0;

            for (int i = 0; i < input.Length; i++)
            {
                Vector4.Dot(input[i], Weights[i]);
            }

            return result;
        }
    }
}
