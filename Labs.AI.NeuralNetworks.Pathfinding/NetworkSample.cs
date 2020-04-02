using AI.NeuralNetworks;
using System;

namespace Pathfinder
{
    public class NetworkSample : Projection
    {
        public static NetworkSample Create(GraphPath graphPath, int currentVertex, int label)
        {
            NetworkSampleInput input = new NetworkSampleInput(graphPath.Graph, currentVertex);
            double[] output = new double[graphPath.Graph.Vertices.Length];
            output[label] = 1;
            return new NetworkSample(graphPath, currentVertex, input.Values, output);
        }

        public readonly GraphPath GraphPath;
        public readonly int CurrentVertex;

        protected NetworkSample(GraphPath graphPath, int currentVertex, double[] input, double[] output) : base(input, output)
        {
            GraphPath = graphPath;
            CurrentVertex = currentVertex;
        }

        private static void SetLabel(double[] output, int Size, int index)
        {
            Array.Clear(output, 0, Size);
            output[index] = 1;
        }
    }
}
