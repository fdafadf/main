using Games.TicTacToe;
using System.Collections.Generic;
using System.Linq;
using Games.Utilities;
using AI.NeuralNetworks.Games;

namespace AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeLabeledStateGenerator<TOutput>
    {
        public static TicTacToeLabeledStateGenerator<TOutput> Instance = new TicTacToeLabeledStateGenerator<TOutput>();

        protected TicTacToeLabeledStateGenerator()
        {
        }

        public IEnumerable<LabeledState<GameState, TOutput>> GetAllUniqueStates(ITreeValueEvaluator<GameState, LabeledState<GameState, TOutput>> outputFunction)
        {
            return GetFullTree(outputFunction).FlattenData().Unique(n => n.State.GetHashCode()).Values;
        }

        public TreeNode<LabeledState<GameState, TOutput>> GetFullTree(ITreeValueEvaluator<GameState, LabeledState<GameState, TOutput>> outputFunction)
        {
            return GetFullTree(TicTacToeGameStateGenerator.Instance.GetFullTree(), outputFunction);
        }

        private TreeNode<LabeledState<GameState, TOutput>> GetFullTree(GameTreeNode<GameState, GameAction> gameNode, ITreeValueEvaluator<GameState, LabeledState<GameState, TOutput>> outputFunction)
        {
            TreeNode<LabeledState<GameState, TOutput>> result;
        
            if (gameNode.Children.Count == 0)
            {
                var output = outputFunction.EvaluateLeaf(gameNode.State);
                result = new TreeNode<LabeledState<GameState, TOutput>>(output);
            }
            else
            {
                result = new TreeNode<LabeledState<GameState, TOutput>>();
        
                foreach (var childState in gameNode.Children)
                {
                    TreeNode<LabeledState<GameState, TOutput>> child = GetFullTree(childState.Value, outputFunction);
                    result.Children.Add(child);
                }

                IEnumerable<LabeledState<GameState, TOutput>> childrenStates = result.Children.Select(c => c.Data);
                result.Data = outputFunction.EvaluateNode(gameNode.State, childrenStates);
            }

            return result;
        }
    }
}
