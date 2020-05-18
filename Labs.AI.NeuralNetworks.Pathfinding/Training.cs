using AI.NeuralNetworks;
using Demos.AI.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pathfinder
{
    public class Training
    {
        public static Task<TrainingResult>[] Train(IEnumerable<TrainingSet> trainingSets)
        {
            bool liveTracking = trainingSets.Count() == 1;
            return trainingSets.Select(set => new Training(set).Train(liveTracking)).ToArray();
        }

        public static readonly object ThreadLock = new object();

        public string Name;
        public int NumberOfVertices;
        public int NumberOfGraphs;
        public int[] HiddenLayers;
        public double LearningRate;
        public double Momentum;
        public int Seed;
        public int Epoches;
        public int BatchSize = 8;

        public Training(TrainingSet trainingSet) : this(trainingSet.Name, trainingSet.Training, trainingSet.TrainingData, trainingSet.Network)
        {
        }

        public Training(string name, TrainingSettings training, TrainingDataSettings trainingData, TrainingNetworkSettings network)
        {
            Name = name;
            NumberOfVertices = trainingData.NumberOfVertices;
            NumberOfGraphs = trainingData.NumberOfGraphs;
            HiddenLayers = network.HiddenLayers;
            LearningRate = training.LearningRate;
            Momentum = training.Momentum;
            Epoches = training.Epoches;
            Seed = training.Seed.HasValue ? training.Seed.Value : Guid.NewGuid().GetHashCode();
        }

        public Task<TrainingResult> Train(bool liveTracking)
        {
            return Task.Run(() =>
            {
                int inputSize = NetworkSampleInput.InputSize(NumberOfVertices);
                Random random = new Random(Seed);
                Network network = NetworkBuilder.Build(new NetworkDefinition(FunctionName.ReLU, inputSize, NumberOfVertices, HiddenLayers), new He(Seed));
                Optimizer optimizer = new SGDMomentum(network, LearningRate, Momentum);
                Trainer trainer = new Trainer(optimizer, random);

                GraphPath[] trainingGraphs = GraphGenerator.Generate(random, NumberOfGraphs, NumberOfVertices).Select(Dijkstra.Find).ToArray();
                NetworkSample[] trainingData = NetworkSampleGenerator.Generate(trainingGraphs).ToArray();

                MeanSquareErrorMonitor monitor = new MeanSquareErrorMonitor();
                trainer.Monitors.Add(monitor);

                if (liveTracking)
                {
                    trainer.Monitors.Add(new TrainingMonitor(Name));
                }

                trainer.Train(trainingData, Epoches, BatchSize);

                lock (ThreadLock)
                {
                    double error = MeanSquareErrorMonitor.CalculateError(network, trainingData);
                    Console.WriteLine($"[{Name}] Seed: {Seed,12}  Momentum: {Momentum,-6}  Learning Rate: {LearningRate,-6}  MSE: {error}");
                }

                return new TrainingResult(Name, monitor.CollectedData as IEnumerable<double>, network, trainingGraphs, trainingData);
            });
        }
    }
}
