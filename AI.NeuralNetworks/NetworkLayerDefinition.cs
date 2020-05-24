namespace AI.NeuralNetworks
{
    public class NetworkLayerDefinition
    {
        public FunctionName ActivationFunction { get; set; }
        public int Size { get; set; }

        public NetworkLayerDefinition()
        {
            ActivationFunction = FunctionName.ReLU;
            Size = 16;
        }

        public NetworkLayerDefinition(FunctionName activationFunction, int size)
        {
            ActivationFunction = activationFunction;
            Size = size;
        }

        public override string ToString()
        {
            return $"{ActivationFunction},{Size}";
        }
    }
}
