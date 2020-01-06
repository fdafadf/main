using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.MonteCarlo
{
    public class MCTreeSearchRound<TKey, TGameState>
        where TKey : class
    {
        public IEnumerable<MCTreeNode<TKey, TGameState>> Path { get; set; }
        public IEnumerable<MCTreeNode<TKey, TGameState>> Selection { get; set; }
        public MCTreeNode<TKey, TGameState> Expansion { get; set; }
        public List<Tuple<TKey, TGameState>> Playout { get; set; }

        public TGameState GetLastGameState()
        {
            if (Playout != null && Playout.Count > 0)
            {
                return Playout.Last().Item2;
            }

            if (Expansion != null)
            {
                return Expansion.GameState;
            }

            return Selection.Last().GameState;
        }
    }
}
