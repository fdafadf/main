namespace Labs.Agents
{
    public class AgentSpaceAnchor<TAgent> : SpaceAnchor
    {
        public TAgent Agent { get; }

        public AgentSpaceAnchor(TAgent agent, ISpaceField field) : base(field)
        {
            Agent = agent;
        }
    }

    public class SpaceAnchor
    {
        public ISpaceField Field { get; internal set; }

        public SpaceAnchor(ISpaceField field)
        {
            Field = field;
        }
    }
}
