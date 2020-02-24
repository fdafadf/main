﻿using AI.NeuralNetworks;
using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class MeanSquareErrorMonitor : TrainingMonitor
    {
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
