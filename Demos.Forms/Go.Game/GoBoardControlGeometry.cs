using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Demos.Forms.Go.Game
{
    public struct GoBoardControlGeometry
    {
        public int FieldWidth;
        public int FieldHalfWidth;
        public int BoardWidth;

        public void Update(Size clientSize, ushort boardSize)
        {
            FieldWidth = (Math.Min(clientSize.Width, clientSize.Height) / boardSize);
            if (FieldWidth % 2 == 0) FieldWidth--;
            FieldHalfWidth = (FieldWidth - 1) / 2;
            BoardWidth = FieldWidth * boardSize;
        }

        public Point PointOnBoard;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPointOnBoard(Point point)
        {
            bool result = point.X > 0 && point.Y > 0 && point.X <= BoardWidth && point.Y <= BoardWidth;

            if (result)
            {
                PointOnBoard.X = point.X / FieldWidth;
                PointOnBoard.Y = point.Y / FieldWidth;
            }

            return result;
        }
    }
}
