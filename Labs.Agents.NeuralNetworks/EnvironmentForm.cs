using Interaction = Labs.Agents.MarkovAgentInteraction<Labs.Agents.NeuralNetworks.Agent, Labs.Agents.Action2>;

namespace Labs.Agents.NeuralNetworks
{
    public class EnvironmentForm : Action2EnvironmentForm<Simulation, MarkovEnvironment2<Agent, AgentState>, Agent, AgentState, Interaction>
    {
        public EnvironmentForm(Simulation simulation) : base(simulation)
        {
        }
    }
}
