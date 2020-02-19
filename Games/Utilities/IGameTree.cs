namespace Games.Utilities
{
    public interface IGameTree<TNode, TState, TAction>
        where TAction : IGameAction
        where TNode : IGameTreeNode<TNode, TState, TAction>
    {
        TNode Root { get; }
    }
}
