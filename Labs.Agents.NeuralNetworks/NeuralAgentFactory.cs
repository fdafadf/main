namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentFactory : IAgentFactory<DestructibleInteractiveSpace<CardinalMovementSpace<NeuralAgent>, NeuralAgent>, NeuralAgent>
    {
        public NeuralAgent CreateAgent(DestructibleInteractiveSpace<CardinalMovementSpace<NeuralAgent>, NeuralAgent> space, int x, int y)
        {
            return new NeuralAgent(space, x, y);
        }
    }
}
