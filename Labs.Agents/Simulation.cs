using System;
using System.Collections.Generic;

namespace Labs.Agents
{
    public abstract class Simulation<TEnvironment, TAgent, TState> 
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState<TEnvironment, TAgent, TState>
    {
        public Random Random { get; }
        public TEnvironment Environment { get; }

        public Simulation(TEnvironment environment)
        {
            Environment = environment;
            Random = new Random(0);
        }

        public abstract void Step();
    }
}
