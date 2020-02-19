namespace Games
{
    public interface IGameState<TPlayer> : IPeriodState where TPlayer : IPlayer
    {
        TPlayer CurrentPlayer { get; }
        TPlayer GetWinner();
    }
}
