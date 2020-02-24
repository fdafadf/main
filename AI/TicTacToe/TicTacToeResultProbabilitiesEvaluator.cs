using AI.NeuralNetworks.Games;
using Games;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe
{
    /// <summary>
    /// Evaluates probabilities of nought win, cross win and draw in specified position.
    /// It uses Min-Max alghoritm.
    /// </summary>
    public class TicTacToeResultProbabilitiesEvaluator : ITreeValueEvaluator<GameState, LabeledState<GameState, TicTacToeResultProbabilities>>
    {
        Func<GameState, double[]> InputTransform { get; }

        public TicTacToeResultProbabilitiesEvaluator(Func<GameState, double[]> inputTransform)
        {
            InputTransform = inputTransform;
        }

        public GameTreeEvaluationNode<GameState, GameAction, LabeledState<GameState, TicTacToeResultProbabilities>> Evaluate(GameTreeNode<GameState, GameAction> node)
        {
            uint outputSize = node.State.BoardSize * node.State.BoardSize;
            var result = new GameTreeEvaluationNode<GameState, GameAction, LabeledState<GameState, TicTacToeResultProbabilities>>(node.State);

            if (node.State.IsFinal)
            {
                result.Evaluation = EvaluateLeaf(node.State);
            }
            else
            {
                result.Children = new Dictionary<GameAction, GameTreeEvaluationNode<GameState, GameAction, LabeledState<GameState, TicTacToeResultProbabilities>>>();

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

        public LabeledState<GameState, TicTacToeResultProbabilities> EvaluateLeaf(GameState finalState)
        {
            var typedOutput = new TicTacToeResultProbabilities(finalState.GetWinner());
            return new LabeledState<GameState, TicTacToeResultProbabilities>(finalState, InputTransform(finalState), typedOutput.Probabilities, typedOutput);
        }

        //public double[] EvaluateNode(GameState nodeState, IEnumerable<TicTacToeResultProbabilities> childrenProbabilities)
        //{
        //    return EvaluateNode(nodeState, childrenProbabilities.Select(c => c.Values));
        //}

        /// <summary>
        /// Children should be evaluated already.
        /// </summary>
        /// <param name="nodeState"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public LabeledState<GameState, TicTacToeResultProbabilities> EvaluateNode(GameState nodeState, IEnumerable<LabeledState<GameState, TicTacToeResultProbabilities>> evaluatedChildren)
        {
            int currentPlayerIndex = nodeState.CurrentPlayer.IsCross ? 1 : 2;
            int currentOpponentIndex = nodeState.CurrentPlayer.IsCross ? 2 : 1;
            bool[] p = new[] { false, false, false };
            double[] result = new double[] { 0, 0, 0 };

            foreach (var childOutputs in evaluatedChildren)
            {
                p[childOutputs.Label.Probabilities.IndexOfMax()] = true;
            }

            if (p[currentPlayerIndex])
            {
                result[currentPlayerIndex] = 1;
            }
            else if (p[0])
            {
                result[0] = 1;
            }
            else
            {
                result[currentOpponentIndex] = 1;
            }

            return new LabeledState<GameState, TicTacToeResultProbabilities>(nodeState, InputTransform(nodeState), result, new TicTacToeResultProbabilities(result));
        }

        public static GameAction BestFor(Player currentPlayer, List<KeyValuePair<GameAction, TicTacToeResultProbabilities>> predictions)
        {
            int currentPlayerIndex = currentPlayer.IsCross ? 1 : 2;
            int currentOpponentIndex = currentPlayer.IsCross ? 2 : 1;
            int[] possibilities = new[] { -1, -1, -1 };

            for (int i = 0; i < predictions.Count; i++)
            {
                possibilities[predictions[i].Value.Probabilities.IndexOfMax()] = i;
            }

            if (possibilities[currentPlayerIndex] >= 0)
            {
                return predictions[possibilities[currentPlayerIndex]].Key;
            }
            else if (possibilities[2] >= 0)
            {
                return predictions[possibilities[2]].Key;
            }
            else
            {
                return predictions[possibilities[currentOpponentIndex]].Key;
            }
        }
    }
}
