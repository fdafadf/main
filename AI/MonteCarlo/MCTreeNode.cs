using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MonteCarlo
{
    public class MCTreeNode<TGameAction, TGameNode>
    {
        public TGameNode Data;
        public Dictionary<TGameAction, MCTreeNode<TGameAction, TGameNode>> Children;
        public uint Simulations;
        public uint Wins;
        public MCTreeNode<TGameAction, TGameNode> Parent;

        public MCTreeNode(MCTreeNode<TGameAction, TGameNode> parent, TGameNode data)
        {
            Parent = parent;
            Data = data;
        }

        public bool IsUnexpanded => Children == null;

        public bool IsExpandedAndHasNoChildren => Children?.Count == 0;

        public MCTreeMove<TGameAction, TGameNode> GetBestAction()
        {
            if (IsUnexpanded || IsExpandedAndHasNoChildren)
            {
                return null;
            }
            else
            {
                var entry = Children.MaxItem(c => c.Value.Simulations);
                return new MCTreeMove<TGameAction, TGameNode>(entry.Key, entry.Value);
            }
        }

        public override string ToString()
        {
            string[] lines = Data.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            lines[0] += $"  S: {Simulations}";
            lines[1] += $"  W: {Wins}";
            return string.Join(Environment.NewLine, lines);
        }
    }
}
