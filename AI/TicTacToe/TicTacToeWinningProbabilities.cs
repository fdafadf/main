using Games.TicTacToe;
using Games.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeWinningProbabilities
    {
        public readonly double[] Probabilities;
        public double Cross => Probabilities[1];
        public double Draw => Probabilities[2];
        public double Nought => Probabilities[0];

        public TicTacToeWinningProbabilities(double[] probabilities)
        {
            Probabilities = probabilities;
        }

        public static int FindBest(IEnumerable<double[]> probabilities, Player player)
        {
            int currentPlayerIndex = player.IsCross ? 1 : 0;
            int currentOpponentIndex = player.IsCross ? 0 : 1;
            int[] possibilities = GetPossibilities(probabilities, player);

            if (possibilities[currentPlayerIndex] >= 0)
            {
                return possibilities[currentPlayerIndex];
            }
            else if (possibilities[2] >= 0)
            {
                return possibilities[2];
            }
            else
            {
                return possibilities[currentOpponentIndex];
            }
        }

        public static double[] Merge(IEnumerable<double[]> probabilities, Player player)
        {
            return probabilities.ElementAt(FindBest(probabilities, player));
        }

        private static int[] GetPossibilities(IEnumerable<double[]> probabilities, Player player)
        {
            int currentPlayerIndex = player.IsCross ? 1 : 0;
            int currentOpponentIndex = player.IsCross ? 0 : 1;
            int[] possibilities = new[] { -1, -1, -1 };
            int[] result = new [] { -1, -1, -1 };
            int probabilityIndex = 0;

            foreach (var probability in probabilities)
            {
                possibilities[probability.IndexOfMax()] = probabilityIndex;
                probabilityIndex++;
            }

            return possibilities;
        }
    }
}
