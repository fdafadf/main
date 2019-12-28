namespace Games.Utilities
{
    public interface IGamePlayoutGenerator<TGameState, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
    {
        TGameState Generate(TGameState state);
    }
}
