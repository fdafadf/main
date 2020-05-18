using System;

namespace Labs.Agents.Simulations.AStar
{
    public class AStarSimulationPluginFactory : ISimulationPluginFactory
    {
        public string Name { get; set; } = "A-Star";

        public SimulationPlugin CreatePlugin()
        {
            return new AStarSimulationPlugin();
        }
    }
}
