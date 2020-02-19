//#define MOMENTUM

using System.Collections.Generic;

namespace SimpleNeuralNetwork
{
    public class TrainingMonitorCollection
    {
        public List<TrainingMonitor> Items { get; private set; }

        internal void OnTrainingStarted(Trainer optimizer, int epoches)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnInit(optimizer, epoches);
                }
            }
        }

        internal void OnOptimized(double[] features, double[] labels, double[] evaluation)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnOptimized(features, labels, evaluation);
                }
            }
        }

        internal void OnEpochFinished(double[][] features, double[][] labels)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnEpoch(features, labels);
                }
            }
        }

        public void Add(TrainingMonitor monitor)
        {
            if (Items == null)
            {
                Items = new List<TrainingMonitor>();
            }

            Items.Add(monitor);
        }
    }
}
