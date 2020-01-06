using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MonteCarlo
{
    public class MCTreeNode<TGameAction, TGameNode>
        : IGameTreeNode<TGameNode, TGameAction, MCTreeNode<TGameAction, TGameNode>>
    {
        public TGameNode GameState { get; }
        public TGameAction LastAction { get; }
        public Dictionary<TGameAction, MCTreeNode<TGameAction, TGameNode>> Children { get; set; }
        public uint Simulations;
        public uint Wins;
        public MCTreeNode<TGameAction, TGameNode> Parent { get; }

        public MCTreeNode(MCTreeNode<TGameAction, TGameNode> parent, TGameNode data, TGameAction lastAction)
        {
            Parent = parent;
            GameState = data;
            LastAction = lastAction;
        }

        public bool IsUnexpanded => Children == null;

        public bool IsExpandedAndHasNoChildren => Children?.Count == 0;

        public int Depth => Parent == null ? 0 : Parent.Depth + 1;

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
            string[] lines = GameState.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            lines[0] += $"  S: {Simulations}";
            lines[1] += $"  W: {Wins}";
            return string.Join(Environment.NewLine, lines);
        }
    }
}
