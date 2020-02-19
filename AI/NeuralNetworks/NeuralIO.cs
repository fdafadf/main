using Games.Utilities;

namespace AI.NeuralNetworks
{
    public class NeuralIO<TOutput>
    {
        public double[] Input { get; }
        public TOutput Output { get; }

        //public NeuralIO(double[] input, double output)
        //{
        //    Input = input;
        //    Output = new[] { output };
        //}

        public NeuralIO(double[] input, TOutput output)
        {
            Input = input;
            Output = output;
        }

        //public NeuralIO(double[] input, int[] output)
        //{
        //    Input = input;
        //    Output = output.ToDouble();
        //}
    }
}
