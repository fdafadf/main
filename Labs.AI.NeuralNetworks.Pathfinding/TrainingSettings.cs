namespace Pathfinder
{
    public class TrainingSettings
    {
        public double LearningRate { get; set; }
        public double Momentum { get; set; }
        public int Epoches { get; set; }
        public int? Seed { get; set; }
    }
}
