using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Basics.Games.TicTacToe
{
    public class TicTacToeGame : IGame<GameState, GameAction, Player>
    {
        public IEnumerable<GameAction> GetAllowedActions(GameState state)
        {
            return state.GetEmptyFields().Select(f => new GameAction { X = f.X, Y = f.Y });
        }

        public GameState Play(GameState state, GameAction action)
        {
            return state.Play(action.X, action.Y);
        }

        //public bool IsLooser(GameState state, Player player)
        //{
        //    var winner = state.GetWinner();
        //    return winner.HasValue && winner.Value == player.Opposite.FieldState;
        //}
        //
        //public bool IsWinner(GameState state, Player player)
        //{
        //    var winner = state.GetWinner();
        //    return winner.HasValue && winner.Value == player.FieldState;
        //}
    }
}
