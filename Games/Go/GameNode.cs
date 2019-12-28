using System;
using System.Collections.Generic;
using System.Linq;

namespace Games.Go
{
    public class GameNode
    {
        public FieldCoordinates PreviousMove { get; }
        public Stone CurrentPlayer { get; }
        public GameState CurrentState { get; }

        public GameNode(FieldCoordinates previousMove, Stone currentPlayer, GameState currentState)
        {
            PreviousMove = previousMove;
            CurrentPlayer = currentPlayer;
            CurrentState = currentState;
        }

        //public static GameNode CreateRoot(uint boardSize)
        //{
        //    return new GameNode(new FieldCoordinates(), Stone.Black, new GameState(boardSize));
        //}

        public IEnumerable<FieldCoordinates> GetNextMoves()
        {
            return CurrentState.GetLegalMoves(CurrentPlayer).Where(move => IsTrueEye(move.X, move.Y, CurrentPlayer) == false);
        }

        //public static Stack<GameNode> RandomPlayout(uint boardSize)
        //{
        //    return GameNode.CreateRoot(boardSize).RandomPlayout();
        //}

        public Stack<GameNode> RandomPlayout()
        {
            Random random = new Random();
            Stack<GameNode> randomPlayout = new Stack<GameNode>();
            randomPlayout.Push(this);
            int passesInRow = 0;

            while (passesInRow < 2)
            {
                GameNode currentNode = randomPlayout.Peek();
                var legalMoves = currentNode.GetNextMoves().ToList();

                if (legalMoves.Any())
                {
                    var randomLegalMove = legalMoves[random.Next(0, legalMoves.Count - 1)];
                    randomPlayout.Push(currentNode.Play(randomLegalMove));
                    passesInRow = 0;
                }
                else
                {
                    randomPlayout.Push(currentNode.Pass());
                    passesInRow++;
                }
            }

            return randomPlayout;
        }

        private GameNode Pass()
        {
            return new GameNode(FieldCoordinates.Empty, CurrentPlayer.Opposite, CurrentState);
        }

        public GameNode Play(FieldCoordinates move)
        {
            GameState nextState = CurrentState.Play(move.X, move.Y, CurrentPlayer);
            return new GameNode(move, CurrentPlayer.Opposite, nextState);
        }

        private bool IsTrueEye(uint x, uint y, Stone stone)
        {
            return
                CurrentState.HasStateOrIsOutside(x + 1, y - 1, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x + 1, y, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x + 1, y + 1, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x, y + 1, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x, y - 1, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x - 1, y - 1, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x - 1, y, stone.Color.State) &&
                CurrentState.HasStateOrIsOutside(x - 1, y + 1, stone.Color.State);
        }
        
        public long CountTotalScoreForBlack()
        {
            GameScore score = new GameScore();
            CountTerritoryOnFinalPosition(ref score);
            return (long)score.Black - score.White + CurrentState.CapturedStones.White - CurrentState.CapturedStones.Black;
        }

        public void CountTerritoryOnFinalPosition(ref GameScore score)
        {
            for (uint y = 0; y < CurrentState.BoardSize; y++)
            {
                for (uint x = 0; x < CurrentState.BoardSize; x++)
                {
                    if (CurrentState.BoardFields[x, y] == FieldState.Empty)
                    {
                        if (CurrentState.IsInsideAndHasState(x - 1, y, FieldState.Black))
                        {
                            score.Black++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x - 1, y, FieldState.White))
                        {
                            score.White++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x + 1, y, FieldState.Black))
                        {
                            score.Black++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x + 1, y, FieldState.White))
                        {
                            score.White++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x, y - 1, FieldState.Black))
                        {
                            score.Black++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x, y - 1, FieldState.White))
                        {
                            score.White++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x, y + 1, FieldState.Black))
                        {
                            score.Black++;
                        }
                        else if (CurrentState.IsInsideAndHasState(x, y + 1, FieldState.White))
                        {
                            score.White++;
                        }
                    }
                }
            }
        }
    }
}