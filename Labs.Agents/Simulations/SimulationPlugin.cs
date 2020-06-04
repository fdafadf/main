using System;
using System.Collections.Generic;

namespace Labs.Agents
{
    public abstract class SimulationPlugin
    {
        public virtual void OnSimulationStarted(ISimulation simulation)
        {
        }

        public virtual void OnSimulationCompleted()
        {
        }

        public virtual void OnSimulationPaused()
        {
        }

        public abstract Type AgentType { get; }

        //public abstract ISimulation CreateSimulation();
    }

    public abstract class SimulationPlugin<TSpace, TAgent> : SimulationPlugin
    {
        //public List<TAgent> Agents = new List<TAgent>();
        IAgentFactory<TSpace, TAgent> AgentFactory;

        public SimulationPlugin(IAgentFactory<TSpace, TAgent> agentFactory)
        {
            AgentFactory = agentFactory;
        }

        public TAgent CreateAgent(TSpace space, int x, int y)
        {
            TAgent agent = AgentFactory.CreateAgent(space, x, y);
            //Agents.Add(agent);
            return agent;
        }

        public override Type AgentType => typeof(TAgent);

        public virtual void OnInteractionCompleted(IEnumerable<TAgent> agents)
        {
        }

        public virtual void OnIterationCompleted(IEnumerable<TAgent> agents)
        {
        }

        public virtual void OnIterationStarted(IEnumerable<TAgent> agents)
        {
        }
    }
}
