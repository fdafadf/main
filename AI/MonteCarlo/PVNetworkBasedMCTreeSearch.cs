using Games;
using Games.Utilities;
using System;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public class PVNetworkBasedMCTreeSearch<TGame, TState, TAction, TPlayer> : MCTreeSearchBase<PVNetworkBasedMCTreeSearchNode<TState, TAction>, TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        public PVNetworkBasedMCTreeSearch(IMCTreeSearchExpander<PVNetworkBasedMCTreeSearchNode<TState, TAction>> expander) : base(expander)
        {
        }

        protected override double Playout(PVNetworkBasedMCTreeSearchNode<TState, TAction> leafNode)
        {
            return leafNode.NetworkOutput.Value;
        }

        protected override double Playout(PVNetworkBasedMCTreeSearchNode<TState, TAction> leafNode, out IEnumerable<Tuple<TAction, TState>> path)
        {
            path = null;
            return leafNode.NetworkOutput.Value;
        }

        protected override PVNetworkBasedMCTreeSearchNode<TState, TAction> SelectChildren(PVNetworkBasedMCTreeSearchNode<TState, TAction> node)
        {
            return node.Children.Values.MaxItem(child => child.SelectionRating);
        }
    }
}
