using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents
{
    public class Environment1<TAgent, TState> : Environment<Environment1<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action1, InteractionResult>>
        where TAgent : IAgent<Environment1<TAgent, TState>, TAgent, TState>
        where TState : AgentState1<TAgent, TState>
    {
        public Environment1(Random random, int width, int height) : base(random, width, height)
        {
        }

        public override void Apply(IEnumerable<AgentInteraction<TAgent, Action1, InteractionResult>> iteractions)
        {
            foreach (var iteraction in iteractions)
            {
                var state = iteraction.Agent.State;

                switch (iteraction.Action)
                {
                    case Action1.MoveForward:
                        //state.X += state.Direction.X;
                        //state.Y += state.Direction.Y;
                        break;
                    case Action1.RotateLeft:
                        state.Direction = state.Direction.Left;
                        break;
                    case Action1.RotateRight:
                        state.Direction = state.Direction.Right;
                        break;
                }
            }
        }

        protected override AgentInteraction<TAgent, Action1, InteractionResult> CreateInteraction(TAgent agent)
        {
            throw new NotImplementedException();
        }

        protected override EnvironmentField<Environment1<TAgent, TState>, TAgent, TState> CreateField(int x, int y)
        {
            return new EnvironmentField<Environment1<TAgent, TState>, TAgent, TState>(this, x, y);
        }
    }
}
