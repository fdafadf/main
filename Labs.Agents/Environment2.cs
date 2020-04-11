using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public class Environment2<TAgent, TState> : Environment<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>>
        where TAgent : IAgent<Environment2<TAgent, TState>, TAgent, TState> 
        where TState : AgentState2<TAgent, TState>
    {
        public Environment2(int width, int height) : base(width, height)
        {
        }

        public override void Apply(IEnumerable<AgentInteraction<TAgent, Action2>> iteractions)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    fields[x, y].Agent = default;
                }
            }

            foreach (var iteraction in iteractions)
            {
                var agent = iteraction.Agent;
                var agentState = iteraction.Agent.State;
                int sourceX = agentState.Field.X;
                int sourceY = agentState.Field.Y;
                int targetX = agentState.Field.X + iteraction.Action.X;
                int targetY = agentState.Field.Y + iteraction.Action.Y;
                var targetField = fields[targetX, targetY];

                if (agentState.IsDestroyed)
                {
                    UndoIteraction(sourceX, sourceY);
                    fields[sourceX, sourceY].Agent = agent;
                }
                else
                {
                    if (fields.IsOutside(targetX, targetY))
                    {
                        UndoIteraction(sourceX, sourceY);
                        fields[sourceX, sourceY].Agent = agent;
                        agent.State.IsDestroyed = true;
                    }
                    else if (targetField.IsAgent == false && targetField.IsObstacle == false)
                    {
                        targetField.Agent = agent;
                    }
                    else
                    {
                        agent.State.IsDestroyed = true;
                        UndoIteraction(targetX, targetY);
                        UndoIteraction(sourceX, sourceY);
                        fields[sourceX, sourceY].Agent = agent;
                    }
                }
            }

            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    var field = fields[x, y];
                    var agent = field.Agent;
            
                    if (agent != null)
                    {
                        agent.State.Field = field;
                    }
                }
            }
        }

        protected override EnvironmentField<Environment2<TAgent, TState>, TAgent, TState> CreateField(int x, int y)
        {
            return new EnvironmentField<Environment2<TAgent, TState>, TAgent, TState>(this, x, y);
        }

        private void UndoIteraction(int x, int y)
        {
            TAgent agent = fields[x, y].Agent;

            if (agent != null)
            {
                agent.State.IsDestroyed = true;
                int sourceX = agent.State.Field.X;
                int sourceY = agent.State.Field.Y;

                if (sourceX != x || sourceY != y)
                {
                    UndoIteraction(sourceX, sourceY);
                    fields[x, y].Agent = default;
                    fields[sourceX, sourceY].Agent = agent;
                }
            }
        }
    }
}
