using System;

namespace Games.Go
{
    public class StoneColor
    {
        public static StoneColor Black { get; private set; }
        public static StoneColor White { get; private set; }

        static StoneColor()
        {
            Black = new StoneColor(FieldState.Black);
            White = new StoneColor(FieldState.White, Black);
        }

        public static StoneColor ParseGtp(string text)
        {
            if (text == "b" || text == "B")
            {
                return Black;
            }
            else if (text == "w" || text == "W")
            {
                return White;
            }
            else
            {
                throw new FormatException();
            }
        }

        public FieldState State { get; private set; }
        public StoneColor Opposite { get; private set; }

        private StoneColor(FieldState state)
        {
            State = state;
        }

        private StoneColor(FieldState state, StoneColor opposite)
        {
            State = state;
            Opposite = opposite;
            opposite.Opposite = this;
        }

        public string ToString(StoneColorFormat format)
        {
            switch (format)
            {
                default:
                case StoneColorFormat.SingleLetter:
                    return State == FieldState.Black ? "B" : "W";
            }
        }
    }

    public enum StoneColorFormat
    {
        SingleLetter
    }
}