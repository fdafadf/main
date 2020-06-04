using System.Collections;
using System.Collections.Generic;

namespace Labs.Agents.Simulations.Dijkstra
{
    public class DijkstraSimulationPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent>, DijkstraAgent>
    {
        public DijkstraSimulationPlugin() : base(new DijkstraAgentFactory())
        {
        }

        public override void OnIterationStarted(IEnumerable<DijkstraAgent> agents)
        {
            foreach (var agent in agents)
            {
                agent.CalculateInteraction();
            }
        }
    }
}
