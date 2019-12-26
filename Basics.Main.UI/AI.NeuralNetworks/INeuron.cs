namespace Basics.AI.NeuralNetworks
{
    public interface INeuron
    {
        double[] Weights { get; }
        //double Sum(double[] input);
        double Evaluate(double[] input);
        //double Evaluate(double[] input, out double outputDerivative);
    }
}
