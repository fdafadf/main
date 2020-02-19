namespace AI.MonteCarlo
{
    public interface IMCTreeSearchExpander<TNode>
    {
        TNode Expand(TNode node);
    }
}
