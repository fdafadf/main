using AI.NeuralNetworks;
using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class MeanSquareErrorMonitor : TrainingMonitor
    {
        public static double CalculateError(Network network, Projection[] projections)
        {
            double cumulatedEpochError = 0;

            foreach (Projection projection in projections)
            {
                double[] prediction = network.Evaluate(projection.Input);
                double error = 0;

                for (int i = 0; i < projection.Output.Length; i++)
                {
                    double difference = projection.Output[i] - prediction[i];
                    error += difference * difference;
                }

                cumulatedEpochError += error / projection.Output.Length;
            }

            return cumulatedEpochError / projections.Length;
        }


        double cumulatedEpochError;

        public override void OnTrainingStarted(Trainer optimizer, int epoches)
        {
            cumulatedEpochError = 0;
        }

        public override void OnEvaluated(double[] features, double[] labels, double[] evaluation)
        {
            double error = 0;

            for (int i = 0; i < labels.Length; i++)
            {
                double difference = labels[i] - evaluation[i];
                error += difference * difference;
            }

            cumulatedEpochError += error / labels.Length;
        }

        public override void OnEpochFinished(Projection[] data)
        {
            double epochError = cumulatedEpochError / data.Length;
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
