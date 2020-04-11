namespace Labs.Agents
{
    public class AgentState1<TAgent, TState> : AgentState<Environment1<TAgent, TState>, TAgent, TState>
        where TAgent : IAgent<Environment1<TAgent, TState>, TAgent, TState>
        where TState : AgentState1<TAgent, TState>
    {
        public CardinalPoint Direction { get; internal set; }

        public AgentState1() : base()
        {
            Direction = CardinalPoint.North;
        }
    }
}
