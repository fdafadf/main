using System;

namespace AI.MonteCarlo
{
    public class MCTreeSearchNode<TState, TAction> : MCTreeNode<MCTreeSearchNode<TState, TAction>, TState, TAction>
    {
        public MCTreeSearchNode(MCTreeSearchNode<TState, TAction> parent, TState gameState, TAction lastAction) : base(parent, gameState, lastAction)
        {
        }
    }
}
