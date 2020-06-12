using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class DestructibleInteractiveSpace<TSpace, TAgent>
        where TSpace : IInteractiveSpace<TAgent>
        where TAgent : IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent
    {
        public TSpace InteractiveSpace { get; }
        public int TotalCollisions { get; private set; }
        public List<double> Collisions = new List<double>();

        public DestructibleInteractiveSpace(TSpace interactiveSpace)
        {
            InteractiveSpace = interactiveSpace;
        }

        public AgentFitness CreateFitness()
        {
            return new AgentFitness();
        }

        public void Interact(IEnumerable<TAgent> agents)
        {
            foreach (var agent in agents.Where(agent => agent.Fitness.IsDestroyed))
            {
                agent.Interaction.Action = CardinalMovement.Nothing;
            }

            InteractiveSpace.Interact(agents);

            foreach (var agent in agents)
            {
                var actionResult = agent.Interaction.ActionResult;

                if (agent.Fitness.IsDestroyed)
                {
                    agent.Interaction.ActionResult = InteractionResult.Suspended;
                }
                else if (actionResult == InteractionResult.Collision)
                {
                    TotalCollisions++;
                    agent.Fitness.IsDestroyed = true;
                }
                else if (actionResult == InteractionResult.SuccessCollision)
                {
                    TotalCollisions++;
                }
            }

            Collisions.Add(TotalCollisions);
        }
    }
}
