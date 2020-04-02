namespace Pathfinder
{
    public class NetworkWalkSample
    {
        public const int WalkLength = 8;
        public const int NumberOfWalks = 8;

        public static int InputSize(int numberOfVertices)
        {
            return numberOfVertices * WalkLength * NumberOfWalks;
        }

        public readonly int Size;
        public readonly double[] Values;

        public NetworkWalkSample(Graph graph)
        {
            Size = graph.Vertices.Length;
            Values = new double[InputSize(Size)];

            for (int i = 0; i < graph.Vertices.Length; i++)
            {

            }
        }
    }
}
