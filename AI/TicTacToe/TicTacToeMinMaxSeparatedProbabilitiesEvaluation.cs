using Games;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeMinMaxSeparatedProbabilitiesEvaluation : IGameTreeEvaluator<GameState>
    {
        public double[] EvaluateLeaf(GameState finalState)
        {
            Player winner = finalState.GetWinner();
            return Result(winner?.FieldState ?? FieldState.Empty);
        }

        public double[] EvaluateNode(GameState nodeState, IEnumerable<IGameStateOutput<GameState>> children)
        {
            int currentPlayerIndex = nodeState.CurrentPlayer.IsCross ? 1 : 0;
            int currentOpponentIndex = nodeState.CurrentPlayer.IsCross ? 0 : 1;
            bool[] p = new[] { false, false, false };
            double[] result = new double[] { 0, 0, 0 };

            foreach (var child in children)
            {
                p[child.Output.IndexOfMax()] = true;
            }

            if (p[currentPlayerIndex])
            {
                result[currentPlayerIndex] = 1;
            }
            else if (p[2])
            {
                result[2] = 1;
            }
            else
            {
                result[currentOpponentIndex] = 1;
            }

            return result;
        }

        public static GameAction BestFor(Player currentPlayer, List<KeyValuePair<GameAction, double[]>> predictions)
        {
            int currentPlayerIndex = currentPlayer.IsCross ? 1 : 0;
            int currentOpponentIndex = currentPlayer.IsCross ? 0 : 1;
            int[] possibilities = new[] { -1, -1, -1 };

            for (int i = 0; i < predictions.Count; i++)
            {
                possibilities[predictions[i].Value.IndexOfMax()] = i;
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

        private static double[] Result(FieldState winner)
        {
            switch (winner)
            {
                case FieldState.Cross:
                    return Result(0, 1, 0);
                case FieldState.Nought:
                    return Result(1, 0, 0);
                default:
                    return Result(0, 0, 1);
            }
        }

        private static double[] Result(double nought, double cross, double draw)
        {
            return new double[] { nought, cross, draw };
        }
    }
}
