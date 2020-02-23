using AI.NeuralNetworks.Games;
using Games.TicTacToe;
using Games.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeResultProbabilities
    {
        readonly double[] UnitTie = new double[] { 1, 0, 0 };
        readonly double[] UnitCross = new double[] { 0, 1, 0 };
        readonly double[] UnitNought = new double[] { 0, 0, 1 };

        public readonly double[] Probabilities;

        public double Tie => Probabilities[0];
        public double Cross => Probabilities[1];
        public double Nought => Probabilities[2];

        public TicTacToeResultProbabilities(Player winner)
        {
            switch (winner?.FieldState ?? FieldState.Empty)
            {
                case FieldState.Cross:
                    Probabilities = UnitCross;
                    break;
                case FieldState.Nought:
                    Probabilities = UnitNought;
                    break;
                default:
                    Probabilities = UnitTie;
                    break;
            }
        }

        [JsonConstructor]
        public TicTacToeResultProbabilities(double[] probabilities)
        {
            Probabilities = probabilities;
        }

        public double this[Player player]
        {
            get
            {
                return Probabilities[player.IsCross ? 1 : 2];
            }
        }

        public FieldState Max
        {
            get
            {
                return (FieldState)Probabilities.IndexOfMax();
            }
        }

        public static int FindBest(IEnumerable<double[]> probabilities, Player player)
        {
            int currentPlayerIndex = player.IsCross ? 1 : 2;
            int currentOpponentIndex = player.IsCross ? 2 : 1;
            int[] possibilities = GetPossibilities(probabilities, player);

            if (possibilities[currentPlayerIndex] >= 0)
            {
                return possibilities[currentPlayerIndex];
            }
            else if (possibilities[0] >= 0)
            {
                return possibilities[0];
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
            int currentPlayerIndex = player.IsCross ? 1 : 2;
            int currentOpponentIndex = player.IsCross ? 2 : 1;
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
