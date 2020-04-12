using System;

namespace Labs.Agents.NeuralNetworks
{
    public class Agent : IAgent<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState>
    {
        public AgentState State => throw new NotImplementedException();
    }
}
