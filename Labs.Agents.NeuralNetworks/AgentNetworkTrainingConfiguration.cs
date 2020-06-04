using System;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetworkTrainingConfiguration
    {
        public double LearningRate { get; set; } = 0.001;
        public double Momentum { get; set; } = 0.04;
        public double Epsilon { get; set; } = 0.2;
        public double Gamma { get; set; } = 0.99;
        public int IterationLimit { get; set; } = 50000;
        public int FirstTrainingIteration { get; set; } = 256;
        public int HistorySubsetSize { get; set; } = 64;
        public int EpochesPerIteration { get; set; } = 1;
        public int BatchSize { get; set; } = 8;

        public override string ToString()
        {
            return $"𝛼: {LearningRate}, 𝑚: {Momentum}, 𝜀: {Epsilon}, 𝛾: {Gamma} batch: {HistorySubsetSize}";
        }

        public string ToToolTipString()
        {
            string[] lines =
            {
                $"SGD Learning Rate: {LearningRate}",
                $"SGD Momentum: {Momentum}",
                $"Q-Learning Epsilon: {Epsilon}",
                $"Q-Learning Gamma: {Gamma}",
                $"First Training Iteration: {FirstTrainingIteration}",
                $"History Subset Size: {HistorySubsetSize}",
                $"Epoches Per Iteration: {EpochesPerIteration}",
                $"Batch Size: {BatchSize}",
            };
            return string.Join("\r\n", lines);
        }
    }
}
