namespace Pathfinder
{
    public class GraphState
    {
        public readonly Graph Graph;
        public readonly int CurrentVertex;

        public GraphState(Graph graph, int currentVertex)
        {
            Graph = graph;
            CurrentVertex = currentVertex;
        }
    }
}
