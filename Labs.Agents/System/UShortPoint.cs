using System;

namespace Labs.Agents
{
    public class UShortPoint
    {
        public ushort X { get; }
        public ushort Y { get; }

        public UShortPoint(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public UShortPoint Add(CardinalMovement move)
        {
            return new UShortPoint((ushort)(X + move.X), (ushort)(Y + move.Y));
        }

        public override int GetHashCode()
        {
            return ushort.MaxValue * X + Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is UShortPoint other)
            {
                return X == other.X && Y == other.Y;
            }
            else
            {
                return false;
            }
        }

        public int Distance(UShortPoint point)
        {
            return Math.Abs(X - point.X) + Math.Abs(Y - point.Y);
        }
    }
}
