using Games;
using Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using Games.Utilities;
using AI.NeuralNetworks;
using AI.NeuralNetworks.Games;

namespace AI.TicTacToe.NeuralNetworks
{
    public class TicTacToeNeuralIOGenerator
    {
        public static TicTacToeNeuralIOGenerator Instance = new TicTacToeNeuralIOGenerator();

        protected TicTacToeNeuralIOGenerator()
        {
        }

        public IEnumerable<GameStateNeuralIO<GameState>> GetAllUniqueStates(Func<FieldState, double> inputFunction, IGameTreeEvaluator<GameState> outputFunction)
        {
            return GetFullTree(inputFunction, outputFunction).Flatten().Unique(s => s.GameState.GetHashCode()).Values;
        }

        public TreeNode<GameStateNeuralIO<GameState>> GetFullTree(Func<FieldState, double> inputFunction, IGameTreeEvaluator<GameState> outputFunction)
        {
            return GetFullTree(TicTacToeGameStateGenerator.Instance.GetFullTree(), inputFunction, outputFunction);
        }

        private TreeNode<GameStateNeuralIO<GameState>> GetFullTree(TreeNode<GameState> gameNode, Func<FieldState, double> inputFunction, IGameTreeEvaluator<GameState> outputFunction)
        {
            TreeNode<GameStateNeuralIO<GameState>> result;
        
            if (gameNode.Children.Count == 0)
            {
                double[] output = outputFunction.EvaluateLeaf(gameNode.Data);
                GameStateNeuralIO<GameState> testData = new GameStateNeuralIO<GameState>(gameNode.Data, gameNode.Data.ToArray(inputFunction), output);
                result = new TreeNode<GameStateNeuralIO<GameState>>(testData);
            }
            else
            {
                result = new TreeNode<GameStateNeuralIO<GameState>>();
        
                foreach (var childState in gameNode.Children)
                {
                    TreeNode<GameStateNeuralIO<GameState>> child = GetFullTree(childState, inputFunction, outputFunction);
                    result.Children.Add(child);
                }

                double[] output = outputFunction.EvaluateNode(gameNode.Data, result.Children.Select(c => c.Data));
                result.Data = new GameStateNeuralIO<GameState>(gameNode.Data, gameNode.Data.ToArray(inputFunction), output);
            }

            return result;
        }
    }
}
