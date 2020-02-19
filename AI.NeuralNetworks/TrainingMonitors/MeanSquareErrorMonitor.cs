using AI.NeuralNetwork;
using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetwork
{
    public class MeanSquareErrorMonitor : TrainingMonitor
    {
        double cumulatedEpochError;

        public override void OnInit(Trainer optimizer, int epoches)
        {
            cumulatedEpochError = 0;
        }

        public override void OnOptimized(double[] features, double[] labels, double[] evaluation)
        {
            double error = 0;

            for (int i = 0; i < labels.Length; i++)
            {
                double difference = labels[i] - evaluation[i];
                error += difference * difference;
            }

            cumulatedEpochError += error / labels.Length;
        }

        public override void OnEpoch(double[][] features, double[][] labels)
        {
            double epochError = cumulatedEpochError / features.Length;
            CollectedData.Add(epochError);
            cumulatedEpochError = 0;
            OnEpoch(epochError);
        }

        protected virtual void OnEpoch(double meanSquaredError)
        {
            cumulatedEpochError += meanSquaredError;
        }

        public override string ToString()
        {
            return "MSE";
        }
    }
}
