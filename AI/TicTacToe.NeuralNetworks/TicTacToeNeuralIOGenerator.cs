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
    public class TicTacToeNeuralIOGenerator<TOutput>
    {
        public static TicTacToeNeuralIOGenerator<TOutput> Instance = new TicTacToeNeuralIOGenerator<TOutput>();

        protected TicTacToeNeuralIOGenerator()
        {
        }

        public IEnumerable<GameStateNeuralIO<GameState, TOutput>> GetAllUniqueStates(Func<FieldState, double> inputFunction, ITreeValueEvaluator<GameState, TOutput> outputFunction)
        {
            return GetFullTree(inputFunction, outputFunction).FlattenData().Unique(s => s.GameState.GetHashCode()).Values;
        }

        public IEnumerable<GameStateNeuralIO<GameState, TOutput>> GetAllUniqueStates(Func<GameState, double[]> inputFunction, ITreeValueEvaluator<GameState, TOutput> outputFunction)
        {
            return GetFullTree(inputFunction, outputFunction).FlattenData().Unique(s => s.GameState.GetHashCode()).Values;
        }

        public TreeNode<GameStateNeuralIO<GameState, TOutput>> GetFullTree(Func<FieldState, double> inputFunction, ITreeValueEvaluator<GameState, TOutput> outputFunction)
        {
            return GetFullTree(TicTacToeGameStateGenerator.Instance.GetFullTree(), inputFunction, outputFunction);
        }

        public TreeNode<GameStateNeuralIO<GameState, TOutput>> GetFullTree(Func<GameState, double[]> inputFunction, ITreeValueEvaluator<GameState, TOutput> outputFunction)
        {
            return GetFullTree(TicTacToeGameStateGenerator.Instance.GetFullTree(), inputFunction, outputFunction);
        }

        private TreeNode<GameStateNeuralIO<GameState, TOutput>> GetFullTree(GameTreeNode<GameState, GameAction> gameNode, Func<FieldState, double> inputFunction, ITreeValueEvaluator<GameState, TOutput> outputFunction)
        {
            return GetFullTree(gameNode, gameState => gameState.ToArray(inputFunction), outputFunction);
        }

        private TreeNode<GameStateNeuralIO<GameState, TOutput>> GetFullTree(GameTreeNode<GameState, GameAction> gameNode, Func<GameState, double[]> inputFunction, ITreeValueEvaluator<GameState, TOutput> outputFunction)
        {
            TreeNode<GameStateNeuralIO<GameState, TOutput>> result;
        
            if (gameNode.Children.Count == 0)
            {
                TOutput output = outputFunction.EvaluateLeaf(gameNode.State);
                GameStateNeuralIO<GameState, TOutput> testData = new GameStateNeuralIO<GameState, TOutput>(gameNode.State, inputFunction(gameNode.State), output);
                result = new TreeNode<GameStateNeuralIO<GameState, TOutput>>(testData);
            }
            else
            {
                result = new TreeNode<GameStateNeuralIO<GameState, TOutput>>();
        
                foreach (var childState in gameNode.Children)
                {
                    TreeNode<GameStateNeuralIO<GameState, TOutput>> child = GetFullTree(childState.Value, inputFunction, outputFunction);
                    result.Children.Add(child);
                }

                TOutput output = outputFunction.EvaluateNode(gameNode.State, result.Children.Select(c => c.Data.Output));
                result.Data = new GameStateNeuralIO<GameState, TOutput>(gameNode.State, inputFunction(gameNode.State), output);
            }

            return result;
        }
    }
}
