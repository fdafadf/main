using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public class Environment2<TAgent, TState> : Action2Environment<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>>
        where TAgent : IAgent<Environment2<TAgent, TState>, TAgent, TState> 
        where TState : AgentState2<Environment2<TAgent, TState>, TAgent, TState>
    {
        public Environment2(int width, int height) : base(width, height)
        {
        }

        protected override EnvironmentField<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>> CreateField(int x, int y)
        {
            return new EnvironmentField<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>>(this, x, y);
        }
    }
}
