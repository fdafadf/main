namespace Labs.Agents
{
    public interface IAnchoredAgent<TAgent>
    {
        AgentAnchor<TAgent> Anchor { get; }
    }
}
