using System;
using System.Collections.Generic;
using System.Linq;

namespace Basics.Main.UI
{
    class Perceptron
    {
        public double[] Weights;
        public double BiasWeight;
        public double Bias = -1;

        public Perceptron(int size, double min, double max)
        {
            Weights = new double[size];
            Rand(min, max);
        }

        public void Rand(double min, double max)
        {
            Weights.Fill(min, max);
            BiasWeight = Extensions.Random.NextDouble();
        }

        public double Output(double[] input)
        {
            return input.Product(Weights) + Bias * BiasWeight;
        }

        public void Train(IEnumerable<TestData> testData, Func<double, double> classifier, double alpha, int iterations)
        {
            TestData[] test = testData.ToArray();

            for (int j = 0; j < iterations; j++)
            {
                for (int i = 0; i <= test.Count() - 1; i++)
                {
                    double output = classifier(Output(test[i].Input));

                    if (output != test[i].Output)
                    {
                        Bias = Bias + alpha * (test[i].Output - output) * BiasWeight / 2;

                        for (int w = 0; w < Weights.Length; w++)
                        {
                            Weights[w] = Weights[w] + alpha * (test[i].Output - output) * test[i].Input[w] / 2;
                        }
                    }
                }
            }
        }
    }
}
