//#define MOMENTUM

using System.Collections.Generic;

namespace AI.NeuralNetworks
{
    public class TrainingMonitorCollection
    {
        public List<TrainingMonitor> Items { get; private set; }

        public void OnTrainingStarted(Trainer optimizer, int epoches)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnTrainingStarted(optimizer, epoches);
                }
            }
        }

        public void OnTrainingFinished(long milisecondsElapsed)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnTrainingFinished(milisecondsElapsed);
                }
            }
        }

        public void OnEvaluated(double[] features, double[] labels, double[] evaluation)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnEvaluated(features, labels, evaluation);
                }
            }
        }

        public void OnEpochFinished(Projection[] data)
        {
            if (Items != null)
            {
                foreach (var monitor in Items)
                {
                    monitor.OnEpochFinished(data);
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

        public void AddRange(params TrainingMonitor[] monitors)
        {
            foreach (var monitor in monitors)
            {
                Add(monitor);
            }
        }
    }
}
