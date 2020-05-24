namespace Labs.Agents.Simulations.AStar
{
    public class AStarSimulationPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<AStarAgent>, AStarAgent>, AStarAgent>
    {
        public AStarSimulationPlugin() : base(new AStarAgentFactory())
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
