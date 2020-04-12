using System.Collections.Generic;
using Interaction = Labs.Agents.MarkovAgentInteraction<Labs.Agents.NeuralNetworks.Agent, Labs.Agents.Action2>;

namespace Labs.Agents.NeuralNetworks
{
    public class Simulation : Simulation<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState>
    {
        List<Interaction> EnvironmentIteractions = new List<Interaction>();

        public Simulation(MarkovEnvironment2<Agent, AgentState> environment, int numberOfAgents) : base(environment, numberOfAgents)
        {
        }

        protected override void InitializeAgents()
        {
            for (int i = 0; i < Agents.Length; i++)
            {
                var interaction = Environment.AddAgent(Agents[i] = new Agent(), Environment.GetRandomUnusedPosition());
                EnvironmentIteractions.Add(interaction);
            }
        }

        public override void Step()
        {
            foreach (var interaction in EnvironmentIteractions)
            {
                interaction.Action = Action2.MoveNorth;
            }

            Environment.Apply(EnvironmentIteractions);
        }
    }
}
