using System;
using System.Collections.Generic;

namespace Labs.Agents
{
    public class MarkovEnvironment2<TAgent, TState> : Action2Environment<MarkovEnvironment2<TAgent, TState>, TAgent, TState, MarkovAgentInteraction<TAgent, Action2>>
        where TAgent : IAgent<MarkovEnvironment2<TAgent, TState>, TAgent, TState>
        where TState : AgentState2<MarkovEnvironment2<TAgent, TState>, TAgent, TState>
    {
        public MarkovEnvironment2(Random random, int width, int height) : base(random, width, height)
        {
        }

        public override void Apply(IEnumerable<MarkovAgentInteraction<TAgent, Action2>> interactions)
        {
            base.Apply(interactions);
            CalculateRewards(interactions);
        }

        protected override EnvironmentField<MarkovEnvironment2<TAgent, TState>, TAgent, TState, MarkovAgentInteraction<TAgent, Action2>> CreateField(int x, int y)
        {
            return new EnvironmentField<MarkovEnvironment2<TAgent, TState>, TAgent, TState, MarkovAgentInteraction<TAgent, Action2>>(this, x, y);
        }

        private void CalculateRewards(IEnumerable<MarkovAgentInteraction<TAgent, Action2>> interactions)
        {
            foreach (var interaction in interactions)
            {
                interaction.Reward = 0;
            }
        }
    }
}
