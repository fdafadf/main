using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe.NeuralNetworks
{
    public class TicTacToeTrainingData
    {
        public static void Load(Func<GameState, double[]> inputFunction, out double[][] inputs, out TicTacToeResultProbabilities[] expectedPredictions)
        {
            var trainingData = TicTacToeNeuralIOGenerator<TicTacToeResultProbabilities>.Instance.GetAllUniqueStates(inputFunction, new TicTacToeResultProbabilitiesEvaluator());
            inputs = trainingData.Select(d => d.Input).ToArray();
            expectedPredictions = trainingData.Select(d => d.Output).ToArray();
        }

        public static void Load(Func<FieldState, double> inputFunction, out double[][] inputs, out TicTacToeResultProbabilities[] expectedPredictions)
        {
            var trainingData = TicTacToeNeuralIOGenerator<TicTacToeResultProbabilities>.Instance.GetAllUniqueStates(inputFunction, new TicTacToeResultProbabilitiesEvaluator());
            inputs = trainingData.Select(d => d.Input).ToArray();
            expectedPredictions = trainingData.Select(d => d.Output).ToArray();
        }
        
        public static void Load(Func<FieldState, double> inputFunction, out double[][] inputs, out TicTacToeActionProbabilities[] expectedPredictions)
        {
            GameState gameState = new GameState();
            var gameTree = TicTacToeGameStateGenerator.Instance.GetFullTree();
            var evaluatedTree = new TicTacToeResultProbabilitiesEvaluator().Evaluate(gameTree);
            var flattenTree = evaluatedTree.Flatten().Where(n => n.Evaluation != null);
            var uniqueNodes = flattenTree.Unique(n => n.GameState.GetHashCode());
            inputs = uniqueNodes.Values.Select(n => n.GameState.ToArray(inputFunction)).ToArray();
            expectedPredictions = uniqueNodes.Values.Select(n => new TicTacToeActionProbabilities(n)).ToArray();
        }

        public static void Load(Func<FieldState, double> inputFunction, out double[][] inputs, out TicTacToePVNetworkOutput[] outputs)
        {
            GameState gameState = new GameState();
            var gameTree = TicTacToeGameStateGenerator.Instance.GetFullTree();
            var evaluatedTree = new TicTacToeResultProbabilitiesEvaluator().Evaluate(gameTree);
            var flattenTree = evaluatedTree.Flatten().Where(n => n.Evaluation != null);
            var uniqueNodes = flattenTree.Unique(n => n.GameState.GetHashCode());
            inputs = uniqueNodes.Values.Select(n => n.GameState.ToArray(inputFunction)).ToArray();
            outputs = uniqueNodes.Values.Select(TicTacToePVNetworkOutput.Create).ToArray();
        }

        public static bool IsPredictionCorrect(TicTacToeResultProbabilities expectedPrediction, double[] prediction)
        {
            return expectedPrediction.Probabilities.IndexOfMax() == prediction.IndexOfMax();
        }

        public static bool IsPredictionCorrect(TicTacToeResultProbabilities expectedPrediction, float[] predictions, int predictionIndex)
        {
            float[] output = new float[3];
            output[0] = predictions[predictionIndex * 3 + 0];
            output[1] = predictions[predictionIndex * 3 + 1];
            output[2] = predictions[predictionIndex * 3 + 2];
            return expectedPrediction.Probabilities.IndexOfMax() == output.IndexOfMax();
        }
    }
}
