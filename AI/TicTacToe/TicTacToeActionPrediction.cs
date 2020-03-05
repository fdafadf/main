using Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeActionPrediction
    {
        public static IDictionary<GameAction, TOutput> Predict<TOutput>(GameState gameState, Func<GameState, double[]> inputTransform, Func<double[], TOutput> predictFunction)
        {
            double[] input = new double[9];
            Dictionary<GameAction, TOutput> predictions = new Dictionary<GameAction, TOutput>();

            foreach (GameAction gameAction in TicTacToeGame.Instance.GetAllowedActions(gameState))
            {
                GameState nextGameState = TicTacToeGame.Instance.Play(gameState, gameAction);
                predictions.Add(gameAction, predictFunction(inputTransform(nextGameState)));
            }

            return predictions;
        }

        public readonly GameState GameState;
        public readonly List<KeyValuePair<GameAction, double[]>> Predictions;
        public readonly GameAction BestAction;

        public TicTacToeActionPrediction(GameState gameState, List<KeyValuePair<GameAction, double[]>> predictions, GameAction bestAction)
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
