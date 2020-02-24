namespace AI.NeuralNetworks.Functions
{
    public class Relu : IFunction
    {
        public Relu()
        {
        }

        public double Value(double x)
        {
            return x > 0 ? x : 0;
        }

        public double Derivative(double x)
        {
            return x > 0 ? 1 : 0;
        }

        public override string ToString()
        {
            return "Relu";
        }
    }
}
