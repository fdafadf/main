namespace Labs.Agents.Simulations.Dijkstra
{
    public class DijkstraSimulationPluginFactory : ISimulationPluginFactory
    {
        public string Name { get; set; } = "Dijkstra";

        // TODO: otypować mocniej
        public SimulationPlugin CreatePlugin()
        {
            return new DijkstraSimulationPlugin();
        }
    }
}
