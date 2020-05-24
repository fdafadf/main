namespace Labs.Agents
{
    public interface ISimulationPluginFactory : INamed
    {
        SimulationPlugin CreatePlugin();
    }
}
