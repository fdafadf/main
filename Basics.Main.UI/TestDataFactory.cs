using System;
using System.Collections.Generic;
using System.Linq;

namespace Basics.Main.UI
{
    public class TestDataFactory
    {
        int InputSize;
        Func<double[], double> Classifier;
        double Min;
        double Max;

        public TestDataFactory(int inputSize, Func<double[], double> classifier, double min, double max)
        {
            InputSize = inputSize;
            Classifier = classifier;
            Min = min;
            Max = max;
        }

        public TestData Generate()
        {
            double[] input = new double[InputSize].Fill(Min, Max);

            return new TestData()
            {
                Input = input,
                Output = Classifier(input)
            };
        }

        public IEnumerable<TestData> Generate(int size)
        {
            return Enumerable.Range(0, size).Select(k => Generate());
        }
    }
}
