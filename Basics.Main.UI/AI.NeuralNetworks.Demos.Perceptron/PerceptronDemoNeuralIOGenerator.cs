using Basics.AI.NeuralNetworks;
using Basics.Main.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basics.AI.NeuralNetworks.Demos.Perceptron
{
    public class PointsNeuralIOGenerator
    {
        int InputSize;
        Func<double[], double> Classifier;
        double Min;
        double Max;

        public PointsNeuralIOGenerator(int inputSize, Func<double[], double> classifier, double min, double max)
        {
            InputSize = inputSize;
            Classifier = classifier;
            Min = min;
            Max = max;
        }

        public NeuralIO Generate()
        {
            double[] input = new double[InputSize].FillWithRandomValues(Min, Max);
            return new NeuralIO(input, Classifier(input));
        }

        public IEnumerable<NeuralIO> Generate(int size)
        {
            return Enumerable.Range(0, size).Select(k => Generate());
        }
    }
}
