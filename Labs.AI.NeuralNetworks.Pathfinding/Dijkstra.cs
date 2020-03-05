using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder
{
    public class Dijkstra
    {
        public static GraphPath Find(Graph graph)
        {
            int n = graph.Vertices.Length;
            float[] distances = new float[n];
            int[] predecessors = new int[n];
            Array.Fill(distances, float.PositiveInfinity);
            distances[0] = 0;
            int[] queue = Enumerable.Range(0, n).ToArray();
            int queuePosition = 0;

            int dequeue()
            {
                float min = distances[queue[queuePosition]];
                int minIndex = queue[queuePosition];
                int p = queuePosition;

                for (int i = queuePosition + 1; i < n; i++)
                {
                    int index = queue[i];

                    if (distances[index] < min)
                    {
                        min = distances[index];
                        minIndex = index;
                        p = i;
                    }
                }

                int tmp = queue[queuePosition];
                queue[queuePosition] = minIndex;
                queue[p] = tmp;
                queuePosition++;
                return minIndex;
            }

            for (int i = 0; i < n; i++)
            {
                int minIndex = dequeue();

                for (int j = 0; j < n; j++)
                {
                    if (graph.Edges[minIndex, j] > float.Epsilon)
                    {
                        float d = distances[minIndex] + graph.Edges[minIndex, j];

                        if (d < distances[j])
                        {
                            distances[j] = d;
                            predecessors[j] = minIndex;
                        }
                    }
                }
            }

            List<int> path = new List<int>();
            int t = n - 1;
            path.Add(t);

            while (t != 0)
            {
                t = predecessors[t];
                path.Add(t);
            }

            path.Reverse();
            return new GraphPath(graph, path.ToArray());
        }
    }
}
