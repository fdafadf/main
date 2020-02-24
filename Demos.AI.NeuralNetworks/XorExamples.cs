using AI.NeuralNetworks;
using System;
using System.Diagnostics;
using static Demos.AI.NeuralNetwork.NetworkUtilities;

namespace Demos.AI.NeuralNetwork
{
    class XorExamples
    {
        public static void Train1(int epoches, double learningRate, params int[] layers)
        {
            var optimizer = SGD(Network(SIGM, 2, 1, layers), learningRate);
            Train2("Xor", optimizer, Inputs, Outputs, epoches);
            WritePredictions(optimizer.Network, Inputs, Outputs);
            Console.WriteLine();
        }

        public static void WritePredictions(Network evaluator, double[][] inputs, double[][] outputs)
        {
            Console.WriteLine($"Test results:");

            for (int k = 0; k < inputs.Length; k++)
            {
                Console.WriteLine($"{inputs[k][0]:f0} xor {inputs[k][1]:f0} = {evaluator.Evaluate(inputs[k])[0]:f2}");
            }
        }

        public static void Compare()
        {
            int epoches = 6000;
            var optimizer1 = SGD(Network(SIGM, 2, 1, 4), 0.3);
            var optimizer2 = SGD(Network(SIGM, 2, 1, 4, 6), 0.3);
            var optimizer3 = SGD(Network(SIGM, 2, 1, 40, 80, 60), 0.1);
            var optimizer4 = SGD(Network(RELU, 2, 1, 4), 0.03);
            //var optimizer5 = SGD(Network(RELU, 2, 1, 4, 6), 0.9); nie reaguje na LR
            var optimizer6 = SGD(Network(RELU, 2, 1, 40, 80, 60), 0.003);
            ChartForm chartForm = new ChartForm("Xor");
            chartForm.Add(optimizer1, Train(optimizer1, Inputs, Outputs, epoches, 1, MSE));
            chartForm.Add(optimizer2, Train(optimizer2, Inputs, Outputs, epoches, 1, MSE));
            chartForm.Add(optimizer3, Train(optimizer3, Inputs, Outputs, epoches, 1, MSE));
            chartForm.Add(optimizer4, Train(optimizer4, Inputs, Outputs, epoches, 1, MSE));
            //chartForm.Add(optimizer5, Train(optimizer5, Inputs, Outputs, epoches, MSE));
            chartForm.Add(optimizer6, Train(optimizer6, Inputs, Outputs, epoches, 1, MSE));
            chartForm.ShowDialog();
        }

        public static void CompareMseDemo(int epoches, params int[] layers)
        {
            Console.WriteLine($"[CompareMseDemo] [Network: {string.Join(",", layers)}] [Epoches: {epoches}]");
            var optimizer1 = SGD(Network(SIGM, 2, 1, layers), 0.3);
            var optimizer2 = SGD(Network(SIGM, 2, 1, layers), 0.1);
            var optimizer3 = SGDMomentum(Network(SIGM, 2, 1, layers), 0.3, 0.5);
            var optimizer4 = SGDMomentum(Network(SIGM, 2, 1, layers), 0.1, 0.5);
            ChartForm chartForm = new ChartForm();
            chartForm.Add(optimizer1, Train(optimizer1, Inputs, Outputs, epoches));
            //chartForm.Add(Train(optimizer2, Inputs, Outputs, epoches));
            //chartForm.Add(Train(optimizer3, Inputs, Outputs, epoches));
            //chartForm.Add(Train(optimizer4, Inputs, Outputs, epoches));
            chartForm.ShowDialog();
        }

        public static void Train2<TOptimizer>(string name, TOptimizer optimizer, double[][] inputs, double[][] outputs, int epoches)
            where TOptimizer : SGD
        {
            Console.WriteLine($"[Demo: {name}] [Network: {optimizer.Network}] [Epoches: {epoches}]");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
        
            for (int epoch = 0; epoch < epoches; epoch++)
            {
                for (int k = 0; k < inputs.Length; k++)
                {
                    optimizer.Evaluate(inputs[k], outputs[k]);
                    optimizer.Update(1);
                }
            }
        
            stopwatch.Stop();
            Console.WriteLine($"Training time: {stopwatch.ElapsedMilliseconds}ms");
        }

        readonly static double[][] Inputs =
        {
            new double[] { 0, 0 },
            new double[] { 1, 0 },
            new double[] { 0, 1 },
            new double[] { 1, 1 }
        };

        readonly static double[][] Outputs =
        {
            new double[] { 0 },
            new double[] { 1 },
            new double[] { 1 },
            new double[] { 0 }
        };
    }
}
