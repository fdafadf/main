using Games;

namespace AI.NeuralNetworks.Games
{
    public interface IGameAI<TGameState, TPlayer, TGameAction> where TGameState : IGameState<TPlayer> where TPlayer : IPlayer where TGameAction : IGameAction
    {
        TGameAction GenerateMove(TGameState gameState);
    }
}
