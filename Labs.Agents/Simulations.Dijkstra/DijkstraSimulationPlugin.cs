namespace Labs.Agents.Simulations.Dijkstra
{
    public class DijkstraSimulationPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent>, DijkstraAgent>
    {
        public DijkstraSimulationPlugin() : base(new DijkstraAgentFactory())
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

        public override void OnSimulationCompleted()
        {
        }

        public override void OnSimulationPaused()
        {
        }
    }
}
