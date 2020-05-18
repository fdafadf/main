namespace Labs.Agents.Simulations.AStar
{
    public class AStarAgentFactory : IAgentFactory<DestructibleInteractiveSpace<CardinalMovementSpace<AStarAgent>, AStarAgent>, AStarAgent>
    {
        public AStarAgent CreateAgent(DestructibleInteractiveSpace<CardinalMovementSpace<AStarAgent>, AStarAgent> space, int x, int y)
        {
            return new AStarAgent(space, x, y);
        }
    }
}
