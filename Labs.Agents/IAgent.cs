namespace Labs.Agents
{
    public interface IAgent<TEnvironment, TAgent, TState>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        TState State { get; }
    }
}
