using Games;

namespace AI
{
    public interface IActionGenerator<TState, TPlayer, TAction> 
        where TState : IGameState<TPlayer> 
        where TPlayer : IPlayer 
        where TAction : IGameAction
    {
        TAction GenerateAction(TState gameState);
    }
}
