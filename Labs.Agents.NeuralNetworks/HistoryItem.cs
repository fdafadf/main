namespace Labs.Agents.NeuralNetworks
{
    public class HistoryItem
    {
        public readonly AgentNetworkInput Input;
        public readonly AgentNetworkInput State;
        public readonly double Reward;

        public HistoryItem(AgentNetworkInput input, AgentNetworkInput state, double reward)
        {
            Input = input;
            State = state;
            Reward = reward;
        }
    }
}
