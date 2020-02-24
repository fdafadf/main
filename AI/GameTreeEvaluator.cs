using Games;
using Games.TicTacToe;
using Games.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace AI
{
    public abstract class GameTreeEvaluator<TEvaluation> : ITreeValueEvaluator<GameState, TEvaluation>
    {
        //TicTacToeMinMaxResultProbabilitiesEvaluator e = new TicTacToeMinMaxResultProbabilitiesEvaluator();
        //Func<TicTacToeActionProbabilities> factory;
        //IGame<GameState, GameAction, Player> Game;

        public GameTreeEvaluator()
        {
            //factory =>
            //{
            //    var p = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //    return new TicTacToeActionProbabilities(p, );
            //};
        }

        public GameTreeEvaluationNode<GameState, GameAction, TEvaluation> Evaluate(GameTreeNode<GameState, GameAction> node)
        {
            uint outputSize = node.State.BoardSize * node.State.BoardSize;
            var result = new GameTreeEvaluationNode<GameState, GameAction, TEvaluation>(node.State);

            if (node.State.IsFinal)
            {
                result.Evaluation = EvaluateLeaf(node.State);
            }
            else
            {
                result.Children = new Dictionary<GameAction, GameTreeEvaluationNode<GameState, GameAction, TEvaluation>>();

                foreach (var child in node.Children)
                {
                    result.Children.Add(child.Key, Evaluate(child.Value));
                }

                result.Evaluation = EvaluateNode(node.State, result.Children.Select(c => c.Value.Evaluation));

                //var allowedActions = Game.GetAllowedActions(node.GameState);
                //int playerIndex = node.GameState.CurrentPlayer.IsCross ? 1 : 0;
                //
                //if (allowedActions.Any())
                //{
                //    result.Children = new Dictionary<GameAction, GameTreeEvaluationNode<GameState, GameAction, TEvaluation>>();
                //
                //    foreach (var allowedAction in allowedActions)
                //    {
                //        var nextGameState = node.GameState.Play(allowedAction.X, allowedAction.Y);
                //        result.Children.Add(allowedAction, Evaluate(nextGameState));
                //    }
                //
                //    //result.Evaluation = EvaluateNode(gameState, result.Children);
                //
                //}
            }

            return result;
        }

        public abstract TEvaluation EvaluateLeaf(GameState finalState);

        public abstract TEvaluation EvaluateNode(GameState nodeState, IEnumerable<TEvaluation> evaluatedChildren);
    }
}
