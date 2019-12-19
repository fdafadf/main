namespace Basics.Games
{
    public interface IGameState<TPlayer> where TPlayer : IPlayer
    {
        TPlayer CurrentPlayer { get; }
        bool IsFinal { get; }
        TPlayer GetWinner();
    }
}
