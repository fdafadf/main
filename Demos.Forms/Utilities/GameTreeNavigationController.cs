using AI.MonteCarlo;
using Demos.Forms.Go.Game;
using Games;
using Games.Go;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Demos.Forms.Utilities
{
    public class GameTreeNavigationController<TGameTree, TGame, TGameState, TGameAction, TPlayer, TNode>
        where TGameTree : IGameTree<TGameState, TGameAction, TNode>
        where TGame : IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TGameAction : IGameAction
        where TPlayer : IPlayer
        where TNode : class, IGameTreeNode<TGameState, TGameAction, TNode>
    {
        HScrollBar scrollBar;
        IGameTreeNavigator<TGameTree, TGameState, TGameAction, TNode> navigator1;
        List<TGameAction> path = new List<TGameAction>();
        int scrollBarPreviousValue;

        public GameTreeNavigationController(IGameTreeNavigator<TGameTree, TGameState, TGameAction, TNode> navigator, HScrollBar scrollBar)
        {
            this.scrollBar = scrollBar;
            this.scrollBar.Minimum = 0;
            this.scrollBar.Maximum = 0;
            scrollBarPreviousValue = -1;
            this.scrollBar.Value = 0;
            this.scrollBar.ValueChanged += ScrollBar_ValueChanged;
            this.navigator1 = navigator;
            this.navigator1.Forwarded += OnForwarded;
            this.navigator1.Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, GameTreePath<TGameAction> e)
        {
            if (sender != this)
            {
                path = GameTreeNode<TGameState, TGameAction, TNode>.GetPath(navigator1.CurrentNode).Skip(1).Select(n => n.LastAction).ToList();
                scrollBar.Maximum = path.Count;
                scrollBarPreviousValue = scrollBar.Maximum;
                scrollBar.Value = scrollBarPreviousValue;
            }
        }

        private void OnForwarded(object sender, TGameAction action)
        {
            if (sender != this)
            {
                // TODO : optymalizacja
                path = GameTreeNode<TGameState, TGameAction, TNode>.GetPath(navigator1.CurrentNode).Skip(1).Select(n => n.LastAction).ToList();
                scrollBar.Maximum = path.Count;
                scrollBarPreviousValue = scrollBar.Maximum;
                scrollBar.Value = scrollBarPreviousValue;
            }
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            if (scrollBarPreviousValue != scrollBar.Value)
            {
                IEnumerable<TGameAction> undos = scrollBar.Value < scrollBarPreviousValue ? path.Skip(scrollBar.Value).Take(scrollBarPreviousValue - scrollBar.Value) : GameTreePath<TGameAction>.Empty;
                IEnumerable<TGameAction> plays = scrollBarPreviousValue < scrollBar.Value ? path.Skip(scrollBarPreviousValue).Take(scrollBar.Value - scrollBarPreviousValue) : GameTreePath<TGameAction>.Empty;
                //Navigated?.Invoke(this, new GameTreeTrack(undos, plays));
                navigator1.Navigate(this, new GameTreePath<TGameAction>(undos, plays));
                scrollBarPreviousValue = scrollBar.Value;
            }
        }
    }
}
