using Games.Utilities;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public class MCTreeNode<TNode, TState, TAction> : IGameTreeNode<TNode, TState, TAction>
        where TNode : MCTreeNode<TNode, TState, TAction>
    {
        public TState State { get; }
        public TAction LastAction { get; }
        public TNode Parent { get; }
        public Dictionary<TAction, TNode> Children { get; set; }
        public uint Visits;
        public double Value;
        public bool IsUnexpanded => Children == null;
        public bool IsExpandedAndHasNoChildren => Children?.Count == 0;

        public MCTreeNode(TState state)
        {
            State = state;
        }

        public MCTreeNode(TNode parent, TState state, TAction lastAction)
        {
            Parent = parent;
            State = state;
            LastAction = lastAction;
        }

        public TNode GetMostVisitedChild()
        {
            if (IsUnexpanded || IsExpandedAndHasNoChildren)
            {
                return default(TNode);
            }
            else
            {
                return Children.MaxItem(c => c.Value.Visits).Value;
            }
        }
    }
}
