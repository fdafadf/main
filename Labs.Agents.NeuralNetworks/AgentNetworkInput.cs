using System;
using System.Numerics;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetworkInput
    {
        public static readonly int R = 2;
        public static readonly int V = R * 2 + 1;
        public static readonly int ObstaclesOffset = 0;
        public static readonly int AgentsOffset = V * V;
        public static readonly int DirectionOffset = AgentsOffset * 2;
        public static readonly int ActionsOffset = DirectionOffset + 2;
        public static readonly int Size = ActionsOffset + Action2.All.Length;
        public readonly double[] Encoded;

        public AgentNetworkInput(AgentState state)
        {
            Encoded = new double[Size];
            EncodeState(state);
            EncodeDirection(state);
        }

        public void EncodeState(AgentState state)
        {
            var environment = state.Field.Environment;
            int px = state.Field.X - R;
            int py = state.Field.Y - R;
            int i = ObstaclesOffset;

            for (int y = 0; y < V; y++)
            {
                for (int x = 0; x < V; x++)
                {
                    var field = environment[px + x, py + y];
                    Encoded[i++] = field.IsOutside || field.IsObstacle ? 1 : 0;
                }
            }

            px = state.Field.X - R;
            py = state.Field.Y - R;
            i = AgentsOffset;

            for (int y = 0; y < V; y++)
            {
                for (int x = 0; x < V; x++)
                {
                    var field = environment[px + x, py + y];
                    Encoded[i++] = field.IsAgent ? 1 : 0;
                }
            }
        }

        public void EncodeDirection(AgentState state)
        {
            int dx = state.Goal.X - state.Field.X;
            int dy = state.Goal.Y - state.Field.Y;
            var dn = Vector2.Normalize(new Vector2(dx, dy));
            Encoded[DirectionOffset] = dn.X;
            Encoded[DirectionOffset + 1] = dn.Y;
            //var v1 = Vector2.Normalize(agent.Direction);
            //var v2 = Vector2.Normalize(agent.Goal - agent.Position);
            //var sin = Vector2.Dot(v2, v1);
            //var cos = v1.CrossProduct(v2);
        }

        public void EncodeAction(Action2 action)
        {
            Array.Clear(Encoded, ActionsOffset, Action2.All.Length);
            Encoded[ActionsOffset + action.Index] = 1;
        }
    }
}
