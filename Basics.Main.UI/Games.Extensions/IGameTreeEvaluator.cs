using Basics.Games.TicTacToe;
using System.Collections.Generic;

namespace Basics.Games
{
    public interface IGameTreeEvaluator<TGameState>
    {
        double[] EvaluateLeaf(GameState finalState);
        double[] EvaluateNode(TGameState nodeState, IEnumerable<IGameStateOutput<TGameState>> children);
    }
}
