namespace Basics.Games.TicTacToe
{
    public class GameAction : IGameAction
    {
        public ushort X;
        public ushort Y;

        public override string ToString()
        {
            return $"{{{X},{Y}}}";
        }
    }
}
