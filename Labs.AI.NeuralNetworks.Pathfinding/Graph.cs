using System.Drawing;

namespace Pathfinder
{
    public class Graph
    {
        public readonly PointF[] Vertices;
        public readonly float[,] Edges;

        public Graph(PointF[] vertices, float[,] edges)
        {
            Vertices = vertices;
            Edges = edges;
        }
    }
}
