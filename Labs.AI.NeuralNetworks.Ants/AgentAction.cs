namespace Labs.AI.NeuralNetworks.Ants
{
    public class AgentAction
    {
        public static readonly double[] Rotations = { 0, 20, -20 };
        public static readonly int EncodedSize = Rotations.Length + 2;

        public int Rotation;
        public bool Forward;
        public double[] Encoded;

        public AgentAction(int rotation, bool forward)
        {
            Rotation = rotation;
            Forward = forward;
            Encoded = new double[EncodedSize];
            Encoded[Rotation] = 1;
            Encoded[Rotations.Length + (Forward ? 0 : 1)] = 1;
        }
    }
}
