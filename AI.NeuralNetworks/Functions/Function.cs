using System;

namespace AI.NeuralNetworks
{
    public static class Function
    {
        public static readonly IFunction ReLU = new Functions.Relu();
        public static readonly IFunction LeakyReLU = new Functions.LeakyRelu();
        public static readonly IFunction Sigmoidal = new Functions.Sigmoidal();

        public static IFunction Get(FunctionName name)
        {
            switch (name)
            {
                case FunctionName.ReLU:
                    return ReLU;
                case FunctionName.LeakyReLU:
                    return LeakyReLU;
                case FunctionName.Sigmoidal:
                    return Sigmoidal;
                default:
                    throw new NotSupportedException();
            }
        }
    }

    public enum FunctionName
    {
        ReLU,
        LeakyReLU,
        Sigmoidal
    }
}
