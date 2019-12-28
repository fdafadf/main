using Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeGameActionPrediction
    {
        public static List<KeyValuePair<GameAction, double[]>> Predict(GameState gameState, Func<FieldState, double> inputFunction, Func<double[], double[]> predictFunction)
        {
            double[] input = new double[9];
            Dictionary<GameAction, double[]> predictions = new Dictionary<GameAction, double[]>();

            foreach (GameAction gameAction in TicTacToeGame.Instance.GetAllowedActions(gameState))
            {
                GameState nextGameState = TicTacToeGame.Instance.Play(gameState, gameAction);
                nextGameState.ToArray(inputFunction, input);
                predictions.Add(gameAction, predictFunction(input));
            }

            return predictions.ToList();
        }

        public readonly GameState GameState;
        public readonly List<KeyValuePair<GameAction, double[]>> Predictions;
        public readonly GameAction BestAction;

        public TicTacToeGameActionPrediction(GameState gameState, List<KeyValuePair<GameAction, double[]>> predictions, GameAction bestAction)
        {
            GameState = gameState;
            Predictions = predictions;
            BestAction = bestAction;
        }

        //public GameAction GetBestGameAction()
        //{
        //    int bestActionIndex = TicTacToeWinningProbabilities.FindBest(Predictions.Select(p => p.Value), GameState.CurrentPlayer);
        //    return Predictions[bestActionIndex].Key;
        //}
    }
}
