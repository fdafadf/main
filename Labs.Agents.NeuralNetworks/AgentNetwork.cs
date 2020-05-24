using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetwork
    {
        static int All;
        static int Zeros;
        public readonly AgentNetworkInputCoder InputCoder;
        public readonly Network Network;

        public AgentNetwork(AgentNetworkInputCoder inputCoder, IEnumerable<Layer> layers)
        {
            InputCoder = inputCoder;
            Network = new Network(layers);
        }

        public AgentNetwork(FileInfo fileInfo)
        {
            using (FileStream stream = fileInfo.OpenRead())
            {
                var layers = NetworkSerializer.LoadLayers(new BinaryReader(stream));
                var viewRadius = AgentNetworkInputCoder.EncodedSizeToViewRadius(layers.First().Neurons.First().Weights.Length - 1);
                InputCoder = new AgentNetworkInputCoder(viewRadius);

                if (layers.Last().Neurons.Length != 1)
                {
                    throw new ArgumentException("Invalid neural network shape.");
                }

                if (layers.First().Neurons.First().Weights.Length - 1 != InputCoder.EncodedSize)
                {
                    throw new ArgumentException("Invalid neural network shape.");
                }

                Network = new Network(layers);
            }
        }

        public AgentNetwork(AgentNetworkDefinition definition, ILayerInitializer initializer)
        {
            InputCoder = new AgentNetworkInputCoder(definition.ViewRadius);
            var lastLayer = new NetworkLayerDefinition(definition.LastLayerActivationFunction, 1);
            var networkDefinition = new NetworkDefinition(InputCoder.EncodedSize, definition.Layers.Concat(new[] { lastLayer }));
            Network = NetworkBuilder.Build(networkDefinition, initializer);
        }

        public AgentNetworkPrediction Predict(NeuralAgent agent)
        {
            return Predict(InputCoder.Encode(agent));
        }

        public AgentNetworkPrediction Predict(double[] input)
        {
            double bestValue = double.MinValue;
            CardinalMovement bestAction = null;

            foreach (CardinalMovement action in CardinalMovement.All)
            {
                InputCoder.EncodeAction(input, action);
                double predictedValue = Network.Evaluate(input)[0];
                All++;

                if (predictedValue == 0)
                {
                    Zeros++;
                }

                if (All % 1000000 == 0)
                {
                    Console.WriteLine($"{Zeros}/{All}");
                    //System.Diagnostics.Debugger.Break();
                }

                if (predictedValue > bestValue)
                {
                    bestValue = predictedValue;
                    bestAction = action;
                }
            }

            InputCoder.EncodeAction(input, bestAction);
            return new AgentNetworkPrediction(bestAction, bestValue, input);
        }

        public double[] CreateInput(NeuralAgent agent, CardinalMovement action)
        {
            var input = InputCoder.Encode(agent);
            InputCoder.EncodeAction(input, action);
            return input;
        }

        public void Fit(IEnumerable<MarkovHistoryItem> batch, AgentNetworkTrainingConfiguration configuration, Random random)
        {
            var optimizer = new SGDMomentum(Network, configuration.LearningRate, configuration.Momentum, Console.Out);
            var trainer = new Trainer(optimizer, random);
            var nextQ = batch.Select(item => new Projection(item.Input, new double[] { item.Reward + configuration.Gamma * Predict(item.State).Value })).ToArray();
            trainer.Train(nextQ, configuration.EpochesPerIteration, configuration.BatchSize);
        }

        public void Save(FileInfo fileInfo)
        {
            using (FileStream stream = fileInfo.OpenWrite())
            {
                NetworkSerializer.SaveLayers(new BinaryWriter(stream), Network.Layers);
            }
        }

        public override string ToString()
        {
            var activationFunction = Network.Layers.First().ActivationFunctionName;
            return $"{activationFunction}, {InputCoder.ViewRadius}, [{Network}]";
        }
    }
}
