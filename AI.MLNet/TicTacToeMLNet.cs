using AI.NeuralNetworks;
using Games.TicTacToe;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using Games.Utilities;
using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using AI.NeuralNetworks.Games;

namespace Basics.MLNet
{
    public class TicTacToeMLNet : IGameAI<GameState, Player, GameAction>
    {
        PredictionEngine<ModelInput, ModelOutput> predictionEngine;

        public TicTacToeMLNet(string modelPath)
        {
            Type t0 = typeof(DataKind);
            Type t1 = typeof(StandardTrainersCatalog);
            Type t2 = typeof(LightGbmExtensions);
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        public double[] Predict(double[] input)
        {
            ModelInput modelInput = new ModelInput
            {
                Col1 = (float)input[0],
                Col2 = (float)input[1],
                Col3 = (float)input[2],
                Col4 = (float)input[3],
                Col5 = (float)input[4],
                Col6 = (float)input[5],
                Col7 = (float)input[6],
                Col8 = (float)input[7],
                Col9 = (float)input[8],
            };

            var prediction = predictionEngine.Predict(modelInput);
            float output = prediction.Score[1];
            return new double[] { output };
        }

        public GameAction GenerateMove(GameState gameState)
        {
            var predictions = TicTacToeGameActionPrediction.Predict(gameState, TicTacToeNeuralIOLoader.InputFunctions.Bipolar, Predict);
            int index;

            if (gameState.CurrentPlayer.IsCross)
            {
                index = predictions.IndexOfMin(p => p.Value[0]);
            }
            else
            {
                index = predictions.IndexOfMax(p => p.Value[0]);
            }

            return predictions[index].Key;
        }
    }
}
