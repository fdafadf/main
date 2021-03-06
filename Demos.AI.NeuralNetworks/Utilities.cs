﻿using AI.NeuralNetworks;
using System;
using System.Linq;

namespace Demos.AI.NeuralNetwork
{
    public static class NetworkUtilities
    {
        public static readonly FunctionName RELU = FunctionName.ReLU;
        public static readonly FunctionName LEAKY = FunctionName.LeakyReLU;
        public static readonly FunctionName SIGM = FunctionName.Sigmoidal;

        public static TrainingMonitor MSE
        {
            get
            {
                return new MeanSquareErrorMonitor();
            }
        }

        public static TrainingMonitor[] Monitors(params TrainingMonitor[] monitors)
        {
            return monitors.Union(new TrainingMonitor[] { new TrainingEpochMonitor(t => { Console.Title = t; }, Console.Out) }).ToArray();
        }

        public static Network Network(FunctionName activationFunction, int inputSize, int outputSize, params int[] hiddenLayersSizes)
        {
            return NetworkBuilder.Build(new NetworkDefinition(activationFunction, inputSize, outputSize, hiddenLayersSizes), new He(0));
        }

        public static SGD SGD(Network evaluator, double learningRate)
        {
            return new SGD(evaluator, learningRate);
        }

        public static SGDMomentum SGDMomentum(Network network, double learningRate, double momentum)
        {
            return new SGDMomentum(network, learningRate, momentum);
        }

        public static AdaGrad AdaGrad(Network network, double learningRate)
        {
            return new AdaGrad(network, learningRate);
        }

        public static TrainingMonitorCollection Train(Optimizer optimizer, Projection[] data, int epoches, int batchSize = 1, params TrainingMonitor[] monitors)
        {
            var trainer = new Trainer(optimizer, new Random(0));

            foreach (var monitor in monitors)
            {
                trainer.Monitors.Add(monitor);
            }

            trainer.Train(data, epoches, batchSize);
            return trainer.Monitors;
        }

        //public static double MaxWeight(Network network)
        //{
        //    return network.Layers.Max(layer => layer.Neurons.Max(neuron => neuron.Weights.Max(w => Math.Abs(w))));
        //}
    }
}
