using Labs.Agents.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents.Dijkstra
{
    public class DijkstraAgent : IAnchoredAgent<DijkstraAgent>, IDestructibleAgent, IInteractiveAgent<CardinalMovement, InteractionResult>, IGoalAgent
    {
        public AgentAnchor<DijkstraAgent> Anchor { get; }
        public Interaction<CardinalMovement, InteractionResult> Interaction { get; }
        public AgentFitness Fitness { get; }
        public AgentGoal Goal { get; }
        LinkedList<CardinalMovement> PathToGoal = new LinkedList<CardinalMovement>();

        public DijkstraAgent(DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent> space, Point p) : this(space, p.X, p.Y)
        {
        }

        public DijkstraAgent(DestructibleInteractiveSpace<CardinalMovementSpace<DijkstraAgent>, DijkstraAgent> space, int x, int y)
        {
            Anchor = space.InteractiveSpace.CreateAgentAnchor(this, x, y);
            Interaction = space.InteractiveSpace.CreateInteraction();
            Fitness = space.CreateFitness();
            Goal = new AgentGoal();
        }

        public void CalculateInteraction()
        {
            Interaction.Action = GetNextAction();
        }

        private CardinalMovement GetNextAction()
        {
            if (Goal.Position == default)
            {
                return CardinalMovement.Nothing;
            }
            else
            {
                if (PathToGoal.Count == 0)
                {
                    CalculatePathToGoal();
                }

                var action = PathToGoal.First.Value;
                PathToGoal.RemoveFirst();
                return action;
            }
        }

        private void CalculatePathToGoal()
        {
            var space = Anchor.Field.Space;
            int width = space.Width;
            int height = space.Height;
            Dictionary<UShortPoint, int> distances = new Dictionary<UShortPoint, int>();
            Dictionary<UShortPoint, CardinalMovement> predecessors = new Dictionary<UShortPoint, CardinalMovement>();
            List<UShortPoint> unvisited = new List<UShortPoint>();

            UShortPoint removeUnvisitedMin()
            {
                int minDistance = int.MaxValue;
                UShortPoint minPoint = default;

                foreach (UShortPoint point in unvisited)
                {
                    int distance = distances[point];

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minPoint = point;
                    }
                }

                unvisited.Remove(minPoint);
                return minPoint;
            }

            for (ushort y = 0; y < height; y++)
            {
                for (ushort x = 0; x < width; x++)
                {
                    var point = new UShortPoint(x, y);
                    distances[point] = int.MaxValue;
                    predecessors[point] = default;

                    if (space[x, y].IsEmpty || space[x, y].IsAgent)
                    {
                        unvisited.Add(point);
                    }
                }
            }

            UShortPoint startPosition = new UShortPoint((ushort)Anchor.Field.X, (ushort)Anchor.Field.Y);
            distances[startPosition] = 0;

            while (unvisited.Count > 0)
            {
                UShortPoint current = removeUnvisitedMin();
                int currentDistance = distances[current];

                foreach (CardinalMovement move in CardinalMovement.All)
                {
                    UShortPoint neighbor = current.Add(move);
                    var neighborField = space[neighbor.X, neighbor.Y];

                    if (neighborField.IsEmpty || neighborField.IsAgent)
                    {
                        int newDistance = currentDistance + 1;
                        int neighborDistance = distances[neighbor];

                        if (newDistance < neighborDistance)
                        {
                            distances[neighbor] = newDistance;
                            predecessors[neighbor] = move;
                        }
                    }
                }
            }

            UShortPoint goalPosition = new UShortPoint((ushort)Goal.Position.X, (ushort)Goal.Position.Y);

            while (goalPosition.Equals(startPosition) == false)
            {
                var action = predecessors[goalPosition];
                UShortPoint predecessor = goalPosition.Add(action.Opposite);
                PathToGoal.AddFirst(action);
                goalPosition = predecessor;
            }
        }
    }
}
