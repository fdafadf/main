using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public interface IGameTreeNavigator<TGameTree, TGameState, TGameAction, TNode>
        where TGameTree : IGameTree<TGameState, TGameAction, TNode>
        where TGameAction : IGameAction
        where TNode : IGameTreeNode<TGameState, TGameAction, TNode>
    {
        TGameTree GameTree { get; }
        TNode CurrentNode { get; }
        event EventHandler<TGameAction> Forwarded;
        event EventHandler<GameTreePath<TGameAction>> Navigated;
        void Navigate(object sender, GameTreePath<TGameAction> track);
        void NavigateFromRoot(object sender, IEnumerable<TGameAction> forward);
        bool Forward(object sender, TGameAction action);
    }
}
