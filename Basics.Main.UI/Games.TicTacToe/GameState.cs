using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Basics.Games.TicTacToe
{
    public class GameState : IGameState<Player>
    {
        public static GameState Parse(string text)
        {
            GameState result = new GameState();
            string[] lines = text.Split('\n');
            int nonEmptyFields = 0;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    FieldState fieldState;

                    if (GameStatePrintSettings.Default.CrossCharater.Equals(lines[y][x]))
                    {
                        fieldState = FieldState.Cross;
                        nonEmptyFields++;
                    }
                    else if (GameStatePrintSettings.Default.NoughtCharater.Equals(lines[y][x]))
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
        //public Player Winner { get; private set; }
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

        //public bool IsFinal()
        //{
        //    return this.GetWinner().HasValue;
        //}

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
            StringBuilder builder = new StringBuilder();

            for (uint y = 0; y < this.BoardSize; y++)
            {
                for (uint x = 0; x < this.BoardSize; x++)
                {
                    char c;

                    switch (this.BoardFields[x, y])
                    {
                        case FieldState.Cross: c = GameStatePrintSettings.Default.CrossCharater; break;
                        case FieldState.Nought: c = GameStatePrintSettings.Default.NoughtCharater; break;
                        default: c = GameStatePrintSettings.Default.EmptyCharater; break;
                    }

                    builder.Append(c);
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }

    class GameStatePrintSettings
    {
        public static GameStatePrintSettings Default = new GameStatePrintSettings();

        public char CrossCharater = 'X';
        public char NoughtCharater = 'O';
        public char EmptyCharater = '.';
    }
}
