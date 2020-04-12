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
}
