using System.Collections.Generic;

namespace Games
{
    public interface IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : IGameAction
    {
        TGameState Play(TGameState state, TGameAction action);
        IEnumerable<TGameAction> GetAllowedActions(TGameState state);
    }
}
