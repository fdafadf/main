namespace Labs.Agents
{
    public class CardinalMovement : IPoint
    {
        public static readonly CardinalMovement Nothing = new CardinalMovement("Nothing", 0, 0, 0);
        public static readonly CardinalMovement MoveNorth = new CardinalMovement("North", 1, 0, 1);
        public static readonly CardinalMovement MoveSouth = new CardinalMovement("South", 2, 0, -1);
        public static readonly CardinalMovement MoveEast = new CardinalMovement("East", 3, 1, 0);
        public static readonly CardinalMovement MoveWest = new CardinalMovement("West", 4, -1, 0);

        public static readonly CardinalMovement[] All = new CardinalMovement[]
        {
            Nothing,
            MoveNorth,
            MoveSouth,
            MoveEast,
            MoveWest,
        };

        public static readonly CardinalMovement[] AllExceptNothing;

        static CardinalMovement()
        {
            Nothing.Opposite = Nothing;
            MoveNorth.Opposite = MoveSouth;
            MoveSouth.Opposite = MoveNorth;
            MoveEast.Opposite = MoveWest;
            MoveWest.Opposite = MoveEast;

            AllExceptNothing = new CardinalMovement[]
            {
                MoveNorth,
                MoveSouth,
                MoveEast,
                MoveWest
            };
        }

        public string Name { get; }
        public int Index { get; }
        public int X { get; }
        public int Y { get; }
        public CardinalMovement Opposite { get; private set; }

        private CardinalMovement(string name, int index, int x, int y)
        {
            Name = name;
            Index = index;
            X = x;
            Y = y;
        }

        public override string ToString() => Name;
    }
}
