namespace Pathfinder
{
    public class GraphPath
    {
        public readonly Graph Graph;
        public readonly int[] Path;

        public GraphPath(Graph graph, int[] path)
        {
            Graph = graph;
            Path = path;
        }
    }
}
