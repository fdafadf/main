using Basics.AI.NeuralNetworks.TicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Basics.Games.Demos.TicTacToe
{
    public class TicTacToePythonInterop
    {
        public static bool IsInitialized => TicTacToePredicateCallback != null;
        private static TicTacToePredicateCallback TicTacToePredicateCallback;

        public static TicTacToeGameActionPrediction Predict(GameState gameState)
        {
            var predictions = TicTacToeGameActionPrediction.Predict(gameState, TicTacToeNeuralIOLoader.InputFunctions.Bipolar, input => TicTacToePredicateCallback(input));
            int bestActionIndex = TicTacToeWinningProbabilities.FindBest(predictions.Select(p => p.Value), gameState.CurrentPlayer);
            GameAction bestAction = predictions[bestActionIndex].Key;
            return new TicTacToeGameActionPrediction(gameState, predictions, bestAction);
            //return new TicTacToeGameActionPrediction(gameState, predictions);

            //double[] input = new double[9];
            //Dictionary<GameAction, double[]> predictions = new Dictionary<GameAction, double[]>();
            //
            //foreach (GameAction gameAction in TicTacToeGame.Instance.GetAllowedActions(gameState))
            //{
            //    GameState nextGameState = TicTacToeGame.Instance.Play(gameState, gameAction);
            //    TicTacToeNeuralIO.ToInput(nextGameState, TicTacToeNeuralIO.InputFunctions.Bipolar, input);
            //    predictions.Add(gameAction, TicTacToePredicateCallback(input));
            //}
            //
            //return new TicTacToeGameActionPrediction(gameState, predictions);
        }

        public void Run(TicTacToePredicateCallback callback)
        {
            TicTacToePredicateCallback = callback;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public delegate double[] TicTacToePredicateCallback(double[] input);
}
