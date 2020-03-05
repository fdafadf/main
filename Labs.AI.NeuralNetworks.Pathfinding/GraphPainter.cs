using System.Drawing;

namespace Pathfinder
{
    public class GraphPainter
    {
        public readonly GraphPath GraphPath;
        public readonly PointF[] Vertices;
        private Brush[] VerticesBrushes;
        private float scale;
        private int margin = 5;

        public GraphPainter(GraphPath graphPath)
        {
            GraphPath = graphPath;
            Graph graph = graphPath.Graph;
            Vertices = new PointF[graph.Vertices.Length];
            VerticesBrushes = new Brush[graph.Vertices.Length];
            Scale = 1;
            
            for (int i = 0; i < Vertices.Length; i++)
            {
                VerticesBrushes[i] = Brushes.DarkGray;
            }

            VerticesBrushes[0] = Brushes.Magenta;
            VerticesBrushes[Vertices.Length - 1] = Brushes.LightSeaGreen;
        }

        public float Scale 
        { 
            get
            {
                return scale;
            }
            set
            {
                scale = value - margin * 2;

                for (int i = 0; i < Vertices.Length; i++)
                {
                    Vertices[i].X = GraphPath.Graph.Vertices[i].X * scale;
                    Vertices[i].Y = GraphPath.Graph.Vertices[i].Y * scale;
                }
            }
        }

        public void Paint(Graphics g)
        {
            for (int i = 1; i < Vertices.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (GraphPath.Graph.Edges[i, j] > float.Epsilon)
                    {
                        g.DrawLine(Pens.LightSteelBlue, Vertices[i], Vertices[j]);
                    }
                }
            }

            for (int i = 0; i < Vertices.Length; i++)
            {
                g.FillEllipse(VerticesBrushes[i], Vertices[i].X - 5, Vertices[i].Y - 5, 10, 10);
            }
        }
    }
}
