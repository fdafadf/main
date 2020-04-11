namespace Labs.AI.NeuralNetworks.Ants
{
    public abstract class AntNetworkInput
    {
        public double[] Encoded;

        public AntNetworkInput(int encodedSize)
        {
            Encoded = new double[encodedSize];
        }

        public abstract AntNetworkInput EncodeAction(AgentAction action);
    }
}
