namespace Labs.Agents
{
    public class AgentIteraction<TAgent, TAction>
    {
        public TAgent Agent { get; }
        public TAction Action;

        public AgentIteraction(TAgent agent)
        {
            Agent = agent;
        }
    }
}
