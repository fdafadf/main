using Games;
using Games.Utilities;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public class MCTreeSearchNavigator<TMcts, TGame, TNode, TState, TAction, TPlayer>
        : IGameTreeNavigator<TNode, TState, TAction>,
          IGameTree<TNode, TState, TAction>
        where TGame : IGame<TState, TAction, TPlayer>
        where TNode : MCTreeNode<TNode, TState, TAction>
        where TState : IGameState<TPlayer>
        where TAction : IGameAction
        where TPlayer : IPlayer
        where TMcts : MCTreeSearchBase<TNode, TState, TAction, TPlayer>
    {
        public TMcts Mcts;
        public TGame Game;
        public TNode Root { get; }
        public TNode CurrentNode { get; private set; }
        public IGameTree<TNode, TState, TAction> GameTree => this;

        public MCTreeSearchNavigator(TMcts mcts, TGame game, TNode rootNode)
        {
            Mcts = mcts;
            Game = game;
            Root = rootNode;
            CurrentNode = rootNode;
        }

        public void Round(int repeats)
        {
            for (int i = 0; i < repeats; i++)
            {
                Mcts.Round(CurrentNode);
            }
        }

        public bool Play(TAction action)
        {
            Mcts.Expander.Expand(CurrentNode);
            TNode nextNode = CurrentNode.Children[action];
        
            if (nextNode == null)
            {
                return false;
            }
            else
            {
                CurrentNode = nextNode;
                return true;
            }
        }

        public bool Forward(TNode childNode)
        {
            CurrentNode = childNode;
            return true;
        }

        public bool Backward()
        {
            if (CurrentNode.Parent == null)
            {
                return false;
            }
            else
            {
                CurrentNode = CurrentNode.Parent;
                return true;
            }
        }

        public IEnumerable<MCTreeSearchRound<TNode, TState, TAction>> RoundWithDetails(int repeats)
        {
            List<MCTreeSearchRound<TNode, TState, TAction>> result = new List<MCTreeSearchRound<TNode, TState, TAction>>();

            for (int i = 0; i < repeats; i++)
            {
                result.Add(Mcts.RoundWithDetails(CurrentNode));
            }

            return result;
        }
    }
}
