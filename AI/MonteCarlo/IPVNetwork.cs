using Games;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public interface IPVNetwork<TState, TAction>
        where TState : IPeriodState
    {
        //double Value(TGameState gameState);
        //double[] Probability(TGameState gameState, TGameAction action);
        PVNetworkOutput<TAction> Predict(TState state);
        IEnumerable<PVNetworkOutput<TAction>> Predict(IEnumerable<TState> states);
    }
}
