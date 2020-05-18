using System;
using System.Numerics;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetworkInputCoder
    {
        private static int ViewRadiusToEncodedSize(int viewRadius)
        {
            int viewLength = viewRadius * 2 + 1;
            int viewSize = viewLength * viewLength;
            int directionSize = 2;
            int stateSize = viewSize * 2 + directionSize;
            int actionsSize = CardinalMovement.All.Length;
            return stateSize + actionsSize;
        }

        public static int EncodedSizeToViewRadius(int encodedSize)
        {
            int actionsSize = CardinalMovement.All.Length;
            int stateSize = encodedSize - actionsSize;
            int directionSize = 2;
            int viewSize = (stateSize - directionSize) / 2;
            int viewLength = (int)Math.Sqrt(viewSize);
            return (viewLength - 1) / 2;
        }

        public readonly int ViewRadius;
        public readonly int ViewLength;
        public readonly int ObstaclesOffset = 0;
        public readonly int AgentsOffset;
        public readonly int DirectionOffset;
        public readonly int ActionsOffset;
        public readonly int EncodedSize;

        public AgentNetworkInputCoder(int viewRadius)
        {
            ViewRadius = viewRadius;
            ViewLength = viewRadius * 2 + 1;
            AgentsOffset = ViewLength * ViewLength;
            DirectionOffset = AgentsOffset * 2;
            ActionsOffset = DirectionOffset + 2;
            EncodedSize = ViewRadiusToEncodedSize(viewRadius);
        }

        public double[] Encode(NeuralAgent agent)
        {
            double[] input = new double[EncodedSize];
            EncodeState(input, agent);
            EncodeGoal(input, agent);
            return input;
        }

        public void EncodeState(double[] input, NeuralAgent agent)
        {
            var space = agent.Anchor.Field.Space;
            int px = agent.Anchor.Field.X - ViewRadius;
            int py = agent.Anchor.Field.Y - ViewRadius;
            int i = ObstaclesOffset;

            for (int y = 0; y < ViewLength; y++)
            {
                for (int x = 0; x < ViewLength; x++)
                {
                    var field = space[px + x, py + y];
                    input[i++] = field.IsOutside || field.IsObstacle ? 1 : 0;
                }
            }

            px = agent.Anchor.Field.X - ViewRadius;
            py = agent.Anchor.Field.Y - ViewRadius;
            i = AgentsOffset;

            for (int y = 0; y < ViewLength; y++)
            {
                for (int x = 0; x < ViewLength; x++)
                {
                    var field = space[px + x, py + y];
                    input[i++] = field.IsAgent ? 1 : 0;
                }
            }
        }

        public void EncodeGoal(double[] input, NeuralAgent agent)
        {
            int dx = agent.Goal.Position.X - agent.Anchor.Field.X;
            int dy = agent.Goal.Position.Y - agent.Anchor.Field.Y;

            if (dx == 0 && dy == 0)
            {
                input[DirectionOffset] = 0;
                input[DirectionOffset + 1] = 0;
            }
            else
            {
                var dn = Vector2.Normalize(new Vector2(dx, dy));
                input[DirectionOffset] = dn.X;
                input[DirectionOffset + 1] = dn.Y;
            }
            //var v1 = Vector2.Normalize(agent.Direction);
            //var v2 = Vector2.Normalize(agent.Goal - agent.Position);
            //var sin = Vector2.Dot(v2, v1);
            //var cos = v1.CrossProduct(v2);
        }

        public void EncodeAction(double[] input, CardinalMovement action)
        {
            Array.Clear(input, ActionsOffset, CardinalMovement.All.Length);
            input[ActionsOffset + action.Index] = 1;
        }
    }
}
