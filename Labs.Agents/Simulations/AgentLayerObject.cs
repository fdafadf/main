namespace Labs.Agents
{
    public class AgentLayerObject<TAgent> : AnimatedRectangle
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>
    {
        public readonly TAgent Agent;

        public AgentLayerObject(TAgent agent) : base(agent.Anchor.Field.X, agent.Anchor.Field.Y, 0, 0, 4)
        {
            Agent = agent;
        }

        public override bool Update(int currentFrame)
        {
            if (currentFrame == 0)
            {
                StartPosition.X = Agent.Interaction.From.X;
                StartPosition.Y = Agent.Interaction.From.Y;
                TargetPosition.X = Agent.Interaction.To.X;
                TargetPosition.Y = Agent.Interaction.To.Y;
            }

            return base.Update(currentFrame);
        }
    }
}
