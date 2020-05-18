using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AI.NeuralNetworks
{
    public class Layer
    {
        public Neuron[] Neurons { get; }
        public IFunction ActivationFunction { get; }
        public FunctionName ActivationFunctionName { get; }

        public Layer(IEnumerable<Neuron> neurons, FunctionName activationFunctionName)
        {
            Neurons = neurons.ToArray();
            ActivationFunction = Function.Get(activationFunctionName);
            ActivationFunctionName = activationFunctionName;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Evaluate(double[] input, double[] output)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                output[i] = ActivationFunction.Value(Neurons[i].Sum(input));
            }
        }
    }
}
