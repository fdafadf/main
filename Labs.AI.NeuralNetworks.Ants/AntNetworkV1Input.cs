using System;
using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AntNetworkV1Input : AntNetworkInput
    {
        public static readonly int EncodedSize = AgentState.EncodedSize + AgentAction.EncodedSize;

        public AgentState State;

        public AntNetworkV1Input(AgentState state) : base(EncodedSize)
        {
            State = state;
            EncodeState(State);
        }

        public AntNetworkV1Input(AgentState state, AgentAction action) : this(state)
        {
            EncodeAction(action);
        }

        public AntNetworkV1Input EncodeState(AgentState state)
        {
            Array.Copy(state.Encoded, Encoded, state.Encoded.Length);
            return this;
        }

        public override AntNetworkInput EncodeAction(AgentAction action)
        {
            Array.Copy(action.Encoded, 0, Encoded, AgentState.EncodedSize, AgentAction.EncodedSize);
            return this;
        }
    }
}
