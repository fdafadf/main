using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using Games.Utilities;

namespace Demos.Forms.Utilities
{
    // TODO: nie w Forms?
    public class ClassifiedNeuralIOGenerator
    {
        int InputSize;
        Func<double[], double> Classifier;
        double Min;
        double Max;

        public ClassifiedNeuralIOGenerator(int inputSize, Func<double[], double> classifier, double min, double max)
        {
            InputSize = inputSize;
            Classifier = classifier;
            Min = min;
            Max = max;
        }

        public NeuralIO<double> Generate()
        {
            double[] input = new double[InputSize].FillWithRandomValues(Min, Max);
            return new NeuralIO<double>(input, Classifier(input));
        }

        public IEnumerable<NeuralIO<double>> Generate(int size)
        {
            return Enumerable.Range(0, size).Select(k => Generate());
        }
    }
}
