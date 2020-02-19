using AI.NeuralNetworks.Games;
using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Games.TicTacToe;
using Keras.Models;
using Python.Runtime;
using System;
using System.Linq;

namespace AI.Keras
{
    public class TicTacToeKerasAI : IGameAI<GameState, Player, GameAction>
    {
        KerasModel model;

        public TicTacToeKerasAI(string modelPath)
        {
            //PythonEngine.BeginAllowThreads();
            model = new KerasModel(() => BaseModel.LoadModel(modelPath));
        }

        public GameAction GenerateMove(GameState gameState)
        {
            Func<double[], TicTacToeResultProbabilities> predictFunction = input => new TicTacToeResultProbabilities(model.Predict(input).Select(i => (double)i).ToArray());
            var predictions = TicTacToeGameActionPrediction.Predict(gameState, TicTacToeNeuralIOLoader.InputFunctions.Bipolar, predictFunction);
            return TicTacToeResultProbabilitiesEvaluator.BestFor(gameState.CurrentPlayer, predictions.ToList());
        }
    }
}
