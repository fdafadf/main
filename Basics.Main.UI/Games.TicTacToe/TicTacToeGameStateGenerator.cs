using Basics.Main;
using Basics.Main.UI;
using System.Collections.Generic;

namespace Basics.Games.TicTacToe
{
    public class TicTacToeGameStateGenerator
    {
        public static TicTacToeGameStateGenerator Instance = new TicTacToeGameStateGenerator();

        TreeNode<GameState> fullTree;
        Dictionary<int, GameState> allUniqueStates;

        protected TicTacToeGameStateGenerator()
        {
        }

        public IEnumerable<GameState> GetAllUniqueStates()
        {
            if (allUniqueStates == null)
            {
                allUniqueStates = GetFullTree().Flatten().Unique(s => s.GetHashCode());
            }

            return allUniqueStates.Values;
        }

        public TreeNode<GameState> GetFullTree()
        {
            return fullTree ?? (fullTree = GetFullTree(new GameState()));
        }

        public TreeNode<GameState> GetFullTree(GameState gameState)
        {
            TreeNode<GameState> result = new TreeNode<GameState>(gameState);

            if (gameState.IsFinal == false)
            {
                foreach (GameAction action in TicTacToeGame.Instance.GetAllowedActions(gameState))
                {
                    TreeNode<GameState> child = GetFullTree(TicTacToeGame.Instance.Play(gameState, action));
                    result.Children.Add(child);
                }
            }

            return result;
        }
    }
}
