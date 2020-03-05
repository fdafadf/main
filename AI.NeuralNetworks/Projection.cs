namespace AI.NeuralNetworks
{
    public class Projection
    {
        public double[] Input { get; }
        public double[] Output { get; }

        public Projection(double[] input, double[] output)
        {
            Input = input;
            Output = output;
        }
    }
}
