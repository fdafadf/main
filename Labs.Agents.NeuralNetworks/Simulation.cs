using System.Collections.Generic;

namespace Labs.Agents.NeuralNetworks
{
    public class Simulation : Simulation<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState>
    {
        List<MarkovAgentInteraction<Agent, Action2>> EnvironmentIteractions = new List<MarkovAgentInteraction<Agent, Action2>>();

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
            Environment.Apply(EnvironmentIteractions);
        }
    }
}
