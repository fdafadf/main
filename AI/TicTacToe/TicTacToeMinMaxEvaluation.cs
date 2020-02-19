using Games;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    //public class TicTacToeActionProbabilitiesEvaluator : GameTreeEvaluator<TicTacToeActionProbabilities>
    //{
    //    protected override TicTacToeActionProbabilities EvaluateLeaf(GameState finalState)
    //    {
    //        uint outputSize = finalState.BoardSize * finalState.BoardSize;
    //        //var result = new GameTreeEvaluationNode<TicTacToeActionProbabilities>(finalState);
    //        Player winner = finalState.GetWinner();
    //        TicTacToeResultProbabilities resultProbabilities = TicTacToeResultProbabilitiesEvaluator.Result(winner?.FieldState ?? FieldState.Empty);
    //        return new TicTacToeActionProbabilities(outputSize, resultProbabilities, null);
    //    }
    //
    //    protected override TicTacToeActionProbabilities EvaluateNode(GameState nodeState, IDictionary<GameAction, GameTreeEvaluationNode<GameState, GameAction, TicTacToeActionProbabilities>> evaluatedChildren)
    //    {
    //        uint outputSize = nodeState.BoardSize * nodeState.BoardSize;
    //        //TicTacToeResultProbabilities resultProbabilities = e.EvaluateNode(gameState, result.Children.Select(c => c.Value.Evaluation.Result));
    //        var result = new TicTacToeActionProbabilities(outputSize, null, a => GameAction.ToIndex(nodeState.BoardSize, a));
    //        
    //        foreach (var child in evaluatedChildren)
    //        {
    //            double p = 0.1;
    //        
    //            if (child.Value.Evaluation.Result[nodeState.CurrentPlayer] == 1)
    //            {
    //                p = 1;
    //            }
    //            else if (child.Value.Evaluation.Result.Tie == 1)
    //            {
    //                p = 0.5;
    //            }
    //        
    //            result[child.Key] = p;
    //        }
    //
    //        return result;
    //    }
    //}
}
