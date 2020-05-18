namespace Labs.Agents.NeuralNetworks
{
    public class MarkovHistoryItem
    {
        public readonly double[] Input;
        public readonly double[] State;
        public readonly double Reward;

        public MarkovHistoryItem(double[] input, double[] state, double reward)
        {
            Input = input;
            State = state;
            Reward = reward;
        }
    }
}
