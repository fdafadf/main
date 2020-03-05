using Games;
using Games.TicTacToe;
using Games.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeWinnerEvaluator : ITreeValueEvaluator<GameState, TicTacToeWinner>
    {
        public TicTacToeWinner EvaluateLeaf(GameState finalState)
        {
            Player winner = finalState.GetWinner();
            return Result(winner?.FieldState ?? FieldState.Empty);
        }

        public TicTacToeWinner EvaluateNode(GameState nodeState, IEnumerable<TicTacToeWinner> children)
        {
            int index;

            if (nodeState.CurrentPlayer.IsCross)
            {
                index = children.IndexOfMin(c => c.Value);
            }
            else
            {
                index = children.IndexOfMax(c => c.Value);
            }

            return children.ElementAt(index);
        }

        private static TicTacToeWinner CrossWon = new TicTacToeWinner(-1);
        private static TicTacToeWinner NoughtWon = new TicTacToeWinner(1);
        private static TicTacToeWinner Draw = new TicTacToeWinner(0);

        private static TicTacToeWinner Result(FieldState winner)
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
