using System;
using System.IO;

namespace AI.NeuralNetwork
{
    public class TrainingEpochMonitor : TrainingMonitor
    {
        int epoches;
        int currentEpoch;
        Action<string> target;
        TextWriter writer;

        public TrainingEpochMonitor(Action<string> target, TextWriter writer)
        {
            this.target = target;
            this.writer = writer;
        }

        public override void OnEpoch(double[][] features, double[][] labels)
        {
            currentEpoch++;
            target($"{currentEpoch}/{epoches}");
        }

        public override void OnTrainingStarted(Trainer optimizer, int epoches)
        {
            this.epoches = epoches;
            this.currentEpoch = 0;
            target($"{currentEpoch}/{epoches}");
        }

        public override void OnTrainingFinished(long milisecondsElapsed)
        {
            writer.WriteLine($"Training time: {milisecondsElapsed}");
        }

        public override void OnEvaluated(double[] features, double[] labels, double[] evaluation)
        {
        }
    }
}
