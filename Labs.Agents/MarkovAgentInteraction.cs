namespace Labs.Agents
{
    public class MarkovAgentInteraction<TAgent, TAction, TResult> : AgentInteraction<TAgent, TAction, TResult>
    {
        public double Reward;

        public MarkovAgentInteraction(TAgent agent) : base(agent)
        {
        }
    }
}
