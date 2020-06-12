using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class CardinalMovementSpace<TAgent> : InteractiveSpace<TAgent, CardinalMovement, InteractionResult>
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>
    {
        public CardinalMovementSpace(int width, int height, AgentsCollisionModel agentsCollisionModel) : base(width, height, agentsCollisionModel)
        {
        }

        public CardinalMovementSpace(SpaceTemplate spaceTemplate, AgentsCollisionModel agentsCollisionModel) : base(spaceTemplate, agentsCollisionModel)
        {
        }

        public override Interaction<CardinalMovement, InteractionResult> CreateInteraction()
        {
            return new Interaction<CardinalMovement, InteractionResult>();
        }

        public override void Interact(IEnumerable<TAgent> agents)
        {
            foreach (var agent in agents)
            {
                var interaction = agent.Interaction;
                interaction.From = agent.Anchor.Field;
                interaction.To = this[agent.Anchor.Field.X + interaction.Action.X, agent.Anchor.Field.Y + interaction.Action.Y];
            }

            //Console.WriteLine();
            //
            //foreach (var agent in agents)
            //{
            //    Console.WriteLine($"{agent.Interaction.From.X,2} {agent.Interaction.From.Y,2}   {agent.Interaction.To.X,2} {agent.Interaction.To.Y,2}");
            //}

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    fields[x, y].RemoveAnchors();
                    //fields[x, y].IsDestroyed = false;
                }
            }

            foreach (var agent in agents)
            {
                var interaction = agent.Interaction;

                if (interaction.To.IsOutside)
                {
                    UndoInteraction(interaction.From);
                    interaction.ActionResult = InteractionResult.Collision;
                    //Console.WriteLine($"Set {interaction.From.X}, {interaction.From.Y}");
                    fields[interaction.From.X, interaction.From.Y].AddAnchor(agent.Anchor);
                }
                else
                {
                    bool isActionValid = interaction.To.IsEmpty || (interaction.To.HasAgent && AgentsCollisionModel == AgentsCollisionModel.Ghost);

                    if (isActionValid)
                    {
                        interaction.ActionResult = interaction.To.HasAgent ? InteractionResult.SuccessCollision : InteractionResult.Success;
                        var field = fields[interaction.To.X, interaction.To.Y];
                        //Console.WriteLine($"Set {interaction.To.X}, {interaction.To.Y}");
                        field.AddAnchor(agent.Anchor);
                        agent.Anchor.Field = field;
                    }
                    else
                    {
                        UndoInteraction(interaction.To);
                        UndoInteraction(interaction.From);
                        interaction.ActionResult = InteractionResult.Collision; 
                        var field = fields[interaction.From.X, interaction.From.Y];
                        //Console.WriteLine($"Set {interaction.From.X}, {interaction.From.Y}");
                        field.AddAnchor(agent.Anchor);
                        agent.Anchor.Field = field;
                    }
                }
            }

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    fields[x, y].IsDestroyed = false;
                }
            }
        }

        public bool[,] GetObstacles()
        {
            bool[,] map = new bool[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    map[x, y] = fields[x, y].HasObstacle;
                }
            }

            return map;
        }

        private void UndoInteraction(ISpaceField spaceField)
        {
            var field = fields[spaceField.X, spaceField.Y];

            if (field.HasAgent)
            {
                foreach (var agent in field.Agents.ToArray())
                {
                    var interaction = agent.Interaction;

                    if (interaction.ActionResult != InteractionResult.Collision)
                    {
                        field.IsDestroyed = true;
                        interaction.ActionResult = InteractionResult.Collision;
                        field.RemoveAnchors();
                        //Console.WriteLine($"Rem {spaceField.X}, {spaceField.Y}");
                        UndoInteraction(interaction.From);
                        var sourceField = fields[interaction.From.X, interaction.From.Y];
                        //Console.WriteLine($"Set {interaction.From.X}, {interaction.From.Y}");
                        sourceField.AddAnchor(agent.Anchor);
                        agent.Anchor.Field = sourceField;
                    }
                }
            }
        }
    }
}
