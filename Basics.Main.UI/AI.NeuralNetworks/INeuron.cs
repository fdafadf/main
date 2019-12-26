namespace Basics.AI.NeuralNetworks
{
    public interface INeuron
    {
        double[] Weights { get; }
        double Evaluate(double[] input);
    }
}
