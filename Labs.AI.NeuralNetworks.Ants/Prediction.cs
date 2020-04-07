namespace Labs.AI.NeuralNetworks.Ants
{
    public class Prediction
    {
        public AgentAction BestAction;
        public double BestValue;
        public double[] Input;

        public Prediction(AgentAction bestAction, double bestValue, double[] input)
        {
            BestAction = bestAction;
            BestValue = bestValue;
            Input = input;
        }
    }
}
