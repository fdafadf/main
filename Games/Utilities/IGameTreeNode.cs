using System.Collections.Generic;

namespace Games.Utilities
{
    public interface IGameTreeNode<TNode, TState, TAction>
        where TNode : IGameTreeNode<TNode, TState, TAction>
    {
        TNode Parent { get; }
        TState State { get; }
        TAction LastAction { get; }
        Dictionary<TAction, TNode> Children { get; set; }
    }
}
