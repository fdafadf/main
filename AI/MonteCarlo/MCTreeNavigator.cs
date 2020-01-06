using Games;
using Games.Utilities;

namespace AI.MonteCarlo
{
    public class MCTreeNavigator<TGame, TGameState, TGameAction, TPlayer>
        : GameTreeNavigator<MCTreeSearchAsGameTree<TGame, TGameState, TGameAction, TPlayer>, TGameState, TGameAction, TPlayer, MCTreeNode<TGameAction, TGameState>>
        where TGame : IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : class, IGameAction
    {
        public MCTreeSearch<TGame, TGameState, TGameAction, TPlayer> Mcts { get; }

        public MCTreeNavigator(MCTreeSearch<TGame, TGameState, TGameAction, TPlayer> mcts) : base(new MCTreeSearchAsGameTree<TGame, TGameState, TGameAction, TPlayer>(mcts))
        {
            Mcts = mcts;
        }

        public override MCTreeNode<TGameAction, TGameState> CurrentNode
        {
            get
            {
                return Mcts.CurrentNode;
            }
        }

        protected override void DoBackward()
        {
            //base.DoBackward();
            Mcts.Undo();
        }

        protected override void DoForward(MCTreeNode<TGameAction, TGameState> nextNode)
        {
            // base.DoForward(nextNode);
            Mcts.Play(nextNode.LastAction);
        }
    }

    public class MCTreeSearchAsGameTree<TGame, TGameState, TGameAction, TPlayer>
        : IGameTree<TGameState, TGameAction, MCTreeNode<TGameAction, TGameState>>
        where TGame : IGame<TGameState, TGameAction, TPlayer>
        where TGameState : IGameState<TPlayer>
        where TPlayer : IPlayer
        where TGameAction : class, IGameAction
    {
        public MCTreeSearch<TGame, TGameState, TGameAction, TPlayer> Mcts;

        public MCTreeSearchAsGameTree(MCTreeSearch<TGame, TGameState, TGameAction, TPlayer> mcts)
        {
            Mcts = mcts;
        }

        public MCTreeNode<TGameAction, TGameState> Root => Mcts.Root;

        public MCTreeNode<TGameAction, TGameState> Expand(MCTreeNode<TGameAction, TGameState> node, TGameAction action)
        {
            Mcts.Expand(node);
            return node.Children[action];
        }

        //public void AddChildren(MCTreeNode<TGameAction, TGameState> parent, MCTreeNode<TGameAction, TGameState> child)
        //{
        //    Mcts.Expand(parent);
        //}
    }
}
