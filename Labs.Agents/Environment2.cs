using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public enum ActionResult
    {
        Success,
        Collision
    }

    public class Environment2<TAgent, TState> : Environment<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>>
        where TAgent : IAgent<Environment2<TAgent, TState>, TAgent, TState> 
        where TState : AgentState2<TAgent, TState>
    {
        public Environment2(int width, int height) : base(width, height)
        {
        }

        public override void Apply(IEnumerable<AgentInteraction<TAgent, Action2>> interactions)
        {
            AssignInteractionsToFields(interactions);
            UpdateFieldsFromAssignedInteractions();
        }

        protected override EnvironmentField<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>> CreateField(int x, int y)
        {
            return new EnvironmentField<Environment2<TAgent, TState>, TAgent, TState, AgentInteraction<TAgent, Action2>>(this, x, y);
        }

        private void UpdateFieldsFromAssignedInteractions()
        {
            for (int x = 0; x < fields.GetLength(0); x++)
            {
                for (int y = 0; y < fields.GetLength(1); y++)
                {
                    var targetField = fields[x, y];
                    var interaction = targetField.Interaction;

                    if (interaction != null)
                    {
                        var agent = interaction.Agent;

                        if (targetField.InteractionResult == InteractionResult.Success)
                        {
                            var sourceField = fields[agent.State.Field.X, agent.State.Field.Y];
                            sourceField.Agent = default;
                            targetField.Agent = agent;
                            agent.State.Field = targetField;
                        }
                        else if (targetField.InteractionResult == InteractionResult.Collision)
                        {
                            agent.State.IsDestroyed = true;
                        }
                    }
                }
            }
        }

        private void AssignInteractionsToFields(IEnumerable<AgentInteraction<TAgent, Action2>> interactions)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    fields[x, y].Interaction = default;
                }
            }

            foreach (var interaction in interactions)
            {
                var agentState = interaction.Agent.State;
                int sourceX = agentState.Field.X;
                int sourceY = agentState.Field.Y;
                var sourceField = fields[sourceX, sourceY];

                if (agentState.IsDestroyed)
                {
                    sourceField.Interaction = interaction;
                    sourceField.InteractionResult = InteractionResult.Ignored;
                    UndoAssignedInteraction(sourceX, sourceY);
                }
                else
                {
                    int targetX = agentState.Field.X + interaction.Action.X;
                    int targetY = agentState.Field.Y + interaction.Action.Y;
                    var targetField = fields[targetX, targetY];

                    if (fields.IsOutside(targetX, targetY))
                    {
                        sourceField.Interaction = interaction;
                        sourceField.InteractionResult = InteractionResult.Collision;
                        UndoAssignedInteraction(sourceX, sourceY);
                    }
                    else if (targetField.IsObstacle == false && targetField.Interaction == null)
                    {
                        targetField.Interaction = interaction;
                        targetField.InteractionResult = InteractionResult.Success;
                    }
                    else
                    {
                        sourceField.Interaction = interaction;
                        sourceField.InteractionResult = InteractionResult.Collision;
                        UndoAssignedInteraction(targetX, targetY);
                        UndoAssignedInteraction(sourceX, sourceY);
                    }
                }
            }
        }

        private void UndoAssignedInteraction(int x, int y)
        {
            var field = fields[x, y];
            var interaction = field.Interaction;

            if (interaction != null)
            {
                var agent = interaction.Agent;
                //agent.State.IsDestroyed = true;
                int sourceX = agent.State.Field.X;
                int sourceY = agent.State.Field.Y;

                if (sourceX != x || sourceY != y)
                {
                    var sourceField = fields[sourceX, sourceY];
                    field.Interaction = null;
                    //field.Agent = default;
                    UndoAssignedInteraction(sourceX, sourceY);
                    //sourceField.Agent = agent;
                    sourceField.Interaction = interaction;
                }
            }
        }
    }
}
