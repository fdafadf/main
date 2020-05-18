using System.Collections.Generic;

namespace Labs.Agents
{
    public abstract class InteractiveSpace<TAgent, TAction, TActionResult> : Space<TAgent>, IInteractiveSpace<TAgent>
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>
    {
        public InteractiveSpace(int width, int height, AgentsCollisionModel agentsCollisionModel) : base(width, height, agentsCollisionModel)
        {
        }

        public InteractiveSpace(SpaceTemplate spaceTemplate, AgentsCollisionModel agentsCollisionModel) : base(spaceTemplate, agentsCollisionModel)
        {
        }

        public abstract Interaction<TAction, TActionResult> CreateInteraction();
        public abstract void Interact(IEnumerable<TAgent> agents);
    }
}
