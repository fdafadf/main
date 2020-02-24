using Games.Utilities;
using System;
using System.Diagnostics;

namespace AI.NeuralNetworks
{
    public class Trainer
    {
        public Optimizer Optimizer { get; }
        public TrainingMonitorCollection Monitors { get; }
        protected Random random;

        public Trainer(Optimizer optimizer, Random random)
        {
            Optimizer = optimizer;
            Monitors = new TrainingMonitorCollection();
            this.random = random;
        }

        public void Train(double[][] features, double[][] labels, int epoches, int batchSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Monitors.OnTrainingStarted(this, epoches);

            for (int epoch = 0; epoch < epoches; epoch++)
            {
                random.Shuffle(features, labels);

                for (int k = 0; k < features.Length; k++)
                {
                    double[] evaluation = Optimizer.Evaluate(features[k], labels[k]);
                    Monitors.OnEvaluated(features[k], labels[k], evaluation);

                    if (k > 0 && k % batchSize == 0)
                    {
                        Optimizer.Update(batchSize);
                    }
                }

                if (features.Length % batchSize != 0)
                {
                    Optimizer.Update(batchSize);
                }

                Monitors.OnEpochFinished(features, labels);
            }

            stopwatch.Stop();
            Monitors.OnTrainingFinished(stopwatch.ElapsedMilliseconds);
        }
    }
}
