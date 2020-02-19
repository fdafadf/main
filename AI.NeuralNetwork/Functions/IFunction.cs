namespace AI.NeuralNetwork
{
    public interface IFunction
    {
        double Value(double x);
        double Derivative(double value);
    }
}
