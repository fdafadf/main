using System.Collections.Generic;

namespace Pathfinder
{
    public class NetworkSampleGenerator
    {
        public static IEnumerable<NetworkSampleInput> Generate(IEnumerable<GraphPath> graphPaths)
        {
            foreach (GraphPath graphPath in graphPaths)
            {
                for (int j = 1; j < graphPath.Path.Length; j++)
                {
                    yield return NetworkSampleInput.Create(graphPath, graphPath.Path[j - 1], graphPath.Path[j]);
                }
            }
        }
    }
}
