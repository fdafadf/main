using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Games.Go
{
    public class GameState : IGameState<Stone>
    {
        private FieldCoordinates PreviousMove { get; }
        public Stone CurrentPlayer { get; }
        public GameStateInternal InternalState { get; }

        public GameState(uint boardSize)
        {
            PreviousMove = null;
            CurrentPlayer = Stone.Black;
            InternalState = new GameStateInternal(boardSize);
        }

        private GameState(FieldCoordinates previousMove, Stone currentPlayer, GameStateInternal internalState)
        {
            PreviousMove = previousMove;
            CurrentPlayer = currentPlayer;
            InternalState = internalState;
        }

        // Final State
        private GameState(Stone currentPlayer, GameStateInternal internalState)
        {
            PreviousMove = FieldCoordinates.Pass2;
            CurrentPlayer = currentPlayer;
            InternalState = internalState;
        }

        public bool IsFinal
        {
            get
            {
                return PreviousMove == FieldCoordinates.Pass2;
            }
        }

        public Stone GetWinner()
        {
            if (IsFinal)
            {
                return GameScore.Black > GameScore.White ? Stone.Black : Stone.White;
            }
            else
            {
                return null;
            }
        }

        Dictionary<FieldCoordinates, GameState> allowedActions;

        public IEnumerable<FieldCoordinates> GetAllowedActions()
        {
            if (allowedActions == null)
            {
                allowedActions = new Dictionary<FieldCoordinates, GameState>();

                if (IsFinal == false)
                {
                    for (byte y = 0; y < InternalState.BoardSize; y++)
                    {
                        for (byte x = 0; x < InternalState.BoardSize; x++)
                        {
                            FieldCoordinates field = FieldCoordinates.Get(x, y);

                            if (field != InternalState.Ko)
                            {
                                GameStateInternal nextState = InternalState.Play(field.X, field.Y, CurrentPlayer);

                                if (nextState != null)
                                {
                                    allowedActions.Add(field, new GameState(field, CurrentPlayer.Opposite, nextState));
                                }
                            }
                        }
                    }

                    if (PreviousMove == FieldCoordinates.Pass)
                    {
                        allowedActions.Add(FieldCoordinates.Pass, new GameState(CurrentPlayer.Opposite, InternalState.Pass()));
                    }
                    else
                    {
                        allowedActions.Add(FieldCoordinates.Pass, new GameState(FieldCoordinates.Pass, CurrentPlayer.Opposite, InternalState.Pass()));
                    }
                }
            }

            return allowedActions.Keys;
        }

        List<FieldCoordinates> allowedActionsForRandomPlayout;

        public IEnumerable<FieldCoordinates> GetAllowedActionsForRandomPlayout()
        {
            if (allowedActionsForRandomPlayout == null)
            {
                allowedActionsForRandomPlayout = new List<FieldCoordinates>();
                allowedActionsForRandomPlayout.AddRange(GetAllowedActions().Where(move => PlayoutOptimized.IsTrueEye(InternalState, move.X, move.Y, CurrentPlayer) == false));

                if (allowedActionsForRandomPlayout.Count > 1)
                {
                    allowedActionsForRandomPlayout.Remove(FieldCoordinates.Pass);
                }
            }

            return allowedActionsForRandomPlayout;
        }

        public Stack<GameState> RandomPlayout()
        {
            Random random = new Random();
            Stack<GameState> randomPlayout = new Stack<GameState>();
            randomPlayout.Push(this);
            GameState lastState = this;

            while (lastState.IsFinal == false)
            {
                var legalMoves = lastState.GetAllowedActionsForRandomPlayout().ToList();

                if (legalMoves.Any())
                {
                    lastState = lastState.Play(legalMoves[random.Next(0, legalMoves.Count - 1)]);
                }
                else
                {
                    lastState = lastState.allowedActions[FieldCoordinates.Pass];
                }

                randomPlayout.Push(lastState);

                //if (legalMoves.Any())
                //{
                //    var randomLegalMove = legalMoves[random.Next(0, legalMoves.Count - 1)];
                //    randomPlayout.Push(currentNode.Play(randomLegalMove));
                //    passesInRow = 0;
                //}
                //else
                //{
                //    randomPlayout.Push(currentNode.Pass());
                //    passesInRow++;
                //}
            }

            return randomPlayout;
        }

        public GameState Play(FieldCoordinates action)
        {
            GetAllowedActions();
            return allowedActions[action];
        }

        /// <summary>
        /// 1|2|3   Simple true eye detector.<br/>
        /// 4|0|5   0 is true eye for player P only if all 1-8<br/>
        /// 6|7|8   fields around are P or outside the board.<br/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="stone"></param>
        /// <returns></returns>
        

        public static class PlayoutOptimized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool IsTrueEye(GameStateInternal gameState, uint x, uint y, Stone stone)
            {
                int sum = 0;

                if (gameState.IsOutsideOrHasState(x + 1, y - 1, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x + 1, y, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x + 1, y + 1, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x, y + 1, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x, y - 1, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x - 1, y - 1, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x - 1, y, stone.Color.State)) sum++;
                if (gameState.IsOutsideOrHasState(x - 1, y + 1, stone.Color.State)) sum++;

                return sum >= 8;
            }

            private static int[] Ones = { -1, 1 };

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static FieldState GetPointWinner(GameStateInternal gameState, uint x, uint y)
            {
                if (gameState.BoardFields[x, y] == FieldState.Empty)
                {
                    FieldState expectedState = FieldState.Empty;

                    if (GetPointWinner_CheckFieldState(gameState, x + 1, y, ref expectedState)
                        && GetPointWinner_CheckFieldState(gameState, x - 1, y, ref expectedState)
                        && GetPointWinner_CheckFieldState(gameState, x, y + 1, ref expectedState)
                        && GetPointWinner_CheckFieldState(gameState, x, y - 1, ref expectedState))
                    {
                        return expectedState;
                    }
                }

                return FieldState.Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private static bool GetPointWinner_CheckFieldState(GameStateInternal gameState, uint x, uint y, ref FieldState expectedState)
            {
                if (gameState.Inside(x, y))
                {
                    FieldState fieldState = gameState.BoardFields[x, y];

                    if (fieldState == FieldState.Empty)
                    {
                        return false;
                    }
                    else
                    {
                        if (expectedState == FieldState.Empty)
                        {
                            expectedState = fieldState;
                            return true;
                        }
                        else if (expectedState != fieldState)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        //public long CountTotalScoreForBlack()
        //{
        //    GameScore score = new GameScore();
        //    CountTerritoryOnFinalPosition(ref score);
        //    return (long)score.Black - score.White + CurrentState.CapturedStones.White - CurrentState.CapturedStones.Black;
        //}

        GameScore gameScore;

        // Wersja działająca tylko dla finalnej pozycji playoutu, 
        // czyli posiadającej wyłącznie jednopunktowe oczy.
        private GameScore GameScore
        {
            get
            {
                if (gameScore == null)
                {
                    gameScore = new GameScore();

                    for (uint y = 0; y < InternalState.BoardSize; y++)
                    {
                        for (uint x = 0; x < InternalState.BoardSize; x++)
                        {
                            FieldState pointWinner = PlayoutOptimized.GetPointWinner(InternalState, x, y);

                            switch (pointWinner)
                            {
                                case FieldState.Black:
                                    gameScore.Black++;
                                    break;
                                case FieldState.White:
                                    gameScore.White++;
                                    break;
                            }
                        }
                    }

                    gameScore.Black += InternalState.CapturedStones.White;
                    gameScore.White += InternalState.CapturedStones.Black;
                }

                return gameScore;
            }
        }

        public override string ToString()
        {
            return ToString(CurrentPlayer);
        }

        public string ToString(Stone stone)
        {
            Dictionary<FieldState, string> labels = new Dictionary<FieldState, string>();
            labels[FieldState.Black] = "⬤";
            labels[FieldState.White] = "⭕";
            labels[FieldState.Empty] = "·";
            labels[FieldState.MarkedEmpty] = "⸭";
            labels[FieldState.MarkedStone] = "⊙";
            labels[FieldState.Black] = "X";
            labels[FieldState.White] = "O";
            labels[FieldState.Empty] = "·";
            labels[FieldState.MarkedEmpty] = "⸭";
            labels[FieldState.MarkedStone] = "⊙";
            //string illegal = "❌";
            string illegal = "i";
            StringBuilder builder = new StringBuilder();
            Func<FieldState, string> fieldToText = state => labels[state];

            for (uint y = 0; y < InternalState.BoardSize; y++)
            {
                for (uint x = 0; x < InternalState.BoardSize; x++)
                {
                    if (InternalState.BoardFields[x, y] == FieldState.Empty)
                    {
                        GetAllowedActions();

                        if (allowedActions.ContainsKey(FieldCoordinates.Get(x, y)))
                        {
                            builder.Append(fieldToText(InternalState.BoardFields[x, y]));
                        }
                        else
                        {
                            builder.Append(illegal);
                        }
                    }
                    else
                    {
                        builder.Append(fieldToText(InternalState.BoardFields[x, y]));
                    }
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}