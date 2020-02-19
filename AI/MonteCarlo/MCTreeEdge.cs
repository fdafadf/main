namespace AI.MonteCarlo
{
    public class MCTreeEdge<TNode, TState, TAction>
        where TNode : MCTreeNode<TNode, TState, TAction>
    {
        public TAction Action { get; private set; }
        public MCTreeNode<TNode, TState, TAction> Node { get; private set; }

        public MCTreeEdge(TAction action, MCTreeNode<TNode, TState, TAction> node)
        {
            this.Action = action;
            this.Node = node;
        }
    }
}
