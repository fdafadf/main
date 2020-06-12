namespace Labs.Agents
{
    public interface IAnchoredAgent<TAgent>
    {
        AgentSpaceAnchor<TAgent> Anchor { get; }
    }
}
