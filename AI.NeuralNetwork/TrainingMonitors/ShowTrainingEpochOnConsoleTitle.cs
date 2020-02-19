using System;

namespace SimpleNeuralNetwork
{
    public class ShowTrainingEpochOnConsoleTitle : TrainingMonitor
    {
        int epoches;
        int currentEpoch;

        public override void OnEpoch(double[][] features, double[][] labels)
        {
            currentEpoch++;
            Console.Title = $"{currentEpoch}/{epoches}";
        }

        public override void OnInit(Trainer optimizer, int epoches)
        {
            this.epoches = epoches;
            this.currentEpoch = 0;
            Console.Title = $"{currentEpoch}/{epoches}";
        }

        public override void OnOptimized(double[] features, double[] labels, double[] evaluation)
        {
        }
    }
}
