using System;
using System.Collections.Generic;

namespace Labs.Agents.Demo
{
    public class DemoSimulation : Simulation<Environment2<DemoAgent, DemoAgentState>, DemoAgent, DemoAgentState>
    {
        List<AgentInteraction<DemoAgent, Action2>> EnvironmentIteractions = new List<AgentInteraction<DemoAgent, Action2>>();

        public DemoSimulation(Environment2<DemoAgent, DemoAgentState> environment, int numberOfAgents) : base(environment, numberOfAgents)
        {
        }

        protected override void InitializeAgents()
        {
            for (int i = 0; i < Agents.Length; i++)
            {
                var interaction = Environment.AddAgent(Agents[i] = new DemoAgent(this), Environment.GetRandomUnusedPosition());
                EnvironmentIteractions.Add(interaction);
            }
        }

        public override void Step()
        {
            foreach (var iteraction in EnvironmentIteractions)
            {
                iteraction.Action = iteraction.Agent.GetAction();
            }

            Environment.Apply(EnvironmentIteractions);
        }
    }
}
