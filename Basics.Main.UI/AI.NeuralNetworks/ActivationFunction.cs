using System;
using System.ComponentModel;

namespace Basics.AI.NeuralNetworks
{
    public class ActivationFunctions
    {
        public static readonly BinaryStepFunction BinaryStep = new BinaryStepFunction(0, -1, 1);
        public static readonly HiperbolicTangensFunction HiperbolicTangens = new HiperbolicTangensFunction();
        public static readonly SigmoidFunction Sigmoid = new SigmoidFunction();

        public class BinaryStepFunction : IActivationFunction
        {
            double threshold;
            double outputBeforeThreshold;
            double outputAfterThreshold;

            public BinaryStepFunction(double threshold, double outputBeforeThreshold, double outputAfterThreshold)
            {
                this.threshold = threshold;
                this.outputBeforeThreshold = outputBeforeThreshold;
                this.outputAfterThreshold = outputAfterThreshold;
            }

            public double DerivativeValue(double input)
            {
                return 1.0;
            }

            public double Value(double input)
            {
                return input < threshold ? outputBeforeThreshold : outputAfterThreshold;
            }
        }

        public class SigmoidFunction : IActivationFunction
        {
            public double DerivativeValue(double x)
            {
                return x * (1.0 - x);
            }

            public double Value(double x)
            {
                return 1.0 / (1.0 + Math.Exp(-x));
            }
        }

        public class HiperbolicTangensFunction : IActivationFunction
        {
            public double DerivativeValue(double input)
            {
                return 1.0 - Math.Pow(Math.Tanh(input), 2);
            }

            public double Value(double input)
            {
                return Math.Tanh(input);
            }
        }
    }
}
