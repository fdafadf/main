namespace Games.Utilities
{
    public interface IGameStateOutput<TGameState>
    {
        TGameState GameState { get; }
        double[] Output { get; }
    }
}
