namespace Labs.Agents
{
    public class CardinalMovement : IPoint
    {
        public static readonly CardinalMovement Nothing = new CardinalMovement(0, 0, 0);
        public static readonly CardinalMovement MoveNorth = new CardinalMovement(1, 0, 1);
        public static readonly CardinalMovement MoveSouth = new CardinalMovement(2, 0, -1);
        public static readonly CardinalMovement MoveEast = new CardinalMovement(3, 1, 0);
        public static readonly CardinalMovement MoveWest = new CardinalMovement(4, -1, 0);

        public static readonly CardinalMovement[] All = new CardinalMovement[]
        {
            Nothing,
            MoveNorth,
            MoveSouth,
            MoveEast,
            MoveWest,
        };

        static CardinalMovement()
        {
            Nothing.Opposite = Nothing;
            MoveNorth.Opposite = MoveSouth;
            MoveSouth.Opposite = MoveNorth;
            MoveEast.Opposite = MoveWest;
            MoveWest.Opposite = MoveEast;
        }

        public int Index { get; }
        public int X { get; }
        public int Y { get; }
        public CardinalMovement Opposite { get; private set; }

        private CardinalMovement(int index, int x, int y)
        {
            Index = index;
            X = x;
            Y = y;
        }
    }
}
