using System.Collections.Generic;

namespace Basics.Games
{
    public interface IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : IGameAction
    {
        TGameState Play(TGameState state, TGameAction action);
        IEnumerable<TGameAction> GetAllowedActions(TGameState state);
        //bool IsWinner(TGameState state, TPlayer player);
        //bool IsLooser(TGameState state, TPlayer player);
    }
}
