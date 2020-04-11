using System;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AntNetworkV1 : AntNetwork<AntNetworkV1Input>
    {
        public AntNetworkV1() : base(AntNetworkV1Input.EncodedSize)
        {
        }

        public override Prediction<AntNetworkV1Input> Predict(AgentState state)
        {
            return Predict(new AntNetworkV1Input(state));
        }

        public override AntNetworkV1Input CreateInput(AgentState state, AgentAction action)
        {
            return new AntNetworkV1Input(state, action);
        }
    }
}
