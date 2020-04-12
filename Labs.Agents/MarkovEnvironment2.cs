using System;
using System.Collections.Generic;

namespace Labs.Agents
{
    public class MarkovEnvironment2<TAgent, TState> : Action2Environment<MarkovEnvironment2<TAgent, TState>, TAgent, TState, MarkovAgentInteraction<TAgent, Action2, InteractionResult>>
        where TAgent : IAgent<MarkovEnvironment2<TAgent, TState>, TAgent, TState>
        where TState : AgentState2<MarkovEnvironment2<TAgent, TState>, TAgent, TState>
    {
        public MarkovEnvironment2(Random random, int width, int height) : base(random, width, height)
        {
        }

        protected override void UpdateAgentPositions(IEnumerable<MarkovAgentInteraction<TAgent, Action2, InteractionResult>> interactions)
        {
            CalculateRewards(interactions);
            base.UpdateAgentPositions(interactions);
        }

        protected override MarkovAgentInteraction<TAgent, Action2, InteractionResult> CreateInteraction(TAgent agent)
        {
            return new MarkovAgentInteraction<TAgent, Action2, InteractionResult>(agent);
        }

        protected override EnvironmentField<MarkovEnvironment2<TAgent, TState>, TAgent, TState> CreateField(int x, int y)
        {
            return new EnvironmentField<MarkovEnvironment2<TAgent, TState>, TAgent, TState>(this, x, y);
        }

        private void CalculateRewards(IEnumerable<MarkovAgentInteraction<TAgent, Action2, InteractionResult>> interactions)
        {
            foreach (var interaction in interactions)
            {
                if (interaction.Result == InteractionResult.Ignored)
                {
                    interaction.Reward = 0;
                }
                else
                {
                    if (interaction.Result == InteractionResult.Collision)
                    {
                        interaction.Reward = -2.0;
                    }
                    else if (interaction.Action.Index == Action2.Nothing.Index)
                    {
                        interaction.Reward = -0.1;
                    }
                    else
                    {
                        double previousDistance = interaction.SourceField.Distance(interaction.Agent.State.Goal);
                        double distance = interaction.TargetField.Distance(interaction.Agent.State.Goal);
                        interaction.Reward = previousDistance - distance;
                    }
                }
            }
        }
    }
}
