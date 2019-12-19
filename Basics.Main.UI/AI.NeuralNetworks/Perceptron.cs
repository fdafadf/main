using Basics.Main.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Basics.AI.NeuralNetworks
{
    public class Perceptron : INeuron
    {
        public double[] Weights;
        public double BiasWeight;
        public double Bias = -1;
        Func<double, double> ActivationFunction;

        public Perceptron(int size, Func<double, double> activationFunction, double min, double max)
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

        public double Output(double[] input)
        {
            return input.Product(Weights) + Bias * BiasWeight;
        }

        public void Train(IEnumerable<NeuralIO> testData, double alpha, int maxIterations)
        {
            NeuralIO[] test = testData.ToArray();
            int testDataSize = test.Count();
            int performedIterations = 0;
            double meanSquaredError = double.MaxValue;

            while (performedIterations++ < maxIterations)
            {
                double outputErrorSum = 0;

                for (int i = 0; i < testDataSize; i++)
                {
                    double targetOutput = ActivationFunction(Output(test[i].Input));
                    double outputError = test[i].Output - targetOutput;
                    outputErrorSum += outputError * outputError;

                    if (targetOutput != test[i].Output)
                    {
                        Bias = Bias + alpha * outputError * BiasWeight / 2;

                        for (int w = 0; w < Weights.Length; w++)
                        {
                            Weights[w] = Weights[w] + alpha * outputError * test[i].Input[w] / 2;
                        }
                    }
                }

                meanSquaredError = 1.0 / (2.0 * testDataSize) * outputErrorSum;

                if (performedIterations % 100 == 0)
                {
                    Console.WriteLine($"Mean squared error after {performedIterations} iterations is {meanSquaredError:F8}");
                }

                if (meanSquaredError < 0.0001)
                {
                    break;
                }
            }

            Console.WriteLine($"Perceptron trained after {performedIterations} iterations");
            Console.WriteLine($"Mean squared error: {meanSquaredError:F8}");
        }
    }
}
