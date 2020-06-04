using System.Collections.Generic;

namespace Labs.Agents.Simulations.AStar
{
    public class AStarSimulationPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<AStarAgent>, AStarAgent>, AStarAgent>
    {
        public AStarSimulationPlugin() : base(new AStarAgentFactory())
        {
        }

        public override void OnIterationStarted(IEnumerable<AStarAgent> agents)
        {
            foreach (var agent in agents)
            {
                agent.CalculateInteraction();
            }
        }
    }
}
