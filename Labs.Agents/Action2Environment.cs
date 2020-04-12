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
        protected readonly TInteraction[,] interactionByTarget;

        public Action2Environment(Random random, int width, int height) : base(random, width, height)
        {
            interactionByTarget = new TInteraction[width, height];
        }

        public override TInteraction AddAgent(TAgent agent, Point point)
        {
            TInteraction interaction = base.AddAgent(agent, point);
            agent.State.Goal = GetRandomUnusedPosition();
            return interaction;
        }

        public override void Apply(IEnumerable<TInteraction> interactions)
        {
            UpdateInteractionByTarget(interactions);
            UpdateAgentPositions(interactions);
            AssignNewGoals();
        }

        protected virtual void UpdateAgentPositions(IEnumerable<TInteraction> interactions)
        {
            foreach (var interaction in interactions)
            { 
                var agent = interaction.Agent;
                var agentState = interaction.Agent.State;

                if (interaction.Result == InteractionResult.Success)
                {
                    var sourceField = fields[interaction.SourceField.X, interaction.SourceField.Y];
                    var targetField = fields[interaction.TargetField.X, interaction.TargetField.Y];
                    sourceField.Agent = default;
                    targetField.Agent = agent;
                    agentState.Field = targetField;
                }
                else if (interaction.Result == InteractionResult.Collision)
                {
                    agentState.IsDestroyed = true;
                }
            }
        }

        protected void UpdateInteractionByTarget(IEnumerable<TInteraction> interactions)
        {
            foreach (var interaction in interactions)
            {
                var agentState = interaction.Agent.State;
                int sourceX = agentState.Field.X;
                int sourceY = agentState.Field.Y;
                int targetX = agentState.Field.X + interaction.Action.X;
                int targetY = agentState.Field.Y + interaction.Action.Y;
                interaction.SourceField = fields[sourceX, sourceY];
                interaction.TargetField = fields[targetX, targetY];
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    interactionByTarget[x, y] = null;
                }
            }

            foreach (var interaction in interactions)
            {
                var agentState = interaction.Agent.State;

                if (agentState.IsDestroyed)
                {
                    UndoInteraction(interaction.SourceField.X, interaction.SourceField.Y);
                    interaction.Result = InteractionResult.Ignored;
                    interaction.TargetField = interaction.SourceField;
                    interactionByTarget[interaction.SourceField.X, interaction.SourceField.Y] = interaction;
                }
                else
                {
                    if (interaction.TargetField.IsOutside)
                    {
                        UndoInteraction(interaction.SourceField.X, interaction.SourceField.Y);
                        interaction.Result = InteractionResult.Collision;
                        interaction.TargetField = interaction.SourceField;
                        interactionByTarget[interaction.SourceField.X, interaction.SourceField.Y] = interaction;
                    }
                    else
                    {
                        if (interaction.TargetField.IsObstacle == false && interactionByTarget[interaction.TargetField.X, interaction.TargetField.Y] == null)
                        {
                            interaction.Result = InteractionResult.Success;
                            interactionByTarget[interaction.TargetField.X, interaction.TargetField.Y] = interaction;
                        }
                        else
                        {
                            UndoInteraction(interaction.TargetField.X, interaction.TargetField.Y);
                            UndoInteraction(interaction.SourceField.X, interaction.SourceField.Y);
                            interaction.Result = InteractionResult.Collision;
                            interaction.TargetField = interaction.SourceField;
                        }
                    }
                }
            }
        }

        private void UndoInteraction(int x, int y)
        {
            var interaction = interactionByTarget[x, y];

            if (interaction != null)
            {
                int sourceX = interaction.SourceField.X;
                int sourceY = interaction.SourceField.Y;

                if (sourceX != x || sourceY != y)
                {
                    interactionByTarget[x, y] = null;
                    UndoInteraction(sourceX, sourceY);
                    interactionByTarget[sourceX, sourceY] = interaction;
                }
            }
        }

        private void AssignNewGoals()
        {
            foreach (TAgent agent in Agents)
            {
                if (agent.State.IsGoalReached)
                {
                    agent.State.Goal = GetRandomUnusedPosition();
                }
            }
        }
    }
}
