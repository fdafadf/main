using System;

namespace AI.NeuralNetwork.Functions
{
    public class Sigmoidal : IFunction
    {
        public Sigmoidal()
        {
        }

        public double Value(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-1.0 * x));
        }

        public double Derivative(double value)
        {
            return value * (1 - value);
        }

        public override string ToString()
        {
            return "Sigm";
        }
    }
}
