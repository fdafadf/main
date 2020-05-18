namespace Labs.Agents
{
    public interface ISimulationAgentDriverDefinition
    {
        string Name { get; set; }
        SimulationAgentDriver CreateDriver();
    }
}
