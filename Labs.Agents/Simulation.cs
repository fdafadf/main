using System;

namespace Labs.Agents
{
    public abstract class Simulation<TEnvironment, TAgent, TState> 
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public Random Random { get; }
        public TEnvironment Environment { get; }
        public TAgent[] Agents { get; }

        public Simulation(TEnvironment environment, int numberOfAgents)
        {
            Environment = environment;
            Random = new Random(0);
            Agents = new TAgent[numberOfAgents];
            InitializeAgents();
        }

        public abstract void Step();
        protected abstract void InitializeAgents();
    }
}
