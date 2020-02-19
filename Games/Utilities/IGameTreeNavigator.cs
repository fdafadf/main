using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public interface IGameTreeNavigator<TNode, TState, TAction>
        where TAction : IGameAction
        where TNode : IGameTreeNode<TNode, TState, TAction>
    {
        TNode CurrentNode { get; }
        bool Forward(TNode childNode);
        bool Backward();
    }

    public interface IObservableGameTreeNavigator<TNode, TState, TAction>
        where TAction : IGameAction
        where TNode : IGameTreeNode<TNode, TState, TAction>
    {
        TNode CurrentNode { get; }
        event EventHandler<TAction> Forwarded;
        event EventHandler<GameTreePath<TAction>> Navigated;
        void Navigate(object sender, GameTreePath<TAction> track);
        void NavigateFromRoot(object sender, IEnumerable<TAction> forward);
        bool Forward(object sender, TAction action);
    }
}
