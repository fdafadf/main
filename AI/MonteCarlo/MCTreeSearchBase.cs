using AI.MonteCarlo;
using Games;
using Games.Utilities;
using System;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public abstract class MCTreeSearchBase<TNode, TState, TAction, TPlayer> 
        where TNode : MCTreeNode<TNode, TState, TAction>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
    {
        public IMCTreeSearchExpander<TNode> Expander;

        public MCTreeSearchBase(IMCTreeSearchExpander<TNode> expander)
        {
            Expander = expander;
        }

        public void Round(TNode node)
        {
            double value;
            TNode selectedNode = Select(node);

            if (selectedNode.State.IsFinal)
            {
                value = FinalValue(selectedNode);
            }
            else
            {
                selectedNode = Expander.Expand(selectedNode);
                //if (Expand(selectedNode))
                //{
                //    selectedNode = SelectExpanded(selectedNode);
                //}

                value = Playout(selectedNode);
            }

            Propagate(node, selectedNode, value);
        }

        //public MCTreeSearchRound<TNode, TState, TAction> RoundWithDetails(TNode tree)
        //{
        //    return RoundWithDetails(tree.CurrentNode);
        //}

        public MCTreeSearchRound<TNode, TState, TAction> RoundWithDetails(TNode node)
        {
            MCTreeSearchRound<TNode, TState, TAction> result = new MCTreeSearchRound<TNode, TState, TAction>();
            TNode selectedNode = Select(node);
            result.Path = node.GetPath<TNode, TState, TAction>();
            result.Selection = selectedNode.GetPath<TNode, TState, TAction>(node);

            if (selectedNode.State.IsFinal)
            {
                result.PlayoutValue = FinalValue(selectedNode);
            }
            else
            {
                TNode expandedNode = Expander.Expand(selectedNode);

                if (expandedNode != selectedNode)
                {
                    result.Expansion = expandedNode;
                }

                selectedNode = expandedNode;
                result.PlayoutValue = Playout(selectedNode, out IEnumerable<Tuple<TAction, TState>> playoutPath);
                result.Playout = playoutPath;
            }

            Propagate(node, selectedNode, result.PlayoutValue);
            return result;
        }

        public TNode Select(TNode node)
        {
            return node.IsUnexpanded || node.IsExpandedAndHasNoChildren ? node : Select(SelectChildren(node));
        }

        protected virtual double FinalValue(TNode finalNode)
        {
            TPlayer player = finalNode.State.GetWinner();

            if (player == null)
            {
                return 0.5;
            }
            else if (player.Equals(finalNode.State.CurrentPlayer))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        
        protected abstract TNode SelectChildren(TNode node);

        protected abstract double Playout(TNode leafNode);

        protected abstract double Playout(TNode leafNode, out IEnumerable<Tuple<TAction, TState>> path);

        protected virtual void Propagate(TNode currentNode, TNode leafNode, double value)
        {
            var node = leafNode;

            do
            {
                node.Visits++;
                node.Value += value;
                node = node.Parent;
                value = 1 - value;
            }
            while (node != currentNode);

            node.Visits++;
            node.Value += value;
        }
    }
}
