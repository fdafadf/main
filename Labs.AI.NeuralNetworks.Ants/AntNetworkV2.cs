namespace Labs.AI.NeuralNetworks.Ants
{
    public class AntNetworkV2 : AntNetwork<AntNetworkV2Input>
    {
        public AntNetworkV2() : base(AntNetworkV2Input.EncodedSize)
        {
        }

        public AntNetworkV2(params int[] hiddenLayerSizes) : base(AntNetworkV2Input.EncodedSize, hiddenLayerSizes)
        {
        }

        public override Prediction<AntNetworkV2Input> Predict(AgentState state)
        {
            return Predict(new AntNetworkV2Input(state, null, History));
        }

        public override AntNetworkV2Input CreateInput(AgentState state, AgentAction action)
        {
            return new AntNetworkV2Input(state, action, History);
        }
    }
}
