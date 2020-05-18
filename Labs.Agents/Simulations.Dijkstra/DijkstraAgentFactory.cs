namespace Labs.Agents.Dijkstra
{
    public class DijkstraAgentFactory : IAgentFactory<DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent>, DijkstraAgent>
    {
        public DijkstraAgent CreateAgent(DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent> space, int x, int y)
        {
            return new DijkstraAgent(space, x, y);
        }
    }
}
