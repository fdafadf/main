using Games.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class Perceptron : INeuron
    {
        public double[] Weights { get; }
        public double BiasWeight;
        public double Bias = 1;
        IActivationFunction ActivationFunction;

        public Perceptron(double[] weights, double bias, IActivationFunction activationFunction)
        {
            Weights = weights;
            BiasWeight = bias;
            ActivationFunction = activationFunction;
        }

        public Perceptron(int size, IActivationFunction activationFunction, double min, double max)
        {
            Weights = new double[size];
            ActivationFunction = activationFunction;
            Rand(min, max);
        }

        public void Rand(double min, double max)
        {
            Weights.FillWithRandomValues(min, max);
            BiasWeight = Extensions.Random.NextDouble();
        }

        public double Evaluate(double[] input)
        {
            double sum = Sum(input);
            return ActivationFunction.Value(sum);
        }

        public double Evaluate(double[] input, out double outputDerivative)
        {
            double sum = Sum(input);
            double output = ActivationFunction.Value(sum);
            outputDerivative = ActivationFunction.DerivativeValue(output);
            return output;
        }

        public double Sum(double[] input)
        {
            return input.Product(Weights) + Bias * BiasWeight;
        }

        public void Train(IEnumerable<NeuralIO<double>> testData, double alpha, int maxEpoches)
        {
            NeuralIO<double>[] test = testData.ToArray();
            int testDataSize = test.Count();
            int epoch = 0;
            double meanSquaredError = double.MaxValue;

            while (epoch++ < maxEpoches)
            {
                double outputErrorSum = 0;

                for (int i = 0; i < testDataSize; i++)
                {
                    double outputDerivative;
                    double output = Evaluate(test[i].Input, out outputDerivative);
                    double outputError = test[i].Output - output;
                    //double outputError = targetOutput - test[i].Output;
                    outputErrorSum += outputError * outputError;

                    if (output != test[i].Output)
                    {
                        Bias = Bias + alpha * outputError * BiasWeight / 2;

                        for (int w = 0; w < Weights.Length; w++)
                        {
                            //Weights[w] += alpha * outputError * test[i].Input[w] / 2;
                            Weights[w] += alpha * outputError * test[i].Input[w] * outputDerivative;
                        }
                    }
                }

                meanSquaredError = 1.0 / (2.0 * testDataSize) * outputErrorSum;

                if (epoch % 100 == 0)
                {
                    //Console.WriteLine($"Mean squared error after {epoch} epoches is {meanSquaredError:F8}");
                }

                if (meanSquaredError < 0.0001)
                {
                    break;
                }
            }

            //Console.WriteLine($"Perceptron trained after {epoch} epoches");
            //Console.WriteLine($"Mean squared error: {meanSquaredError:F8}");
        }
    }
}
