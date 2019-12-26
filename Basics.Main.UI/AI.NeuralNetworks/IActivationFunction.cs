namespace Basics.AI.NeuralNetworks
{
    public interface IActivationFunction
    {
        double DerivativeValue(double input);
        double Value(double input);
    }
}
