namespace AI.MonteCarlo
{
    public abstract class PVNetworkOutput<TAction>
    {
        public abstract double Value { get; }
        public double[] Raw { get; }

        public PVNetworkOutput(double[] raw)
        {
            Raw = raw;
        }

        public abstract double GetProbability(TAction action);

    }
    //public class PVNetworkActionProbabilities<TAction>
    //{
    //    double[] Values;
    //    Func<TAction, int> map;
    //
    //    public PVNetworkActionProbabilities(Func<TAction, int> map)
    //    {
    //
    //    }
    //
    //    public this[TAction action]
    //    {
    //        get
    //        {
    //            Values[map(action)]
    //        }
    //    }
    //}

    //public class PVNetworkStateEvaluation<TState, TAction>
    //    where TState : IPeriodState
    //{
    //    public TState State;
    //    public PVNetworkOutput<TAction> NetworkOutput;
    //    //public double Value => NetworkOutput.[RawNetworkOutput.Length - 1];
    //    //public double Value;
    //    //public IDictionary<TAction, double> Probabilities;
    //
    //    //public PVNetworkStateEvaluation(TState state, double value, IDictionary<TAction, double> probabilities)
    //    //{
    //    //    State = state;
    //    //    Value = value;
    //    //    Probabilities = probabilities;
    //    //}
    //
    //    public PVNetworkStateEvaluation(TState state, PVNetworkOutput<TAction> networkOutput)
    //    {
    //        State = state;
    //        NetworkOutput = networkOutput;
    //    }
    //
    //    //public abstract double GetProbability(TAction action);
    //
    //    //public PVNetworkStateEvaluation(PVNetworkBasedMCTreeSearchNode<TState, TAction> node)
    //    //{
    //    //    State = node.GameState;
    //    //    Value = node.Value;
    //    //    Probabilities = new Dictionary<TAction, double>();
    //    //
    //    //    foreach (var entry in node.NetworkProbabilities)
    //    //    {
    //    //        TAction action = entry.Key;
    //    //        double pn = node.NetworkProbabilities[action];
    //    //        double pm = node.Children[action].Visited / node.Visited;
    //    //        Probabilities.Add(action, (pn + pm) / 2.0);
    //    //    }
    //    //}
    //}
}
