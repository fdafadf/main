namespace Labs.Agents.NeuralNetworks
{
    public class Prediction
    {
        public readonly Action2 Action;
        public readonly double Value;
        public readonly AgentNetworkInput Input;

        public Prediction(Action2 action, double value, AgentNetworkInput input)
        {
            Action = action;
            Value = value;
            Input = input;
        }
    }
}
