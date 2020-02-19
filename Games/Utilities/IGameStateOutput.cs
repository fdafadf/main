namespace Games.Utilities
{
    public interface IGameStateOutput<TGameState, TOutput>
    {
        TGameState GameState { get; }
        TOutput Output { get; }
    }
}
