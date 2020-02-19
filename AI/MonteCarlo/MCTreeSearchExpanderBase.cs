using Games;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.MonteCarlo
{
    public abstract class MCTreeSearchExpanderBase<TGame, TNode, TState, TAction, TPlayer> : IMCTreeSearchExpander<TNode>
        where TNode : MCTreeNode<TNode, TState, TAction>
        where TGame : IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        public TGame Game { get; }
        public Random Random { get; }

        public MCTreeSearchExpanderBase(TGame game, Random random)
        {
            Game = game;
            Random = random;
        }

        public TNode Expand(TNode node)
        {
            if (TryExpand(node))
            {
                return SelectExpanded(node);
            }
            else
            {
                return node;
            }
        }

        protected virtual void CreateChildren(TNode node)
        {
            node.Children = new Dictionary<TAction, TNode>();
            var actions = Game.GetAllowedActions(node.State);
            var states = actions.Select(action => Game.Play(node.State, action));

            foreach (TAction action in actions)
            {
                TState state = Game.Play(node.State, action);
                node.Children.Add(action, CreateNode(node, state, action));
            }
        }

        protected abstract TNode CreateNode(TNode parentNode, TState gameState, TAction action);

        protected abstract TNode SelectExpanded(TNode node);

        protected bool TryExpand(TNode node)
        {
            if (node.IsExpandedAndHasNoChildren)
            {
                return false;
            }

            if (node.IsUnexpanded)
            {
                CreateChildren(node);
                //node.Children = new Dictionary<TAction, MCTreeSearchNode<TState, TAction>>();
                //var actions = Game.GetAllowedActions(node.State);
                //var states = actions.Select(action => Game.Play(node.State, action));
                //
                //foreach (TAction action in actions)
                //{
                //    TState state = Game.Play(node.State, action);
                //    node.Children.Add(action, CreateNode(node, state, action));
                //}
            }

            return node.IsExpandedAndHasNoChildren == false;
        }
    }
}
