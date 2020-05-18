using System.Collections.Generic;
using System.Linq;
using Games;
using Games.Utilities;

namespace AI.MonteCarlo
{
    public class PVNetworkBasedMCTreeSearchExpander<TGame, TState, TAction, TPlayer> : MCTreeSearchExpanderBase<TGame, PVNetworkBasedMCTreeSearchNode<TState, TAction>, TState, TAction, TPlayer>
        where TGame : IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        IPVNetwork<TState, TAction> Network;

        public PVNetworkBasedMCTreeSearchExpander(TGame game, IPVNetwork<TState, TAction> network) : base(game)
        {
            Network = network;
        }

        //public PVNetworkBasedMCTreeSearchNode<TState, TAction> Expand(PVNetworkBasedMCTreeSearchNode<TState, TAction> node)
        //{
        //    foreach (Game.GetAllowedActions(node.State))
        //    {
        //
        //    }
        //}
        //
        //protected bool TryExpand(MCTreeSearchNode<TState, TAction> node)
        //{
        //    if (node.IsExpandedAndHasNoChildren)
        //    {
        //        return false;
        //    }
        //
        //    if (node.IsUnexpanded)
        //    {
        //        node.Children = new Dictionary<TAction, MCTreeSearchNode<TState, TAction>>();
        //        var actions = Game.GetAllowedActions(node.State);
        //        var states = actions.Select(action => Game.Play(node.State, action));
        //
        //        foreach (TAction action in actions)
        //        {
        //            TState state = Game.Play(node.State, action);
        //            node.Children.Add(action, CreateNode(node, state, action));
        //        }
        //    }
        //
        //    return node.IsExpandedAndHasNoChildren == false;
        //}
        //
        //protected override PVNetworkBasedMCTreeSearchNode<TState, TAction> CreateNode(PVNetworkBasedMCTreeSearchNode<TState, TAction> parentNode, TState gameState, TAction action)
        //{
        //    var result = Network.Predict(gameState, Game.GetAllowedActions(gameState));
        //    return new PVNetworkBasedMCTreeSearchNode<TState, TAction>(parentNode, gameState, action, result);
        //}

        protected override void CreateChildren(PVNetworkBasedMCTreeSearchNode<TState, TAction> node)
        {
            node.Children = new Dictionary<TAction, PVNetworkBasedMCTreeSearchNode<TState, TAction>>();
            var actions = Game.GetAllowedActions(node.State);
            var childrenStates = actions.Select(action => Game.Play(node.State, action));
            var childrenPredictions = Network.Predict(childrenStates);
            var states = childrenStates.GetEnumerator();
            var predictions = childrenPredictions.GetEnumerator();

            foreach (TAction action in actions)
            {
                states.MoveNext();
                predictions.MoveNext();
                var childNode = new PVNetworkBasedMCTreeSearchNode<TState, TAction>(node, states.Current, action, predictions.Current);
                node.Children.Add(action, childNode);
            }
        }

        protected override PVNetworkBasedMCTreeSearchNode<TState, TAction> CreateNode(PVNetworkBasedMCTreeSearchNode<TState, TAction> parentNode, TState gameState, TAction action)
        {
            return new PVNetworkBasedMCTreeSearchNode<TState, TAction>(parentNode, gameState, action, Network.Predict(gameState));
        }

        protected override PVNetworkBasedMCTreeSearchNode<TState, TAction> SelectExpanded(PVNetworkBasedMCTreeSearchNode<TState, TAction> node)
        {
            return node.Children.Values.MaxItem(child => child.SelectionRating);
        }
    }
}
