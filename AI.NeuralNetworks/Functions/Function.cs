namespace AI.NeuralNetworks
{
    public static class Function
    {
        public static readonly IFunction ReLU = new Functions.Relu();
        public static readonly IFunction LeakyReLU = new Functions.LeakyRelu();
        public static readonly IFunction Sigmoidal = new Functions.Sigmoidal();
    }
}
