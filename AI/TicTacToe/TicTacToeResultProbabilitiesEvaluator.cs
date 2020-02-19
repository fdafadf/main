using AI.NeuralNetworks.Games;
using Games;
using Games.TicTacToe;
using Games.Utilities;
using System.Collections.Generic;

namespace AI.TicTacToe
{
    /// <summary>
    /// Evaluates probabilities of nought win, cross win and draw in specified position.
    /// It uses Min-Max alghoritm.
    /// </summary>
    public class TicTacToeResultProbabilitiesEvaluator : GameTreeEvaluator<TicTacToeResultProbabilities>
    {
        //private GameStateNeuralIO<GameState, TicTacToeResultProbabilities> d;

        public override TicTacToeResultProbabilities EvaluateLeaf(GameState finalState)
        {
            return new TicTacToeResultProbabilities(finalState.GetWinner());
        }

        //public double[] EvaluateNode(GameState nodeState, IEnumerable<TicTacToeResultProbabilities> childrenProbabilities)
        //{
        //    return EvaluateNode(nodeState, childrenProbabilities.Select(c => c.Values));
        //}

        /// <summary>
        /// Children should be evaluated already.
        /// </summary>
        /// <param name="nodeState"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public override TicTacToeResultProbabilities EvaluateNode(GameState nodeState, IEnumerable<TicTacToeResultProbabilities> evaluatedChildren)
        {
            int currentPlayerIndex = nodeState.CurrentPlayer.IsCross ? 1 : 2;
            int currentOpponentIndex = nodeState.CurrentPlayer.IsCross ? 2 : 1;
            bool[] p = new[] { false, false, false };
            double[] result = new double[] { 0, 0, 0 };

            foreach (var childOutputs in evaluatedChildren)
            {
                p[childOutputs.Probabilities.IndexOfMax()] = true;
            }

            if (p[currentPlayerIndex])
            {
                result[currentPlayerIndex] = 1;
            }
            else if (p[0])
            {
                result[0] = 1;
            }
            else
            {
                result[currentOpponentIndex] = 1;
            }

            return new TicTacToeResultProbabilities(result);
        }

        public static GameAction BestFor(Player currentPlayer, List<KeyValuePair<GameAction, TicTacToeResultProbabilities>> predictions)
        {
            int currentPlayerIndex = currentPlayer.IsCross ? 1 : 2;
            int currentOpponentIndex = currentPlayer.IsCross ? 2 : 1;
            int[] possibilities = new[] { -1, -1, -1 };

            for (int i = 0; i < predictions.Count; i++)
            {
                possibilities[predictions[i].Value.Probabilities.IndexOfMax()] = i;
            }

            if (possibilities[currentPlayerIndex] >= 0)
            {
                return predictions[possibilities[currentPlayerIndex]].Key;
            }
            else if (possibilities[2] >= 0)
            {
                return predictions[possibilities[2]].Key;
            }
            else
            {
                return predictions[possibilities[currentOpponentIndex]].Key;
            }
        }
    }
}
