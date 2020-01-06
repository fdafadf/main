using System.Collections.Generic;

namespace Games.Utilities
{
    public class GameTreeNode<TGameState, TGameAction>
        : GameTreeNode<TGameState, TGameAction, GameTreeNode<TGameState, TGameAction>>

    {
        public GameTreeNode(TGameState gameState, TGameAction lastAction, GameTreeNode<TGameState, TGameAction> parent) : base(gameState, lastAction, parent)
        {
        }
    }

    public abstract class GameTreeNode<TGameState, TGameAction, TNode> 
        : IGameTreeNode<TGameState, TGameAction, TNode>
        where TNode : class, IGameTreeNode<TGameState, TGameAction, TNode>
    {
        public TGameState GameState { get; }
        public TGameAction LastAction { get; }
        public TNode Parent { get; }
        public Dictionary<TGameAction, TNode> Children { get; set; }

        public GameTreeNode(TGameState gameState, TGameAction lastAction, TNode parent)
        {
            GameState = gameState;
            LastAction = lastAction;
            Parent = parent;
        }

        public IEnumerable<TNode> GetPath()
        {
            return GetPath(this as TNode);
        }

        public static IEnumerable<TNode> GetPath(TNode node)
        {
            LinkedList<TNode> result = new LinkedList<TNode>();

            while (node != null)
            {
                result.AddFirst(node);
                node = node.Parent;
            }

            return result;
        }
    }
}
