namespace Basics.Games.TicTacToe
{
    static class Extensions
    {
        public static GameAction ToGameAction(this BoardCoordinates self)
        {
            return new GameAction { X = self.X, Y = self.Y };
        }
    }
}
