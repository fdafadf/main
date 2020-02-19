using System.Collections.Generic;
using System.Text;

namespace Games.Utilities
{
    public interface ITreeValueEvaluator<TGameState, TValue>
    {
        //GameTreeEvaluationNode<TGameState, TGameAction, TEvaluation> Evaluate(TGameState gameState);
        TValue EvaluateLeaf(TGameState finalState);
        TValue EvaluateNode(TGameState nodeState, IEnumerable<TValue> evaluatedChildren);
    }

    public class GameTreeEvaluationNode<TGameState, TGameAction, TEvaluation> : TreeNode<GameTreeEvaluationNode<TGameState, TGameAction, TEvaluation>, TGameAction>
    {
        public TGameState GameState;
        //public Dictionary<GameAction, TicTacToeMinMaxEvaluationNode> Children;
        //public TicTacToeResultProbabilities ResultProbabilityInCurrentPosition;
        //public TicTacToeActionProbabilities CurrentPlayerWinningProbabilityAfterAction;
        public TEvaluation Evaluation;

        public GameTreeEvaluationNode(TGameState gameState)
        {
            GameState = gameState;
        }

        public IEnumerable<GameTreeEvaluationNode<TGameState, TGameAction, TEvaluation>> Flatten()
        {
            return Extensions2.Flatten<GameTreeEvaluationNode<TGameState, TGameAction, TEvaluation>, TGameAction, GameTreeEvaluationNode<TGameState, TGameAction, TEvaluation>>(this, t => t);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            //for (int y = 0; y < 3; y++)
            //{
            //    string boardLine = string.Empty;
            //    string actionProbabilitiesLine = string.Empty;
            //
            //    for (int x = 0; x < 3; x++)
            //    {
            //        FieldState s = GameState[x, y];
            //        boardLine += s == FieldState.Cross ? 'X' : (s == FieldState.Nought ? 'O' : '·');
            //        if (x > 0) { actionProbabilitiesLine += ','; }
            //        actionProbabilitiesLine += CurrentPlayerWinningProbabilityAfterAction.Probabilities[x + y * 3];
            //    }
            //
            //    builder.AppendLine($"{boardLine}  {actionProbabilitiesLine}  ");
            //}

            return builder.ToString();
        }
    }
}
