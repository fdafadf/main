using Games;

namespace AI
{
    public interface IActionGenerator<TState, TAction, TPlayer> 
        where TState : IGameState<TPlayer> 
        where TPlayer : IPlayer 
        where TAction : IGameAction
    {
        TAction GenerateAction(TState gameState);
    }
}
