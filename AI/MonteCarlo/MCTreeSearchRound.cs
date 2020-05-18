using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.MonteCarlo
{
    public class MCTreeSearchRound<TNode, TState, TAction>
        where TNode : MCTreeNode<TNode, TState, TAction>
    {
        public IEnumerable<TNode> Path { get; set; }
        public IEnumerable<TNode> Selection { get; set; }
        public TNode Expansion { get; set; }
        public IEnumerable<Tuple<TAction, TState>> Playout { get; set; }
        public double PlayoutValue { get; set; }

        public TState GetLastGameState()
        {
            if (Playout != null && Playout.Any())
            {
                return Playout.Last().Item2;
            }

            if (Expansion != null)
            {
                return Expansion.State;
            }

            return Selection.Last().State;
        }
    }
}
