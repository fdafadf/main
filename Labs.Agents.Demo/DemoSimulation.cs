using System;
using System.Collections.Generic;

namespace Labs.Agents.Demo
{
    public class DemoSimulation : Simulation<Environment2<DemoAgent, DemoAgentState>, DemoAgent, DemoAgentState>
    {
        List<AgentInteraction<DemoAgent, Action2, InteractionResult>> EnvironmentIteractions = new List<AgentInteraction<DemoAgent, Action2, InteractionResult>>();

        public DemoSimulation(Environment2<DemoAgent, DemoAgentState> environment, int numberOfAgents) : base(environment)
        {
            for (int i = 0; i < numberOfAgents; i++)
            {
                var interaction = Environment.AddAgent(new DemoAgent(this), Environment.GetRandomUnusedPosition());
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
