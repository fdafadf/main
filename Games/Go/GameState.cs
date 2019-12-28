using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Games.Go
{
    public class GameState
    {
        public struct CapturedStonesState
        {
            public uint Black;
            public uint White;

            public void Add(Stone stone, uint numberOfStones)
            {
                if (stone.Color.State == FieldState.Black)
                {
                    White += numberOfStones;
                }
                else
                {
                    Black += numberOfStones;
                }
            }
        }

        public FieldState[,] BoardFields;
        public uint BoardSize { get; }
        public CapturedStonesState CapturedStones;
        public FieldCoordinates Ko { get; private set; }

        public GameState(uint boardSize)
        {
            Ko = FieldCoordinates.Empty;
            BoardSize = boardSize;
            BoardFields = new FieldState[BoardSize, BoardSize];
        }

        public GameState(GameState state)
        {
            Ko = FieldCoordinates.Empty;
            CapturedStones = state.CapturedStones;
            BoardSize = state.BoardSize;
            BoardFields = new FieldState[BoardSize, BoardSize];
            Array.Copy(state.BoardFields, BoardFields, state.BoardFields.Length);
        }

        public GameState Pass()
        {
            return new GameState(this) { Ko = FieldCoordinates.Empty };
        }

        public GameState Play(uint x, uint y, Stone stone)
        {
            if (Inside(x, y))
            {
                if (BoardFields[x, y] == FieldState.Empty)
                {
                    GameState result = new GameState(this);
                    uint capturedW = result.RemoveGroupIfHasOneBreath(x - 1, y, stone.Opposite.Color.State);
                    uint capturedE = result.RemoveGroupIfHasOneBreath(x + 1, y, stone.Opposite.Color.State);
                    uint capturedN = result.RemoveGroupIfHasOneBreath(x, y - 1, stone.Opposite.Color.State);
                    uint capturedS = result.RemoveGroupIfHasOneBreath(x, y + 1, stone.Opposite.Color.State);
                    uint capturedStones = capturedN + capturedS + capturedE + capturedW;
                    result.BoardFields[x, y] = stone.Color.State;
                    int breaths = result.MarkGroup(x, y, stone.Color.State);

                    if (breaths > 0)
                    {
                        result.ClearMarkedFields(stone.Color.State, FieldState.Empty);
                        result.CapturedStones.Add(stone, capturedStones);

                        if (capturedStones == 1 && breaths == 1)
                        {
                            if (capturedN > 0)
                            {
                                result.Ko = new FieldCoordinates(x, y - 1);
                            }
                            else if (capturedS > 0)
                            {
                                result.Ko = new FieldCoordinates(x, y + 1);
                            }
                            else if (capturedE > 0)
                            {
                                result.Ko = new FieldCoordinates(x + 1, y);
                            }
                            else if (capturedW > 0)
                            {
                                result.Ko = new FieldCoordinates(x - 1, y);
                            }
                        }

                        return result;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="boardFieldState"></param>
        /// <returns>Number of removed stones</returns>
        private uint RemoveGroupIfHasOneBreath(uint x, uint y, FieldState boardFieldState)
        {
            if (IsInsideAndHasState(x, y, boardFieldState))
            {
                int breaths = MarkGroup(x, y, boardFieldState);

                if (breaths == 1)
                {
                    return ClearMarkedFields(FieldState.Empty, FieldState.Empty);
                }

                ClearMarkedFields(boardFieldState, FieldState.Empty);
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="markedStoneReplacement"></param>
        /// <returns>Number of replaced marked stones.</returns>
        private uint ClearMarkedFields(FieldState markedStoneReplacement, FieldState markedEmptyFieldReplacement)
        {
            uint replacedStones = 0;

            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    switch (BoardFields[x, y])
                    {
                        case FieldState.MarkedStone:
                            replacedStones++;
                            BoardFields[x, y] = markedStoneReplacement;
                            break;
                        case FieldState.MarkedEmpty:
                            BoardFields[x, y] = markedEmptyFieldReplacement; // FieldState.Empty;
                            break;
                    }
                }
            }

            return replacedStones;
        }

        private int MarkGroup(uint x, uint y, FieldState stone)
        {
            if (Inside(x, y))
            {
                if (BoardFields[x, y] == stone)
                {
                    BoardFields[x, y] = FieldState.MarkedStone;
                    return MarkGroup(x - 1, y, stone) +
                           MarkGroup(x + 1, y, stone) +
                           MarkGroup(x, y - 1, stone) +
                           MarkGroup(x, y + 1, stone);
                }
                else if (BoardFields[x, y] == 0)
                {
                    BoardFields[x, y] = FieldState.MarkedEmpty;
                    return 1;
                }
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Inside(uint x, uint y)
        {
            return x < BoardSize && y < BoardSize;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasStateOrIsOutside(uint x, uint y, FieldState stone)
        {
            return Inside(x, y) == false || BoardFields[x, y] == stone;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInsideAndHasState(uint x, uint y, FieldState stone)
        {
            return Inside(x, y) && BoardFields[x, y] == stone;
        }

        public List<FieldCoordinates> GetLegalMoves(Stone stone)
        {
            List<FieldCoordinates> legalMoves = new List<FieldCoordinates>();
            
            for (byte y = 0; y < BoardSize; y++)
            {
                for (byte x = 0; x < BoardSize; x++)
                {
                    FieldCoordinates coordinates = new FieldCoordinates(x, y);

                    if (IsLegalMove(coordinates, stone))
                    {
                        legalMoves.Add(coordinates);
                    }
                }
            }

            return legalMoves;
        }

        private bool IsLegalMove(FieldCoordinates field, Stone stone)
        {
            return field.Equals(Ko) == false && Play(field.X, field.Y, stone) != null;
        }

        public string ToString(Stone stone)
        {
            Dictionary<FieldState, string> labels = new Dictionary<FieldState, string>();
            labels[FieldState.Black] = "⬤";
            labels[FieldState.White] = "⭕";
            labels[FieldState.Empty] = "·";
            labels[FieldState.MarkedEmpty] = "⸭";
            labels[FieldState.MarkedStone] = "⊙";
            string illegal = "❌";
            StringBuilder builder = new StringBuilder();
            Func<FieldState, string> fieldToText = state => labels[state];

            for (uint y = 0; y < BoardSize; y++)
            {
                for (uint x = 0; x < BoardSize; x++)
                {
                    if (BoardFields[x, y] == FieldState.Empty)
                    {
                        if (IsLegalMove(new FieldCoordinates(x, y), stone))
                        {
                            builder.Append(fieldToText(BoardFields[x, y]));
                        }
                        else
                        {
                            builder.Append(illegal);
                        }
                    }
                    else
                    {
                        builder.Append(fieldToText(BoardFields[x, y]));
                    }
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}