using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Pathfinder
{
    public class GraphGenerator
    {
        public static Graph Generate(Random random, int numberOfVertices)
        {
            return Generate(random, numberOfVertices, i => 3);
        }

        public static IEnumerable<Graph> Generate(Random random, int numberOfGraphs, int numberOfVertices)
        {
            for (int i = 0; i < numberOfGraphs; i++)
            {
                yield return Generate(random, numberOfVertices, i => 3);
            }
        }

        public static Graph Generate(Random random, int numberOfVertices, Func<int, int> density)
        {
            PointF[]  vertices = new PointF[numberOfVertices];
            vertices.FillRandom(random);
            float[,] edges = new float[numberOfVertices, numberOfVertices];

            for (int i = 1; i < numberOfVertices; i++)
            {
                int[] indexes = Enumerable.Range(0, i).ToArray();
                indexes.Shuffle(random);
                int lim = random.Next(1, density(i));

                for (int j = 0; j < i; j++)
                {
                    if (j < lim)
                    {
                        edges[i, indexes[j]] = 1;
                        edges[indexes[j], i] = 1;
                    }
                }
            }

            return new Graph(vertices, edges);
        }
    }
}
