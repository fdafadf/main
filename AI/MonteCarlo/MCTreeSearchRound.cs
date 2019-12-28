using System;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public class MCTreeSearchRound<TKey, TData>
    {
        public IEnumerable<MCTreeNode<TKey, TData>> Path { get; set; }
        public IEnumerable<MCTreeNode<TKey, TData>> Selection { get; set; }
        public MCTreeNode<TKey, TData> Expansion { get; set; }
        public List<Tuple<TKey, TData>> Playout { get; set; }
    }
}
