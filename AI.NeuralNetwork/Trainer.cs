//#define MOMENTUM

using Games.Utilities;
using System;

namespace SimpleNeuralNetwork
{
    public class Trainer
    {
        public Optimizer Optimizer { get; }
        public TrainingMonitorCollection Monitors { get; }
        protected Random random;

        public Trainer(Optimizer optimizer)
        {
            Optimizer = optimizer;
            Monitors = new TrainingMonitorCollection();
            random = new Random(0);
        }

        public void Train(double[][] features, double[][] labels, int epoches)
        {
            Monitors.OnTrainingStarted(this, epoches);

            for (int epoch = 0; epoch < epoches; epoch++)
            {
                random.Shuffle(features, labels);
                //double learningRate = CalculateLearningRate();

                for (int k = 0; k < features.Length; k++)
                {
                    double[] evaluation = Optimizer.Optimize(features[k], labels[k]);
                    Monitors.OnOptimized(features[k], labels[k], evaluation);
                }

                Monitors.OnEpochFinished(features, labels);
            }

            //monitors?.OnFinished(this, epoches);
        }
    }
}
