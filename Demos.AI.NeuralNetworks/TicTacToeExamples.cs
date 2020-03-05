using AI.NeuralNetworks;
using AI.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using static Demos.AI.NeuralNetwork.NetworkUtilities;

namespace Demos.AI.NeuralNetwork
{
    public class TicTacToeExamples
    {
        public static void Compare1()
        {
            int epoches = 50;
            var data = TicTacToeValueLoader.LoadAllUniqueStates(Storage.Instance);
            List<Optimizer> optimizers = new List<Optimizer>();
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.0001, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.01, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.1, 0.04));
            ChartForm chartForm = new ChartForm("TicTacToe");

            foreach (var optimizer in optimizers)
            {
                chartForm.Add(optimizer, Train(optimizer, data, epoches, 1, Monitors(MSE)));
            }

            chartForm.ShowDialog();
        }

        public static void Compare2()
        {
            int epoches = 30;
            var data = TicTacToeValueLoader.LoadAllUniqueStates(Storage.Instance);
            List<Optimizer> optimizers = new List<Optimizer>();
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.0001, 0.004));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.004));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.4));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.01, 0.4));
            ChartForm chartForm = new ChartForm("TicTacToe");

            foreach (var optimizer in optimizers)
            {
                chartForm.Add(optimizer, Train(optimizer, data, epoches, 1, Monitors(MSE)));
            }

            chartForm.ShowDialog();
        }

        public static void Compare3()
        {
            int epoches = 30;
            var data = TicTacToeValueLoader.LoadAllUniqueStates(Storage.Instance);
            List<Optimizer> optimizers = new List<Optimizer>();
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            optimizers.Add(SGDMomentum(Network(LEAKY, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            ChartForm chartForm = new ChartForm("TicTacToe");

            foreach (var optimizer in optimizers)
            {
                chartForm.Add(optimizer, Train(optimizer, data, epoches, 1, Monitors(MSE)));
            }

            chartForm.ShowDialog();
        }

        public static void Compare4()
        {
            int epoches = 600;
            var data = TicTacToeValueLoader.LoadAllUniqueStates(Storage.Instance);
            //List<Optimizer> optimizers = new List<Optimizer>();
            var optimizer1 = SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04);
            var optimizer2 = SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04);
            var optimizer3 = SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04);
            var optimizer4 = AdaGrad(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.01);
            //optimizers.Add(AdaGrad(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001));
            ChartForm chartForm = new ChartForm("TicTacToe");
            //chartForm.Add(optimizer1, Train(optimizer1, @in, @out, epoches, 1, Monitors(MSE)));
            //chartForm.Add(optimizer2, Train(optimizer2, @in, @out, epoches, 8, Monitors(MSE)));
            chartForm.Add(optimizer3, Train(optimizer3, data, epoches, 32, Monitors(MSE)));
            chartForm.Add(optimizer4, Train(optimizer4, data, epoches, 32, Monitors(MSE)));
            chartForm.ShowDialog();
        }

        //public static OptimizerMonitor Accuracy
        //{
        //    get
        //    {
        //        return new TicTacToeAccuracyMonitor();
        //    }
        //}
    }
}
