namespace Labs.Agents
{
    public class MarkovAgentInteraction<TAgent, TAction> : AgentInteraction<TAgent, TAction>
    {
        public double Reward;

        public MarkovAgentInteraction(TAgent agent) : base(agent)
        {
        }
    }
}
