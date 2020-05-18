namespace Labs.Agents
{
    public interface ISimulationPluginFactory
    {
        string Name { get; set; }
        SimulationPlugin CreatePlugin();
    }
}
