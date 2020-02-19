using System.Runtime.CompilerServices;

namespace SimpleNeuralNetwork
{
    public class Layer
    {
        public Neuron[] Neurons { get; }
        public IFunction ActivationFunction { get; }

        public Layer(Neuron[] neurons, IFunction activationFunction, ILayerInitializer initializer)
        {
            Neurons = neurons;
            ActivationFunction = activationFunction;
            initializer.Initialize(this);
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
