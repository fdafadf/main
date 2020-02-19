using AI.NeuralNetwork;
using System.Collections.Generic;

namespace Demos.AI.NeuralNetwork
{
    public class TicTacToeExamples : Examples
    {
        public static void Compare1()
        {
            int epoches = 50;
            var @in = Demos.TicTacToe.DataLoader.TrainingFeatures;
            var @out = Demos.TicTacToe.DataLoader.TrainingLabels;
            List<Optimizer> optimizers = new List<Optimizer>();
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.0001, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.01, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.1, 0.04));
            ChartForm chartForm = new ChartForm("TicTacToe");

            foreach (var optimizer in optimizers)
            {
                chartForm.Add(optimizer, Train(optimizer, @in, @out, epoches, Monitors(MSE)));
            }

            chartForm.ShowDialog();
        }

        public static void Compare2()
        {
            int epoches = 30;
            var @in = Demos.TicTacToe.DataLoader.TrainingFeatures;
            var @out = Demos.TicTacToe.DataLoader.TrainingLabels;
            List<Optimizer> optimizers = new List<Optimizer>();
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.0001, 0.004));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.004));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.4));
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.01, 0.4));
            ChartForm chartForm = new ChartForm("TicTacToe");

            foreach (var optimizer in optimizers)
            {
                chartForm.Add(optimizer, Train(optimizer, @in, @out, epoches, Monitors(MSE)));
            }

            chartForm.ShowDialog();
        }

        public static void Compare3()
        {
            int epoches = 30;
            var @in = Demos.TicTacToe.DataLoader.TrainingFeatures;
            var @out = Demos.TicTacToe.DataLoader.TrainingLabels;
            List<Optimizer> optimizers = new List<Optimizer>();
            optimizers.Add(SGDMomentum(Network(RELU, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            optimizers.Add(SGDMomentum(Network(LEAKY, 9, 3, 72, 72, 72, 36, 36, 36, 18, 18), 0.001, 0.04));
            ChartForm chartForm = new ChartForm("TicTacToe");

            foreach (var optimizer in optimizers)
            {
                chartForm.Add(optimizer, Train(optimizer, @in, @out, epoches, Monitors(MSE)));
            }

            chartForm.ShowDialog();
        }

        public static TrainingMonitor[] Monitors(params TrainingMonitor[] monitors)
        {
            return monitors;
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
