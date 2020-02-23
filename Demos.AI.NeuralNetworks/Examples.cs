using Games.Utilities;
using AI.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AI.NeuralNetworks;

namespace Demos.AI.NeuralNetwork
{
    public class Examples
    {
        public static readonly IFunction RELU = Function.ReLU;
        public static readonly IFunction LEAKY = Function.LeakyReLU;
        public static readonly IFunction SIGM = Function.Sigmoidal;

        public static TrainingMonitor MSE
        {
            get
            {
                return new MeanSquareErrorMonitor();
            }
        }

        public static Network Network(IFunction activationFunction, int inputSize, int outputSize, params int[] hiddenLayersSizes)
        {
            return new Network(activationFunction, inputSize, outputSize, hiddenLayersSizes);
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

        public static TrainingMonitorCollection Train(Optimizer optimizer, double[][] features, double[][] labels, int epoches, int batchSize = 1, params TrainingMonitor[] monitors)
        {
            var trainer = new Trainer(optimizer, new Random(0));

            foreach (var monitor in monitors)
            {
                trainer.Monitors.Add(monitor);
            }

            //optimizer.Monitors.Add(new CosoleTitleProgressMonitor());
            trainer.Train(features, labels, epoches, batchSize);
            return trainer.Monitors;
        }

        public static double MaxWeight(Network network)
        {
            return network.Layers.Max(layer => layer.Neurons.Max(neuron => neuron.Weights.Max(w => Math.Abs(w))));
        }

        public static void WritePredictions(Network evaluator, double[][] inputs, double[][] outputs)
        {
            Console.WriteLine($"Test results:");

            for (int k = 0; k < inputs.Length; k++)
            {
                Console.WriteLine($"{inputs[k][0]:f0} xor {inputs[k][1]:f0} = {evaluator.Evaluate(inputs[k])[0]:f2}");
            }
        }
    }
}
