namespace Labs.Agents
{
    public class AgentState2<TAgent, TState> : AgentState<Environment2<TAgent, TState>, TAgent, TState>
        where TAgent : IAgent<Environment2<TAgent, TState>, TAgent, TState>
        where TState : AgentState2<TAgent, TState>
    {
        public bool IsDestroyed { get; internal set; }
    }
}
