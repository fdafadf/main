using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Games.Draughts
{
    class C
    {
        public static void Test(TextWriter writer)
        {

            ulong boardState1 = 0;
            writer.WriteLine(Draughts8State.ToString(boardState1));

            ulong boardState2 = Draughts8State.SetField(boardState1, 1, 0, DraughtsField.Black);
            writer.WriteLine(boardState2.ToStringBits());
            writer.WriteLine(Draughts8State.ToString(boardState2));

            ulong boardState3 = Draughts8State.SetField(boardState2, 2, 5, DraughtsField.White);
            writer.WriteLine(boardState3.ToStringBits());
            writer.WriteLine(Draughts8State.ToString(boardState3));

            ulong boardState4 = Draughts8State.SetField(boardState3, 2, 5, DraughtsField.Empty);
            writer.WriteLine(boardState4.ToStringBits());
            writer.WriteLine(Draughts8State.ToString(boardState4));
            Draughts8State.GetField(boardState3, 2, 5);
        }
    }

    enum DraughtsField
    {
        Empty = 0,
        Black = 1,
        White = 2
    }

    public static class Extensions2
    {
        public static string ToStringBits(this UInt64 self)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < 64; i++)
            {
                builder.Append((self & 1) == 1 ? '1' : '0');
                self = self >> 1;
            }

            return builder.ToString();
        }
    }

    class Draughts8Game : IGame<Draughts8State, Draughts8Action, Draughts8Player>
    {
        public IEnumerable<Draughts8Action> GetAllowedActions(Draughts8State state)
        {
            return null;
        }

        public Draughts8State Play(Draughts8State state, Draughts8Action action)
        {
            return state.Play(action);
        }
    }

    class Draughts8Player : IPlayer
    {
        public static Draughts8Player White = new Draughts8Player(DraughtsField.White);
        public static Draughts8Player Black = new Draughts8Player(DraughtsField.Black);

        static Draughts8Player()
        {
            White.Opposite = Black;
            Black.Opposite = White;
        }

        public DraughtsField Color { get; }
        public Draughts8Player Opposite { get; private set; }

        private Draughts8Player(DraughtsField color)
        {
            Color = color;
        }
    }

    class Draughts8Action : IGameAction
    {
        public List<Tuple<int, int>> Path = new List<Tuple<int, int>>();
    }

    class Draughts8State : IGameState<Draughts8Player>
    {
        public ulong Value;

        public Draughts8Player CurrentPlayer { get; }

        public bool IsFinal => throw new NotImplementedException();

        public Draughts8State(ulong value, Draughts8Player currentPlayer)
        {
            Value = value;
            CurrentPlayer = currentPlayer;
        }

        public Draughts8State Play(Draughts8Action action)
        {
            ulong newState = Value;

            foreach (var item in action.Path)
            {
                newState = SetField(newState, item.Item1, item.Item2, DraughtsField.Empty);
            }

            var lastField = action.Path.Last();
            SetField(newState, lastField.Item1, lastField.Item2, CurrentPlayer.Color);
            return new Draughts8State(newState, CurrentPlayer.Opposite);
        }

        public static ulong SetField(ulong boardState, int x, int y, DraughtsField fieldState)
        {
            x = (x + (y % 2)) / 2;
            int n = y * 8 + x * 2;
            boardState = boardState & ~((ulong)3 << n);
            return boardState | ((ulong)fieldState << n);
        }

        public static DraughtsField GetField(ulong boardState, int x, int y)
        {
            x = (x + (y % 2)) / 2;
            int n = y * 8 + x * 2;
            return (DraughtsField)((boardState >> n) & 3);
        }

        public static string ToString(ulong boardState)
        {
            StringBuilder builder = new StringBuilder();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        builder.Append('·');
                    }
                    else
                    {
                        switch (GetField(boardState, x, y))
                        {
                            case DraughtsField.Black:
                                builder.Append('X');
                                break;
                            case DraughtsField.White:
                                builder.Append('O');
                                break;
                            default:
                                builder.Append(' ');
                                break;
                        }
                    }
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public Draughts8Player GetWinner()
        {
            throw new NotImplementedException();
        }
    }
}
