using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Games.TicTacToe
{
    public class GameState : IGameState<Player>
    {
        public const char DefaultCrossCharater = 'X';
        public const char DefaultNoughtCharater = 'O';

        public static GameState Parse(string text)
        {
            return Parse(text.Split('\n'));
        }

        public static GameState Parse(params string[] lines)
        {
            GameState result = new GameState();
            int nonEmptyFields = 0;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    FieldState fieldState;

                    if (DefaultCrossCharater.Equals(lines[y][x]))
                    {
                        fieldState = FieldState.Cross;
                        nonEmptyFields++;
                    }
                    else if (DefaultNoughtCharater.Equals(lines[y][x]))
                    {
                        fieldState = FieldState.Nought;
                        nonEmptyFields++;
                    }
                    else
                    {
                        fieldState = FieldState.Empty;
                    }

                    result.BoardFields[x, y] = fieldState;
                }
            }

            result.CurrentPlayer = nonEmptyFields % 2 == 0 ? Player.Cross : Player.Nought;
            return result;
        }

        public Player CurrentPlayer { get; private set; }
        private FieldState[,] BoardFields;
        public uint BoardSize { get; }

        public GameState()
        {
            this.BoardSize = 3;
            this.BoardFields = new FieldState[this.BoardSize, this.BoardSize];
            this.CurrentPlayer = Player.Nought;
        }

        public GameState(GameState state)
        {
            this.BoardSize = state.BoardSize;
            this.BoardFields = new FieldState[this.BoardSize, this.BoardSize];
            Array.Copy(state.BoardFields, this.BoardFields, state.BoardFields.Length);
        }

        public FieldState this[int x, int y]
        {
            get
            {
                return BoardFields[x, y];
            }
        }

        public bool IsFinal
        {
            get
            {
                return this.GetWinner() != null || this.GetEmptyFields().Count() == 0;
            }
        }

        public GameState Play(uint x, uint y)
        {
            return this.Play(x, y, this.CurrentPlayer);
        }

        private GameState Play(uint x, uint y, Player player)
        {
            if (this.Inside(x, y))
            {
                if (this.BoardFields[x, y] == FieldState.Empty)
                {
                    GameState result = new GameState(this);
                    result.BoardFields[x, y] = player.FieldState;
                    result.CurrentPlayer = player.Opposite;
                    return result;
                }
            }

            return null;
        }

        public IEnumerable<BoardCoordinates> GetEmptyFields()
        {
            for (ushort y = 0; y < this.BoardSize; y++)
            {
                for (ushort x = 0; x < this.BoardSize; x++)
                {
                    if (this.BoardFields[x, y] == FieldState.Empty)
                    {
                        yield return new BoardCoordinates(x, y);
                    }
                }
            }
        }

        private static BoardCoordinates[][] Lines =
        {
            new BoardCoordinates[] { new BoardCoordinates(x: 0, y: 0), new BoardCoordinates(x: 1, y: 0), new BoardCoordinates(x: 2, y: 0) },
            new BoardCoordinates[] { new BoardCoordinates(x: 0, y: 1), new BoardCoordinates(x: 1, y: 1), new BoardCoordinates(x: 2, y: 1) },
            new BoardCoordinates[] { new BoardCoordinates(x: 0, y: 2), new BoardCoordinates(x: 1, y: 2), new BoardCoordinates(x: 2, y: 2) },
            new BoardCoordinates[] { new BoardCoordinates(x: 0, y: 0), new BoardCoordinates(x: 0, y: 1), new BoardCoordinates(x: 0, y: 2) },
            new BoardCoordinates[] { new BoardCoordinates(x: 1, y: 0), new BoardCoordinates(x: 1, y: 1), new BoardCoordinates(x: 1, y: 2) },
            new BoardCoordinates[] { new BoardCoordinates(x: 2, y: 0), new BoardCoordinates(x: 2, y: 1), new BoardCoordinates(x: 2, y: 2) },
            new BoardCoordinates[] { new BoardCoordinates(x: 0, y: 0), new BoardCoordinates(x: 1, y: 1), new BoardCoordinates(x: 2, y: 2) },
            new BoardCoordinates[] { new BoardCoordinates(x: 2, y: 0), new BoardCoordinates(x: 1, y: 1), new BoardCoordinates(x: 0, y: 2) },
        };

        public Player GetWinner()
        {
            foreach (BoardCoordinates[] line in Lines)
            {
                if (this.Equals(line, FieldState.Cross))
                {
                    return Player.Cross;
                }
                else if (this.Equals(line, FieldState.Nought))
                {
                    return Player.Nought;
                }
            }

            return null;
        }

        public T[] ToArray<T>(Func<FieldState, T> inputFunction)
        {
            T[] result = new T[9];
            ToArray(inputFunction, result);
            return result;
        }

        public void ToArray<T>(Func<FieldState, T> inputFunction, T[] result)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    result[y * BoardSize + x] = inputFunction(this[x, y]);
                }
            }
        }

        private bool Equals(BoardCoordinates[] fields, FieldState state)
        {
            return fields.All(f => this.BoardFields[f.X, f.Y] == state);
        }

        public override bool Equals(object obj)
        {
            GameState otherState = obj as GameState;

            if (obj != null)
            {
                for (uint y = 0; y < this.BoardSize; y++)
                {
                    for (uint x = 0; x < this.BoardSize; x++)
                    {
                        if (this.BoardFields[x, y] != otherState.BoardFields[x, y])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Inside(uint x, uint y)
        {
            return x < this.BoardSize && y < this.BoardSize;
        }

        public override string ToString()
        {
            return ToString('.', DefaultNoughtCharater, DefaultCrossCharater);
        }

        public virtual string ToString(char empty, char nought, char cross)
        {
            StringBuilder builder = new StringBuilder();
            Func<FieldState, char> charMap = FieldStateExtensions.Map(empty, nought, cross);

            for (uint y = 0; y < BoardSize; y++)
            {
                if (y > 0)
                {
                    builder.AppendLine();
                }

                for (uint x = 0; x < BoardSize; x++)
                {
                    builder.Append(charMap(BoardFields[x, y]));
                }
            }

            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return (int)BoardFields[0, 0]
                + 10 * (int)BoardFields[0, 1]
                + 100 * (int)BoardFields[0, 2]
                + 1000 * (int)BoardFields[1, 0]
                + 10000 * (int)BoardFields[1, 1]
                + 100000 * (int)BoardFields[1, 2]
                + 1000000 * (int)BoardFields[2, 0]
                + 10000000 * (int)BoardFields[2, 1]
                + 100000000 * (int)BoardFields[2, 2];
        }
    }
}
