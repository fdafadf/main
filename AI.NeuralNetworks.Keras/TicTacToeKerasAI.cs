using AI.NeuralNetworks.TicTacToe;
using AI.TicTacToe;
using Games.TicTacToe;
using Keras.Models;
using System;
using System.Linq;

namespace AI.Keras
{
    public class TicTacToeKerasAI : IActionGenerator<GameState, GameAction, Player>
    {
        KerasModel model;

        public TicTacToeKerasAI(string modelPath)
        {
            //PythonEngine.BeginAllowThreads();
            model = new KerasModel(() => BaseModel.LoadModel(modelPath));
        }

        public GameAction GenerateAction(GameState gameState)
        {
            Func<double[], TicTacToeValue> predictFunction = input => new TicTacToeValue(model.Predict(input).Select(i => (double)i).ToArray());
            var predictions = TicTacToeActionPrediction.Predict(gameState, TicTacToeLabeledStateLoader.InputTransforms.Bipolar, predictFunction);
            return TicTacToeValueEvaluator.BestFor(gameState.CurrentPlayer, predictions.ToList());
        }
    }
}
