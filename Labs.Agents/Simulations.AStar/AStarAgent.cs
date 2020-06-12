using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents.Simulations.AStar
{
    public class AStarAgent : IAnchoredAgent<AStarAgent>, IDestructibleAgent, IInteractiveAgent<CardinalMovement, InteractionResult>, IGoalAgent
    {
        public AgentSpaceAnchor<AStarAgent> Anchor { get; }
        public Interaction<CardinalMovement, InteractionResult> Interaction { get; }
        public AgentFitness Fitness { get; }
        public AgentGoal Goal { get; }
        LinkedList<CardinalMovement> PathToGoal = new LinkedList<CardinalMovement>();

        public AStarAgent(DestructibleInteractiveSpace<CardinalMovementSpace<AStarAgent>, AStarAgent> space, Point p) : this(space, p.X, p.Y)
        {
        }

        public AStarAgent(DestructibleInteractiveSpace<CardinalMovementSpace<AStarAgent>, AStarAgent> space, int x, int y)
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
            List<UShortPoint> visited = new List<UShortPoint>();
            UShortPoint goalPosition = new UShortPoint((ushort)Goal.Position.X, (ushort)Goal.Position.Y);

            for (ushort y = 0; y < height; y++)
            {
                for (ushort x = 0; x < width; x++)
                {
                    var point = new UShortPoint(x, y);
                    distances[point] = int.MaxValue;
                    predecessors[point] = default;

                    if (space[x, y].IsEmpty || space[x, y].HasAgent)
                    {
                        unvisited.Add(point);
                    }
                }
            }

            void removeUnvisitedMin()
            {
                int minDistance = int.MaxValue;
                UShortPoint minVisited = default;
                UShortPoint minUnvisited = default;
                CardinalMovement minMove = default;

                foreach (UShortPoint visitedPoint in visited)
                {
                    foreach (CardinalMovement move in CardinalMovement.AllExceptNothing)
                    {
                        UShortPoint neighbor = visitedPoint.Add(move);

                        if (unvisited.Contains(neighbor))
                        {
                            var neighborField = space[neighbor.X, neighbor.Y];
                            var distanceOverNeighbor = distances[visitedPoint] + 1 + neighbor.Distance(goalPosition);

                            if (distanceOverNeighbor < minDistance)
                            {
                                minDistance = distanceOverNeighbor;
                                minVisited = visitedPoint;
                                minUnvisited = neighbor;
                                minMove = move;
                            }
                        }
                    }
                }

                distances[minUnvisited] = distances[minVisited] + 1;
                predecessors[minUnvisited] = minMove;
                unvisited.Remove(minUnvisited);
                visited.Add(minUnvisited);
            }

            UShortPoint startPosition = new UShortPoint((ushort)Anchor.Field.X, (ushort)Anchor.Field.Y);
            distances[startPosition] = 0;
            unvisited.Remove(startPosition);
            visited.Add(startPosition);

            while (visited.Contains(goalPosition) == false)
            {
                removeUnvisitedMin();
            }

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
