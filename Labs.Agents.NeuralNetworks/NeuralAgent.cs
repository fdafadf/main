using System.Drawing;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgent : IAnchoredAgent<NeuralAgent>, IDestructibleAgent, IInteractiveAgent<CardinalMovement, InteractionResult>, IGoalAgent
    {
        public AgentAnchor<NeuralAgent> Anchor { get; }
        public Interaction<CardinalMovement, InteractionResult> Interaction { get; }
        public AgentFitness Fitness { get; }
        public AgentGoal Goal { get; }
        public double[] NetworkLastInput;

        public NeuralAgent(DestructibleInteractiveSpace<CardinalMovementSpace<NeuralAgent>, NeuralAgent> space, Point p) : this(space, p.X, p.Y)
        {
        }

        public NeuralAgent(DestructibleInteractiveSpace<CardinalMovementSpace<NeuralAgent>, NeuralAgent> space, int x, int y)
        {
            Anchor = space.InteractiveSpace.CreateAgentAnchor(this, x, y);
            Interaction = space.InteractiveSpace.CreateInteraction();
            Fitness = space.CreateFitness();
            Goal = new AgentGoal();
        }
    }
}
