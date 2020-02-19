namespace AI.MonteCarlo
{
    public class PVNetworkMockOutput<TAction> : PVNetworkOutput<TAction>
    {
        public override double Value => 0.5;

        public PVNetworkMockOutput() : base(null)
        {
        }

        public override double GetProbability(TAction action)
        {
            return 0.5;
        }
    }
}
