using Games.Utilities;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public static class Extensions
    {
        public static IEnumerable<TNode> GetPath<TNode, TState, TAction>(this TNode self) where TNode : IGameTreeNode<TNode, TState, TAction>
        {
            return self.GetPath<TNode, TState, TAction>(default(TNode));
        }

        public static IEnumerable<TNode> GetPath<TNode, TState, TAction>(this TNode self, TNode node) where TNode : IGameTreeNode<TNode, TState, TAction>
        {
            List<TNode> result = new List<TNode>();
            TNode current = self;

            while (current != null && current.Equals(node) == false)
            {
                result.Add(current);
                current = current.Parent;
            }

            result.Reverse();
            return result;
        }
    }
}
