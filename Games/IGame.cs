using System.Collections.Generic;

namespace Games
{
    public interface IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        TState Play(TState state, TAction action);
        IEnumerable<TAction> GetAllowedActions(TState state);
    }
}
