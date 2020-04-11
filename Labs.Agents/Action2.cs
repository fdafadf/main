namespace Labs.Agents
{
    public class Action2
    {
        public static readonly Action2 Nothing = new Action2(0, 0);
        public static readonly Action2 MoveNorth = new Action2(0, 1);
        public static readonly Action2 MoveSouth = new Action2(0, -1);
        public static readonly Action2 MoveEast = new Action2(1, 0);
        public static readonly Action2 MoveWest = new Action2(-1, 0);

        public static readonly Action2[] All = new Action2[]
        {
            Nothing,
            MoveNorth,
            MoveSouth,
            MoveEast,
            MoveWest,
        };

        public readonly int X;
        public readonly int Y;

        public Action2(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
