using Games.Utilities;
using System.Collections.Generic;

namespace Games.TicTacToe
{
    public class GameAction : IGameAction
    {
        public static int ToIndex(uint boardSize, GameAction action)
        {
            return action.X + (int)(action.Y * boardSize);
        }
        //private static Dictionary<ushort, GameAction>
        //
        //public static GameAction Create()
        //{
        //
        //}

        public readonly ushort X;
        public readonly ushort Y;

        public GameAction(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public GameAction(BoardCoordinates coordinates)
        {
            X = coordinates.X;
            Y = coordinates.Y;
        }

        public override string ToString()
        {
            return $"{{{X},{Y}}}";
        }

        public override bool Equals(object obj)
        {
            if (obj is GameAction action)
            {
                return X == action.X && Y == action.Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X * 1000 + Y;
        }
    }
}
