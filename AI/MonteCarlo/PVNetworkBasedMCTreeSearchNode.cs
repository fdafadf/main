using Games;
using System;

namespace AI.MonteCarlo
{
    public class PVNetworkBasedMCTreeSearchNode<TState, TAction> : MCTreeNode<PVNetworkBasedMCTreeSearchNode<TState, TAction>, TState, TAction>
        where TState : IPeriodState
    {
        public static PVNetworkBasedMCTreeSearchNode<TState, TAction> CreateRoot(TState state, PVNetworkOutput<TAction> networkOutput)
        {
            return new PVNetworkBasedMCTreeSearchNode<TState, TAction>(state, networkOutput);
        }

        public PVNetworkOutput<TAction> NetworkOutput;
        public double NetworkProbability;

        protected PVNetworkBasedMCTreeSearchNode(TState state, PVNetworkOutput<TAction> networkOutput) : base(null, state, default(TAction))
        {
            NetworkOutput = networkOutput;
            NetworkProbability = 1;
        }

        public PVNetworkBasedMCTreeSearchNode(PVNetworkBasedMCTreeSearchNode<TState, TAction> parentNode, TState state, TAction lastAction, PVNetworkOutput<TAction> networkOutput) : base(parentNode, state, lastAction)
        {
            NetworkOutput = networkOutput;
            NetworkProbability = Parent.NetworkOutput.GetProbability(LastAction);
        }

        public double SelectionRating
        {
            // Node with highest Q + U
            // Q (reward) = Wins / Visited
            // U = Probability + Sqrt(Parent.Visited) / (1 + Visited)

            get
            {
                if (Visits == 0)
                {
                    return double.PositiveInfinity;
                }
                else
                {
                    double q = Value / Visits;
                    double u = NetworkProbability * (Math.Sqrt(Parent.Visits) / (1 + Visits));
                    return q + u;
                }
            }
        }
    }
}
