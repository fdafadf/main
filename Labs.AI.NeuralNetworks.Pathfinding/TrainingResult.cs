using AI.NeuralNetworks;
using Demos.AI.NeuralNetwork;
using System.Collections.Generic;

namespace Pathfinder
{
    public class TrainingResult : Serie
    {
        public Network Network;
        public GraphPath[] TrainingGraphs;
        public NetworkSample[] TrainingData;

        public TrainingResult(string name, IEnumerable<double> errors, Network network, GraphPath[] trainingGraphs, NetworkSample[] trainingData) : base(name, errors)
        {
            Network = network;
            TrainingGraphs = trainingGraphs;
            TrainingData = trainingData;
        }
    }
}
