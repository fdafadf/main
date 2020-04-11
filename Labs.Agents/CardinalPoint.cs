namespace Labs.Agents
{
    public class CardinalPoint
    {
        public static readonly CardinalPoint North;
        public static readonly CardinalPoint South;
        public static readonly CardinalPoint East;
        public static readonly CardinalPoint West;

        static CardinalPoint()
        {
            North = new CardinalPoint(0, -1);
            South = new CardinalPoint(0, 1);
            East = new CardinalPoint(-1, 0);
            West = new CardinalPoint(1, 0);
            North.Left = West;
            North.Right = East;
            South.Left = East;
            South.Right = West;
            East.Left = North;
            East.Right = South;
            West.Left = South;
            West.Right = North;
        }

        public CardinalPoint Left { get; private set; }
        public CardinalPoint Right { get; private set; }
        public readonly int X;
        public readonly int Y;

        private CardinalPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
