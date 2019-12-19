namespace Basics.AI.NeuralNetworks
{
    public class NeuralIO
    {
        public readonly double[] Input;
        public readonly double Output;

        public NeuralIO(double[] input, double output)
        {
            Input = input;
            Output = output;
        }
    }
}
