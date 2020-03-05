using System;
using TicTacToeLabeledState = AI.NeuralNetworks.Games.LabeledState<Games.TicTacToe.GameState, AI.TicTacToe.TicTacToeValue>;

namespace AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeValueNetworkTrainer
    {
        public IFunction ActivationFunction;
        public int[] HiddenLayerSizes;
        public double LearingRate;
        public double Momentum;
        public int BatchSize;
        public int Seed;
        public TicTacToeLabeledState[] TrainingData;

        public TicTacToeValueNetworkTrainer(TicTacToeLabeledState[] trainingData, int seed)
        {
            ActivationFunction = Function.ReLU;
            HiddenLayerSizes = new[] { 72, 72, 72, 36, 36, 36, 18, 18 };
            LearingRate = 0.001;
            Momentum = 0.04;
            BatchSize = 16;
            Seed = seed;
            TrainingData = trainingData; // TicTacToeResultProbabilitiesLoader.LoadAllUniqueStates(storage);
        }

        public TicTacToeValueNetwork Train(int epoches, params TrainingMonitor[] monitors)
        {
            var network = new TicTacToeValueNetwork(HiddenLayerSizes, ActivationFunction, new Random(Seed));
            var optimizer = new SGDMomentum(network.Network, LearingRate, Momentum);
            var trainer = new Trainer(optimizer, new Random(Seed));
            trainer.Monitors.AddRange(monitors);
            trainer.Train(TrainingData, epoches, 16);
            return network;
        }
    }
}
