namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetworkPrediction
    {
        public readonly CardinalMovement Action;
        public readonly double Value;
        public readonly double[] Input;

        public AgentNetworkPrediction(CardinalMovement action, double value, double[] input)
        {
            Action = action;
            Value = value;
            Input = input;
        }

        public override string ToString()
        {
            return $"{Action} {Value}";
        }
    }
}
