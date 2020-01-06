using System.Collections.Generic;

namespace Games.Utilities
{
    public abstract class GameTree<TGameState, TGameAction, TNode>
        : IGameTree<TGameState, TGameAction, TNode>
        where TGameAction : IGameAction
        where TNode : IGameTreeNode<TGameState, TGameAction, TNode>
    {
        public TNode Root { get; }

        public GameTree(TNode rootNode)
        {
            Root = rootNode; // CreateNode(rootState, rootAction, null);
        }

        //public void AddChildren(TNode parent, TNode child)
        //{
        //    parent.Children.Add(child.LastAction, child);
        //}

        //public TNode Expand(TNode node, TGameAction action)
        //{
        //    TGameState newGameState = Game.Play(node.GameState, action);
        //
        //    if (newGameState == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        if (node.Children == null)
        //        {
        //            node.Children = new Dictionary<TGameAction, TNode>();
        //        }
        //
        //        if (node.Children.TryGetValue(action, out TNode childNode) == false)
        //        {
        //            childNode = CreateNode(newGameState, action, node);
        //            node.Children.Add(action, childNode);
        //        }
        //
        //        return childNode;
        //    }
        //}

        //public abstract TNode CreateNode(TGameState gameState, TGameAction lastAction, TNode parentNode);
    }
}
