using Games;
using Games.Utilities;
using System;

namespace AI.NeuralNetworks.Games
{
    public class LabeledState<TState, TLabel> : ConvertedInput
    {
        public TState State { get; }
        public TLabel Label { get; }

        public LabeledState(TState state, double[] input, double[] output, TLabel label) : base(input, output)
        {
            State = state;
            Label = label;
        }
    }
}
