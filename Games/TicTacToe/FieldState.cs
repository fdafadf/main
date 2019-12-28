using System;

namespace Games.TicTacToe
{
    public enum FieldState : ushort
    {
        Empty = 0,
        Cross = 1,
        Nought = 2
    }

    public static class FieldStateExtensions
    {
        public static Func<FieldState, T> Map<T>(T @default, T nought, T cross)
        {
            return fieldState =>
            {
                switch (fieldState)
                {
                    case FieldState.Cross:
                        return cross;
                    case FieldState.Nought:
                        return nought;
                    default:
                        return @default;
                }
            };
        }

        //public static Func<FieldState, T> Map<T>(this object self, T @default, T nought, T cross)
        //{
        //
        //}
    }
}
