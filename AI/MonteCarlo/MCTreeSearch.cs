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
        public static readonly double UCT_C = Math.Sqrt(2);

        IGamePlayoutGenerator<TState, TPlayer, TAction> PlayoutGenerator;

        public MCTreeSearch(IMCTreeSearchExpander<MCTreeSearchNode<TState, TAction>> expander, IGamePlayoutGenerator<TState, TPlayer, TAction> playoutGenerator) : base(expander)
        {
            PlayoutGenerator = playoutGenerator;
        }

        protected override double Playout(MCTreeSearchNode<TState, TAction> leafNode)
        {
            TState playoutFinalState = PlayoutGenerator.Generate(leafNode.State);
            TPlayer winner = playoutFinalState.GetWinner();
            return winner == null ? 0.5 : playoutFinalState.CurrentPlayer.Equals(winner) ? 1 : 0;
        }

        protected override double Playout(MCTreeSearchNode<TState, TAction> leafNode, out IEnumerable<Tuple<TAction, TState>> path)
        {
            path = PlayoutGenerator.GeneratePath(leafNode.State);
            TState playoutFinalState = path.Last().Item2;
            TPlayer winner = playoutFinalState.GetWinner();
            return winner == null ? 0.5 : playoutFinalState.CurrentPlayer.Equals(winner) ? 1 : 0;
        }

        protected override MCTreeSearchNode<TState, TAction> SelectChildren(MCTreeSearchNode<TState, TAction> node)
        {
            double logT = Math.Log(node.Visits);
            return node.Children.Values.MaxItem(child => CalculateUTC(child, logT));
        }

        //protected override MCTreeSearchNode<TState, TAction> SelectExpanded(MCTreeSearchNode<TState, TAction> node)
        //{
        //    return Random.Next(node.Children).Value;
        //}

        private static double CalculateUTC(MCTreeSearchNode<TState, TAction> node, double logT)
        {
            if (node.Visits == 0)
            {
                return double.PositiveInfinity;
            }
            else
            {
                double s1 = node.Value / node.Visits;
                double s2 = UCT_C * Math.Sqrt(logT / node.Visits);
                return s1 + s2;
            }
        }
    }
}
