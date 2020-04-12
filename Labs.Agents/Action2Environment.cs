using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents
{
    public abstract class Action2Environment<TEnvironment, TAgent, TState, TInteraction> : Environment<TEnvironment, TAgent, TState, TInteraction>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState2<TEnvironment, TAgent, TState>
        where TInteraction : AgentInteraction<TAgent, Action2, InteractionResult>
    {
        public Action2Environment(Random random, int width, int height) : base(random, width, height)
        {
        }

        public override TInteraction AddAgent(TAgent agent, Point point)
        {
            TInteraction interaction = base.AddAgent(agent, point);
            agent.State.Goal = GetRandomUnusedPosition();
            return interaction;
        }

        public override void Apply(IEnumerable<TInteraction> interactions)
        {
            AssignInteractionsToFields(interactions);
            UpdateFieldsFromAssignedInteractions();
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

                        if (targetField.Interaction.Result == InteractionResult.Success)
                        {
                            var sourceField = fields[agent.State.Field.X, agent.State.Field.Y];
                            sourceField.Agent = default;
                            targetField.Agent = agent;
                            agent.State.Field = targetField;
                        }
                        else if (targetField.Interaction.Result == InteractionResult.Collision)
                        {
                            agent.State.IsDestroyed = true;
                        }
                    }
                }
            }
        }

        private void AssignInteractionsToFields(IEnumerable<TInteraction> interactions)
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
                    sourceField.Interaction.Result = InteractionResult.Ignored;
                    UndoAssignedInteraction(sourceX, sourceY);
                }
                else
                {
                    int targetX = agentState.Field.X + interaction.Action.X;
                    int targetY = agentState.Field.Y + interaction.Action.Y;

                    if (fields.IsOutside(targetX, targetY))
                    {
                        sourceField.Interaction = interaction;
                        sourceField.Interaction.Result = InteractionResult.Collision;
                        UndoAssignedInteraction(sourceX, sourceY);
                    }
                    else
                    {
                        var targetField = fields[targetX, targetY];

                        if (targetField.IsObstacle == false && targetField.Interaction == null)
                        {
                            targetField.Interaction = interaction;
                            targetField.Interaction.Result = InteractionResult.Success;
                        }
                        else
                        {
                            sourceField.Interaction = interaction;
                            sourceField.Interaction.Result = InteractionResult.Collision;
                            UndoAssignedInteraction(targetX, targetY);
                            UndoAssignedInteraction(sourceX, sourceY);
                        }
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
                int sourceX = agent.State.Field.X;
                int sourceY = agent.State.Field.Y;

                if (sourceX != x || sourceY != y)
                {
                    var sourceField = fields[sourceX, sourceY];
                    field.Interaction = null;
                    UndoAssignedInteraction(sourceX, sourceY);
                    sourceField.Interaction = interaction;
                }
            }
        }
    }
}
