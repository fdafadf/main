using System;
using System.Collections.Generic;

namespace Labs.Agents
{
    public abstract class SimulationAgentDriver
    {
        public abstract void OnInteractionCompleted();
        public abstract void OnIterationCompleted();
        public abstract void OnIterationStarted();
        public abstract Type AgentType { get; }
    }

    public abstract class SimulationAgentDriver<TSpace, TAgent> : SimulationAgentDriver
    {
        public List<TAgent> Agents = new List<TAgent>();
        IAgentFactory<TSpace, TAgent> AgentFactory;

        public SimulationAgentDriver(IAgentFactory<TSpace, TAgent> agentFactory)
        {
            AgentFactory = agentFactory;
        }

        public TAgent CreateAgent(TSpace space, int x, int y)
        {
            TAgent agent = AgentFactory.CreateAgent(space, x, y);
            Agents.Add(agent);
            return agent;
        }

        public override Type AgentType => typeof(TAgent);
    }
}
