using System.Collections.Generic;

namespace Games.Utilities
{
    public interface IGameTreeNode<TGameState, TGameAction, TNode>
        where TNode : IGameTreeNode<TGameState, TGameAction, TNode>
    {
        TGameState GameState { get; }
        TGameAction LastAction { get; }
        TNode Parent { get; }
        Dictionary<TGameAction, TNode> Children { get; set; }
    }
}
