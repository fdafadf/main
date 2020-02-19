//#define MOMENTUM

using Games.Utilities;
using System;

namespace SimpleNeuralNetwork
{
    public abstract class Optimizer
    {
        public Network Network { get; }
        public double LearningRate { get; private set; }
        protected double[][] gradient;

        public Optimizer(Network network, double learningRate)
        {
            Network = network;
            LearningRate = learningRate;
        }

        public abstract double[] Optimize(double[] features, double[] labels);
    }
}
