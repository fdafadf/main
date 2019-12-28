namespace Games.TicTacToe
{
    public class Player : IPlayer
    {
        public static Player Nought = new Player(FieldState.Nought);
        public static Player Cross = new Player(FieldState.Cross);

        static Player()
        {
            Nought.Opposite = Cross;
            Cross.Opposite = Nought;
        }

        public FieldState FieldState { get; private set; }
        public Player Opposite { get; private set; }

        private Player(FieldState state)
        {
            FieldState = state;
        }

        public bool IsCross
        {
            get
            {
                return FieldState == FieldState.Cross;
            }
        }
    }
}
