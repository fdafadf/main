using Basics.Games;
using Basics.Games.TicTacToe;
using Basics.Main;
using Basics.Main.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basics.AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeNeuralIOGenerator
    {
        public static TicTacToeNeuralIOGenerator Instance = new TicTacToeNeuralIOGenerator();

        protected TicTacToeNeuralIOGenerator()
        {
        }

        public IEnumerable<TicTacToeNeuralIO> GetAllUniqueStates(Func<FieldState, double> inputFunction, IGameTreeEvaluator<GameState> outputFunction)
        {
            return GetFullTree(inputFunction, outputFunction).Flatten().Unique(s => s.GameState.GetHashCode()).Values;
        }

        public TreeNode<TicTacToeNeuralIO> GetFullTree(Func<FieldState, double> inputFunction, IGameTreeEvaluator<GameState> outputFunction)
        {
            return GetFullTree(TicTacToeGameStateGenerator.Instance.GetFullTree(), inputFunction, outputFunction);
        }

        private TreeNode<TicTacToeNeuralIO> GetFullTree(TreeNode<GameState> gameNode, Func<FieldState, double> inputFunction, IGameTreeEvaluator<GameState> outputFunction)
        {
            TreeNode<TicTacToeNeuralIO> result;
        
            if (gameNode.Children.Count == 0)
            {
                double[] output = outputFunction.EvaluateLeaf(gameNode.Data);
                TicTacToeNeuralIO testData = new TicTacToeNeuralIO(gameNode.Data, inputFunction, output);
                result = new TreeNode<TicTacToeNeuralIO>(testData);
            }
            else
            {
                result = new TreeNode<TicTacToeNeuralIO>();
        
                foreach (var childState in gameNode.Children)
                {
                    TreeNode<TicTacToeNeuralIO> child = GetFullTree(childState, inputFunction, outputFunction);
                    result.Children.Add(child);
                }

                double[] output = outputFunction.EvaluateNode(gameNode.Data, result.Children.Select(c => c.Data));
                result.Data = new TicTacToeNeuralIO(gameNode.Data, inputFunction, output);
            }

            return result;
        }
    }
}
