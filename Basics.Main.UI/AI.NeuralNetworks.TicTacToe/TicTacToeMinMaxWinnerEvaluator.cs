using Basics.Games;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System.Collections.Generic;
using System.Linq;

namespace Basics.AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeMinMaxWinnerEvaluator : IGameTreeEvaluator<GameState>
    {
        public double[] EvaluateLeaf(GameState finalState)
        {
            Player winner = finalState.GetWinner();
            return Result(winner?.FieldState ?? FieldState.Empty);
        }

        public double[] EvaluateNode(GameState nodeState, IEnumerable<IGameStateOutput<GameState>> children)
        {
            int index;

            if (nodeState.CurrentPlayer.IsCross)
            {
                index = children.IndexOfMin(c => c.Output[0]);
            }
            else
            {
                index = children.IndexOfMax(c => c.Output[0]);
            }

            return children.ElementAt(index).Output;
        }

        private static double[] CrossWon = new double[] { -1 };
        private static double[] NoughtWon = new double[] { 1 };
        private static double[] Draw = new double[] { 0 };

        private static double[] Result(FieldState winner)
        {
            switch (winner)
            {
                case FieldState.Cross:
                    return CrossWon;
                case FieldState.Nought:
                    return NoughtWon;
                default:
                    return Draw;
            }
        }
    }
}
