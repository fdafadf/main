namespace Labs.AI.NeuralNetworks.Ants
{
    public class Prediction<TInput>
    {
        public AgentAction BestAction;
        public double BestValue;
        public TInput Input;

        public Prediction(AgentAction bestAction, double bestValue, TInput input)
        {
            BestAction = bestAction;
            BestValue = bestValue;
            Input = input;
        }
    }
}
