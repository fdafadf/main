//#define MOMENTUM

using System.Collections.Generic;

namespace AI.NeuralNetworks
{
    public abstract class TrainingMonitor
    {
        public List<double> CollectedData = new List<double>();

        public virtual void OnTrainingStarted(Trainer optimizer, int epoches)
        {

        }

        public virtual void OnTrainingFinished(long milisecondsElapsed)
        {

        }

        public abstract void OnEvaluated(double[] features, double[] labels, double[] evaluation);
        public abstract void OnEpochFinished(Projection[] data);
    }
}
