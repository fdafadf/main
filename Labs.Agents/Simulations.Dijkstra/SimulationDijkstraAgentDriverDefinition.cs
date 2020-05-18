namespace Labs.Agents.Dijkstra
{
    public class SimulationDijkstraAgentDriverDefinition : ISimulationAgentDriverDefinition
    {
        public string Name { get; set; } = "Dijkstra";

        // TODO: otypować mocniej
        public SimulationAgentDriver CreateDriver()
        {
            return new SimulationDijkstraAgentDriver();
        }
    }
}
