namespace Labs.Agents
{
    public interface IEnvironmentField<TEnvironment, TAgent, TState> : IEnvironmentField
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        TEnvironment Environment { get; }
        TAgent Agent { get; }
    }

    public interface IEnvironmentField : IPoint
    {
        bool IsOutside { get; }
        bool IsEmpty { get; }
        bool IsObstacle { get; }
        bool IsAgent { get; }
    }
}
