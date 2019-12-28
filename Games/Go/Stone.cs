namespace Games.Go
{
    public class Stone
    {
        public static Stone Black { get; private set; }
        public static Stone White { get; private set; }

        static Stone()
        {
            Black = new Stone(StoneColor.Black);
            White = new Stone(StoneColor.White);
            Black.Opposite = White;
            White.Opposite = Black;
        }

        public StoneColor Color { get; private set; }
        public Stone Opposite { get; private set; }

        private Stone(StoneColor color)
        {
            Color = color;
        }
    }
}