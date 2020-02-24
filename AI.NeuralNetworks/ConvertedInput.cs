namespace AI.NeuralNetworks
{
    public class ConvertedInput
    {
        public double[] Input { get; }
        public double[] Output { get; }

        public ConvertedInput(double[] input, double[] output)
        {
            Input = input;
            Output = output;
        }
    }
}
