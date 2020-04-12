namespace Labs.Agents
{
    public class AgentInteraction<TAgent, TAction, TResult>
    {
        public TAgent Agent { get; }
        public TAction Action;
        public TResult Result { get; internal set; }
        internal IEnvironmentField SourceField{ get; set; }
        internal IEnvironmentField TargetField { get; set; }

        public AgentInteraction(TAgent agent)
        {
            Agent = agent;
        }
    }
}
