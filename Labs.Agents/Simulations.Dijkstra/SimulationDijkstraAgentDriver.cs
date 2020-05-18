using System;

namespace Labs.Agents.Dijkstra
{
    public class SimulationDijkstraAgentDriver : SimulationAgentDriver<DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent>, DijkstraAgent>
    {
        public SimulationDijkstraAgentDriver() : base(new DijkstraAgentFactory())
        {
        }

        public override void OnInteractionCompleted()
        {
        }

        public override void OnIterationCompleted()
        {
        }

        public override void OnIterationStarted()
        {
            foreach (var agent in Agents)
            {
                agent.CalculateInteraction();
            }
        }
    }
}
