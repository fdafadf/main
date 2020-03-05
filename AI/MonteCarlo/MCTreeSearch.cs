using Games;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.MonteCarlo
{
    public class MCTreeSearch<TGame, TState, TAction, TPlayer> : MCTreeSearchBase<MCTreeSearchNode<TState, TAction>, TState, TAction, TPlayer>
        where TGame : IGame<TState, TAction, TPlayer>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        IGamePlayoutGenerator<TState, TPlayer, TAction> PlayoutGenerator;

        public MCTreeSearch(IMCTreeSearchExpander<MCTreeSearchNode<TState, TAction>> expander, IGamePlayoutGenerator<TState, TPlayer, TAction> playoutGenerator) : base(expander)
        {
            PlayoutGenerator = playoutGenerator;
        }

        protected override double Playout(MCTreeSearchNode<TState, TAction> leafNode)
        {
            TState playoutFinalState = PlayoutGenerator.Generate(leafNode.State);
            TPlayer winner = playoutFinalState.GetWinner();
            return winner == null ? 0.5 : leafNode.State.CurrentPlayer.Equals(winner) ? 0 : 1;
        }

        protected override double Playout(MCTreeSearchNode<TState, TAction> leafNode, out IEnumerable<Tuple<TAction, TState>> path)
        {
            path = PlayoutGenerator.GeneratePath(leafNode.State);
            TState playoutFinalState = path.Any() ? path.Last().Item2 : leafNode.State;
            TPlayer winner = playoutFinalState.GetWinner();
            return winner == null ? 0.5 : leafNode.State.CurrentPlayer.Equals(winner) ? 0 : 1;
        }

        //protected override MCTreeSearchNode<TState, TAction> SelectChildren(MCTreeSearchNode<TState, TAction> node)
        //{
        //    double logT = Math.Log(node.Children.Sum(child => child.Value.Visits));
        //    return node.Children.Values.MaxItem(child => CalculateUTC(child, logT));
        //}

        //protected override MCTreeSearchNode<TState, TAction> SelectExpanded(MCTreeSearchNode<TState, TAction> node)
        //{
        //    return Random.Next(node.Children).Value;
        //}
         
    }
}
