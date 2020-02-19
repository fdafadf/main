using System.Collections.Generic;

namespace Games.Utilities
{
    public class GameTreeNode<TState, TAction>
        : GameTreeNode<GameTreeNode<TState, TAction>, TState, TAction>

    {
        public GameTreeNode(TState gameState, TAction lastAction, GameTreeNode<TState, TAction> parent) : base(gameState, lastAction, parent)
        {
        }
    }

    public abstract class GameTreeNode<TNode, TState, TAction> 
        : IGameTreeNode<TNode, TState, TAction>
        where TNode : class, IGameTreeNode<TNode, TState, TAction>
    {
        public TState State { get; }
        public TAction LastAction { get; }
        public TNode Parent { get; }
        public Dictionary<TAction, TNode> Children { get; set; }

        public GameTreeNode(TState gameState, TAction lastAction, TNode parent)
        {
            State = gameState;
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
