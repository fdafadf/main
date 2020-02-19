//#define MOMENTUM

using System.Collections.Generic;

namespace SimpleNeuralNetwork
{
    public abstract class TrainingMonitor
    {
        public List<double> CollectedData = new List<double>();

        public abstract void OnInit(Trainer optimizer, int epoches);
        public abstract void OnOptimized(double[] features, double[] labels, double[] evaluation);
        public abstract void OnEpoch(double[][] features, double[][] labels);
    }
}
