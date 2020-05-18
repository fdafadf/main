namespace Labs.Agents
{
    public interface IAgentFactory<TSpace, TAgent>
    {
        TAgent CreateAgent(TSpace space, int x, int y);
    }
}
