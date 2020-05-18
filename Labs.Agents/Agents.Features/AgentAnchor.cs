namespace Labs.Agents
{
    public class AgentAnchor<TAgent>
    {
        public TAgent Agent { get; }
        public ISpaceField Field { get; internal set; }

        public AgentAnchor(TAgent agent, ISpaceField field)
        {
            Agent = agent;
            Field = field;
        }
    }
}
