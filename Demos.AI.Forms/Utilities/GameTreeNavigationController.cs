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
    public class GameTreeNavigationController<TGame, TState, TAction, TPlayer, TNode>
        where TGame : IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
        where TNode : class, IGameTreeNode<TNode, TState, TAction>
    {
        HScrollBar scrollBar;
        IObservableGameTreeNavigator<TNode, TState, TAction> navigator1;
        List<TAction> path = new List<TAction>();
        int scrollBarPreviousValue;

        //public GameTreeNavigationController(IGameTreeNavigator<TGameTree, TGameState, TGameAction, TNode> navigator, HScrollBar scrollBar)
        public GameTreeNavigationController(IObservableGameTreeNavigator<TNode, TState, TAction> navigator, HScrollBar scrollBar)
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

        private void OnNavigated(object sender, GameTreePath<TAction> e)
        {
            if (sender != this)
            {
                path = GameTreeNode<TNode, TState, TAction>.GetPath(navigator1.CurrentNode).Skip(1).Select(n => n.LastAction).ToList();
                scrollBar.Maximum = path.Count;
                scrollBarPreviousValue = scrollBar.Maximum;
                scrollBar.Value = scrollBarPreviousValue;
            }
        }

        private void OnForwarded(object sender, TAction action)
        {
            if (sender != this)
            {
                // TODO : optymalizacja
                path = GameTreeNode<TNode, TState, TAction>.GetPath(navigator1.CurrentNode).Skip(1).Select(n => n.LastAction).ToList();
                scrollBar.Maximum = path.Count;
                scrollBarPreviousValue = scrollBar.Maximum;
                scrollBar.Value = scrollBarPreviousValue;
            }
        }

        private void ScrollBar_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"Previous: {scrollBarPreviousValue}");
            Console.WriteLine($"Current: {scrollBar.Value}");

            if (scrollBarPreviousValue != scrollBar.Value)
            {
                IEnumerable<TAction> undos = scrollBar.Value < scrollBarPreviousValue ? path.Skip(scrollBar.Value).Take(scrollBarPreviousValue - scrollBar.Value) : GameTreePath<TAction>.Empty;
                IEnumerable<TAction> plays = scrollBarPreviousValue < scrollBar.Value ? path.Skip(scrollBarPreviousValue).Take(scrollBar.Value - scrollBarPreviousValue) : GameTreePath<TAction>.Empty;
                //Navigated?.Invoke(this, new GameTreeTrack(undos, plays));
                navigator1.Navigate(this, new GameTreePath<TAction>(undos.Reverse(), plays));
                scrollBarPreviousValue = scrollBar.Value;
            }
        }
    }
}
