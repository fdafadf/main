using System;

namespace Pathfinder
{
    public class NetworkSampleInput
    {
        public static int InputSize(int numberOfVertices)
        {
            //return numberOfVertices * 2 + numberOfVertices * numberOfVertices + numberOfVertices;
            return numberOfVertices * numberOfVertices + numberOfVertices;
        }

        public readonly int Size;
        public readonly double[] Values;

        public NetworkSampleInput(Graph graph, int currentVertex)
        {
            Size = graph.Vertices.Length;
            Values = new double[InputSize(Size)];
            SetGraph(graph);
            SetCurrentVertex(currentVertex);
        }

        private void SetGraph(Graph graph)
        {
            int p = 0;

            //for (int i = 0; i < Size; i++)
            //{
            //    Values[p++] = graph.Vertices[i].X;
            //    Values[p++] = graph.Vertices[i].Y;
            //}

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (graph.Edges[i, j] > float.Epsilon)
                    {
                        Values[p++] = graph.Edges[i, j] / 100.0;
                    }
                    else
                    {
                        Values[p++] = 1;
                    }
                }
            }
        }

        public void SetCurrentVertex(int index)
        {
            int p = /*Size * 2 +*/ Size * Size;
            Array.Clear(Values, p, Size);
            Values[p + index] = 1;
        }
    }
}
