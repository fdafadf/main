using System;
using System.Collections.Generic;
using System.Text;

namespace AI
{
    public interface IPolicy<TState, TAction>
    {
        IDictionary<TAction, double> GetProbabilities(TState state);
    }

    public interface IStatePolicy<TAction>
    {
        double GetProbability(TAction action);
    }

    public interface IValueFunction<TState>
    {
        double Evaluate(TState state);
    }
}
