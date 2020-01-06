using System;
using System.Collections.Generic;

namespace Games.Utilities
{
    public class GameTreeNavigator<TGameTree, TGameState, TGameAction, TPlayer, TNode>
        : IGameTreeNavigator<TGameTree, TGameState, TGameAction, TNode>
        where TGameTree : IGameTree<TGameState, TGameAction, TNode>
        where TGameState : IGameState<TPlayer>
        where TGameAction : IGameAction
        where TPlayer : IPlayer
        where TNode : IGameTreeNode<TGameState, TGameAction, TNode>
    {
        public event EventHandler<TGameAction> Forwarded;
        public event EventHandler<GameTreePath<TGameAction>> Navigated;
        public TGameTree GameTree { get; }

        TNode currentNode;

        public GameTreeNavigator(TGameTree gameTree)
        {
            GameTree = gameTree;
            currentNode = gameTree.Root;
        }

        public virtual TNode CurrentNode
        {
            get
            {
                return currentNode;
            }
        }

        public bool Forward(object sender, TGameAction action)
        {
            return Forward(sender, action, true);
        }

        protected virtual void DoBackward()
        {
            currentNode = CurrentNode.Parent;
        }

        protected virtual void DoForward(TNode nextNode)
        {
            currentNode = nextNode;
        }

        protected bool Forward(object sender, TGameAction action, bool raiseEvent)
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

        public void NavigateFromRoot(object sender, IEnumerable<TGameAction> forward)
        {
            while (CurrentNode.Parent != null)
            {
                DoBackward();
            }

            Navigate(sender, new GameTreePath<TGameAction>(GameTreePath<TGameAction>.Empty, forward));
        }

        public void Navigate(object sender, GameTreePath<TGameAction> track)
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
