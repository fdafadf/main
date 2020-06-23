using AI.TicTacToe;
using AI.NeuralNetworks.TicTacToe;
using AI.NeuralNetworks;
using AI.NeuralNetworks.Games;
using Games.TicTacToe;
using System;

namespace Demos.TicTacToe
{
    public class AccuracyMonitor : TrainingMonitor
    {
        TicTacToeValueNetwork Network;
        LabeledState<GameState, TicTacToeValue>[] TestData;

        public AccuracyMonitor(TicTacToeValueNetwork network)
        {
            Network = network;
        }

        public override void OnEpochFinished(Projection[] data)
        {
            CollectedData.Add(CalculateAccuracy(Network, TestData).Value);
        }

        public override void OnTrainingStarted(Trainer trainer, int epoches)
        {
            //Trainer = trainer;
        }

        public override void OnEvaluated(double[] features, double[] labels, double[] evaluation)
        {
        }

        public static Accuracy CalculateAccuracy(TicTacToeValueNetwork network, LabeledState<GameState, TicTacToeValue>[] data)
        {
            return CalculateAccuracy(network.Network.Evaluate, data);
        }

        public static Accuracy CalculateAccuracy(Func<double[], double[]> predictor, LabeledState<GameState, TicTacToeValue>[] data)
        {
            int correctPredictionCount = 0;

            for (int i = 0; i < data.Length; i++)
            {
                double[] prediction = predictor(data[i].Input);

                if (data[i].Label.IsPredictionCorrect(prediction))
                {
                    correctPredictionCount++;
                }
            }

            return new Accuracy(correctPredictionCount, data.Length);
        }

        public override string ToString()
        {
            return "ACC";
        }
    }
}
