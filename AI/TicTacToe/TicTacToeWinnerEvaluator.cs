using Games;
using Games.TicTacToe;
using Games.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    public class TicTacToeWinnerEvaluator : ITreeValueEvaluator<GameState, TicTacToeWinnerPrediction>
    {
        public TicTacToeWinnerPrediction EvaluateLeaf(GameState finalState)
        {
            Player winner = finalState.GetWinner();
            return Result(winner?.FieldState ?? FieldState.Empty);
        }

        public TicTacToeWinnerPrediction EvaluateNode(GameState nodeState, IEnumerable<TicTacToeWinnerPrediction> children)
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

        private static TicTacToeWinnerPrediction CrossWon = new TicTacToeWinnerPrediction(-1);
        private static TicTacToeWinnerPrediction NoughtWon = new TicTacToeWinnerPrediction(1);
        private static TicTacToeWinnerPrediction Draw = new TicTacToeWinnerPrediction(0);

        private static TicTacToeWinnerPrediction Result(FieldState winner)
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
