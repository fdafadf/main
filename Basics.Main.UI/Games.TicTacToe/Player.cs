namespace Basics.Games.TicTacToe
{
    public class Player : IPlayer
    {
        public static Player Nought = new Player(FieldState.Nought);
        public static Player Cross = new Player(FieldState.Cross);

        static Player()
        {
            Player.Nought.Opposite = Player.Cross;
            Player.Cross.Opposite = Player.Nought;
        }

        public FieldState FieldState { get; private set; }
        public Player Opposite { get; private set; }

        private Player(FieldState state)
        {
            this.FieldState = state;
        }
    }
}
