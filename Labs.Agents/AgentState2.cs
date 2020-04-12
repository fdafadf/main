namespace Labs.Agents
{
    public class AgentState2<TEnvironment, TAgent, TState> : AgentState<TEnvironment, TAgent, TState>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState2<TEnvironment, TAgent, TState>
    {
        public bool IsDestroyed { get; internal set; }
    }
}
