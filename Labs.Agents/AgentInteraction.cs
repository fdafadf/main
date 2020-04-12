namespace Labs.Agents
{
    public class AgentInteraction<TAgent, TAction>
    {
        public TAgent Agent { get; }
        public TAction Action;

        public AgentInteraction(TAgent agent)
        {
            Agent = agent;
        }
    }

    public class MarkovAgentInteraction<TAgent, TAction> : AgentInteraction<TAgent, TAction>
    {
        public double Reward;

        public MarkovAgentInteraction(TAgent agent) : base(agent)
        {
        }
    }
}
