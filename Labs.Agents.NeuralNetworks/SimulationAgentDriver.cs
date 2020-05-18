namespace Labs.Agents.NeuralNetworks
{
    public abstract class SimulationAgentDriver
    {
        public abstract void OnInteractionCompleted();
        public abstract void OnIterationCompleted();
        public abstract void OnIterationStarted();
    }

    public abstract class SimulationAgentDriver<TAgent> : SimulationAgentDriver
         where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructible, IGoalAgent
    {
        public abstract TAgent CreateAgent(DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent> space, int x, int y);
    }
}
