namespace Labs.Agents.NeuralNetworks
{
    public class TrainingProgressTrackerConfiguration
    {
        public bool Enabled { get; set; } = true;
        public int Interval { get; set; } = 100;
        public int IterationLimit { get; set; } = 1000;

        public override string ToString()
        {
            return Enabled ? $"{IterationLimit}/{Interval}" : "Disabled";
        }
    }
}
