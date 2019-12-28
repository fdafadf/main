using System;

namespace AI.NeuralNetworks
{
    public class Neuron : INeuron
    {
        public IActivationFunction ActivationFunction;
        public double[] Weights { get; private set; }
        public double Output;
        public double Derivative;
        public double Error;

        public Neuron(IActivationFunction activationFunction, int inputSize, Random random)
        {
            ActivationFunction = activationFunction;
            Weights = new double[inputSize + 1];
            Weights[Weights.Length - 1] = random.NextDouble();

            for (int i = 0; i < inputSize; i++)
            {
                Weights[i] = random.NextDouble();
            }
        }

        public double Evaluate(double[] input)
        {
            double sum = Weights[Weights.Length - 1];

            for (int i = 0; i < input.Length; i++)
            {
                sum += Weights[i] * input[i];
            }

            Output = ActivationFunction.Value(sum);
            Derivative = ActivationFunction.DerivativeValue(Output);
            return Output;
        }

        public void SetWeights(double[] weights)
        {
            Weights = weights;
        }
    }
}
