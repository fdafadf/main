namespace AI.NeuralNetworks.Functions
{
    public class LeakyRelu : IFunction
    {
        public LeakyRelu()
        {
        }

        public double Value(double x)
        {
            return x > 0 ? x : (x * 0.01);
        }

        public double Derivative(double x)
        {
            return x > 0 ? 1 : 0.01;
        }

        public override string ToString()
        {
            return "Leaky";
        }
    }
}
