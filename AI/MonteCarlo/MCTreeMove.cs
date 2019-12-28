namespace AI.MonteCarlo
{
    public class MCTreeMove<TGameAction, TGameNode>
    {
        public TGameAction Action { get; private set; }
        public MCTreeNode<TGameAction, TGameNode> Node { get; private set; }

        public MCTreeMove(TGameAction action, MCTreeNode<TGameAction, TGameNode> node)
        {
            this.Action = action;
            this.Node = node;
        }
    }
}
