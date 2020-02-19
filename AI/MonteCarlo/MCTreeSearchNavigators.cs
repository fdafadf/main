using Games;
using System.Collections.Generic;
using System.Linq;

namespace AI.MonteCarlo
{
    public class MCTreeSearchNavigators<TMcts, TGame, TNode, TState, TAction, TPlayer>
        where TGame : IGame<TState, TAction, TPlayer>
        where TNode : MCTreeNode<TNode, TState, TAction>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
        where TMcts : MCTreeSearchBase<TNode, TState, TAction, TPlayer>
    {
        IEnumerable<MCTreeSearchNavigator<TMcts, TGame, TNode, TState, TAction, TPlayer>> navigators;

        public void Round()
        {

        }

        public void RoundCollection(TNode[] nodes)
        {
            var selectedNodes = navigators.Select(n => n.Mcts.Select(n.CurrentNode));
            //var selectedNodes = nodes.Select(Select);
            var expandResults = ExpandCollection(navigators, selectedNodes).GetEnumerator();
            var navigs = navigators.GetEnumerator();

            foreach (TNode selectedNode in selectedNodes)
            {
                double value;
                expandResults.MoveNext();
                navigs.MoveNext();
                var mcts = navigs.Current.Mcts;

                if (selectedNode.State.IsFinal)
                {
                    value = 0; // mcts.FinalValue(selectedNode);
                }
                else
                {
                    if (expandResults.Current != null)
                    {
                        value = 0; // mcts.Playout(expandResults.Current);
                    }
                    else
                    {
                        value = 0; // mcts.Playout(selectedNode);
                    }
                }

                //mcts.Propagate(value, selectedNode);
            }
        }

        public virtual IEnumerable<TNode> ExpandCollection(IEnumerable<MCTreeSearchNavigator<TMcts, TGame, TNode, TState, TAction, TPlayer>> navigators, IEnumerable<TNode> selectedNodes)
        {
            var n = navigators.GetEnumerator();

            foreach (TNode selectedNode in selectedNodes)
            {
                n.MoveNext();

                if (selectedNode.State.IsFinal == false)
                {
                    yield return n.Current.Mcts.Expander.Expand(selectedNode);
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
