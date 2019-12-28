using System.Collections.Generic;

namespace Games.Utilities
{
    public interface IGameTreeEvaluator<TGameState>
    {
        double[] EvaluateLeaf(TGameState finalState);
        double[] EvaluateNode(TGameState nodeState, IEnumerable<IGameStateOutput<TGameState>> children);
    }
}
