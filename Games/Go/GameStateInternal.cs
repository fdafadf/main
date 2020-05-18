using System;
using System.Runtime.CompilerServices;

namespace Games.Go
{
    public class GameStateInternal
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

        public GameStateInternal(uint boardSize)
        {
            Ko = FieldCoordinates.Pass;
            BoardSize = boardSize;
            BoardFields = new FieldState[BoardSize, BoardSize];
        }

        public GameStateInternal(GameStateInternal state)
        {
            Ko = FieldCoordinates.Pass;
            CapturedStones = state.CapturedStones;
            BoardSize = state.BoardSize;
            BoardFields = new FieldState[BoardSize, BoardSize];
            Array.Copy(state.BoardFields, BoardFields, state.BoardFields.Length);
        }

        public GameStateInternal Pass()
        {
            return new GameStateInternal(this) { Ko = FieldCoordinates.Pass };
        }

        public GameStateInternal Play(uint x, uint y, Stone stone)
        {
            if (Inside(x, y))
            {
                if (BoardFields[x, y] == FieldState.Empty)
                {
                    GameStateInternal result = new GameStateInternal(this);
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
                                result.Ko = FieldCoordinates.Get(x, y - 1);
                            }
                            else if (capturedS > 0)
                            {
                                result.Ko = FieldCoordinates.Get(x, y + 1);
                            }
                            else if (capturedE > 0)
                            {
                                result.Ko = FieldCoordinates.Get(x + 1, y);
                            }
                            else if (capturedW > 0)
                            {
                                result.Ko = FieldCoordinates.Get(x - 1, y);
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
        public bool IsOutsideOrHasState(uint x, uint y, FieldState stone)
        {
            return Inside(x, y) == false || BoardFields[x, y] == stone;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsInsideAndHasState(uint x, uint y, FieldState stone)
        {
            return Inside(x, y) && BoardFields[x, y] == stone;
        }

        //public List<FieldCoordinates> GetAllowedActions(Stone stone)
        //{
        //    List<FieldCoordinates> legalMoves = new List<FieldCoordinates>();
        //    
        //    for (byte y = 0; y < BoardSize; y++)
        //    {
        //        for (byte x = 0; x < BoardSize; x++)
        //        {
        //            FieldCoordinates coordinates = FieldCoordinates.Get(x, y);
        //
        //            if (IsLegalMove(coordinates, stone))
        //            {
        //                legalMoves.Add(coordinates);
        //            }
        //        }
        //    }
        //
        //    return legalMoves;
        //}
    }
}