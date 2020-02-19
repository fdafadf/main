using Games.Utilities;
using System.Collections.Generic;

namespace Games.TicTacToe
{
    public class TicTacToeGameStateGenerator
    {
        public static TicTacToeGameStateGenerator Instance = new TicTacToeGameStateGenerator();

        GameTreeNode<GameState, GameAction> fullTree;
        Dictionary<int, GameState> allUniqueStates;

        protected TicTacToeGameStateGenerator()
        {
        }

        public IEnumerable<GameState> GetAllUniqueStates()
        {
            if (allUniqueStates == null)
            {
                //allUniqueStates = GetFullTree().FlattenData().Unique(s => s.GetHashCode());
            }

            return allUniqueStates.Values;
        }

        public GameTreeNode<GameState, GameAction> GetFullTree()
        {
            return fullTree ?? (fullTree = GetFullTree(new GameState(), null, null));
        }

        private GameTreeNode<GameState, GameAction> GetFullTree(GameState gameState, GameAction lastAction, GameTreeNode<GameState, GameAction> parentNode)
        {
            GameTreeNode<GameState, GameAction> result = new GameTreeNode<GameState, GameAction>(gameState, lastAction, parentNode);
            result.Children = new Dictionary<GameAction, GameTreeNode<GameState, GameAction>>();

            if (gameState.IsFinal == false)
            {
                foreach (GameAction action in TicTacToeGame.Instance.GetAllowedActions(gameState))
                {
                    GameTreeNode<GameState, GameAction> child = GetFullTree(TicTacToeGame.Instance.Play(gameState, action), action, result);
                    result.Children.Add(action, child);
                }
            }

            return result;
        }
    }
}
