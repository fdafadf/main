namespace Basics.Games
{
    public interface IGameStateOutput<TGameState>
    {
        TGameState GameState { get; }
        double[] Output { get; }
    }
}
