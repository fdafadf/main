using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public class ObservableGameTreeNavigator<TNavigator, TState, TAction, TPlayer, TNode>
        : IObservableGameTreeNavigator<TNode, TState, TAction>
        where TNavigator : IGameTreeNavigator<TNode, TState, TAction>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
        where TNode : IGameTreeNode<TNode, TState, TAction>
    {
        public event EventHandler<TAction> Forwarded;
        public event EventHandler<GameTreePath<TAction>> Navigated;
        //public TGameTree GameTree { get; }

        public TNavigator Navigator { get; }

        public ObservableGameTreeNavigator(TNavigator navigator)
        {
            Navigator = navigator;
        }

        public virtual TNode CurrentNode
        {
            get
            {
                return Navigator.CurrentNode;
            }
        }

        public bool Forward(object sender, TAction action)
        {
            return Forward(sender, action, true);
        }

        protected virtual void DoBackward()
        {
            Navigator.Backward();
            //currentNode = CurrentNode.Parent;
        }

        protected virtual void DoForward(TNode nextNode)
        {
            Navigator.Forward(nextNode);
            //currentNode = nextNode;
        }

        protected bool Forward(object sender, TAction action, bool raiseEvent)
        {
            bool result = false;

            if (CurrentNode.Children != null)
            {
                result = CurrentNode.Children.TryGetValue(action, out TNode node);

                if (result)
                {
                    DoForward(node);

                    if (raiseEvent)
                    {
                        Forwarded?.Invoke(sender, action);
                    }
                }
            }

            return result;
        }

        public void NavigateFromRoot(object sender, IEnumerable<TAction> forward)
        {
            while (CurrentNode.Parent != null)
            {
                DoBackward();
            }

            Navigate(sender, new GameTreePath<TAction>(GameTreePath<TAction>.Empty, forward));
        }

        public void Navigate(object sender, GameTreePath<TAction> track)
        {
            foreach (var item in track.Backward)
            {
                if (item.Equals(CurrentNode.LastAction))
                {
                    DoBackward();
                }
                else
                {
                    throw new Exception();
                }
            }

            foreach (var item in track.Forward)
            {
                if (Forward(sender, item, false) == false)
                {
                    throw new Exception();
                }
            }

            Navigated?.Invoke(sender, track);
        }
    }
}
