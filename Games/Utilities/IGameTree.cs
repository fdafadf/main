namespace Games.Utilities
{
    public interface IGameTree<TGameState, TGameAction, TNode>
        where TGameAction : IGameAction
        where TNode : IGameTreeNode<TGameState, TGameAction, TNode>
    {
        TNode Root { get; }
    }
}
