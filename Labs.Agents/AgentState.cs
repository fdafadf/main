using System.Drawing;
using System.Numerics;

namespace Labs.Agents
{
    public abstract class AgentState<TEnvironment, TAgent, TState>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public IEnvironmentField<TEnvironment, TAgent, TState> Field { get; internal set; }
    }
}
