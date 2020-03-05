using System.Drawing;

namespace Pathfinder
{
    public class GraphPathPainter
    {
        GraphPainter GraphPainter;
        int[] Path;
        Pen Pen;

        public GraphPathPainter(GraphPainter graphPainter, int[] path, Pen pen)
        {
            GraphPainter = graphPainter;
            Path = path;
            Pen = pen;
        }

        public void Paint(Graphics g)
        {
            for (int i = 1; i < Path.Length; i++)
            {
                g.DrawLine(Pen, GraphPainter.Vertices[Path[i - 1]], GraphPainter.Vertices[Path[i]]);
            }
        }
    }
}
