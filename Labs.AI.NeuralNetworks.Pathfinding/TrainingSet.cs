namespace Pathfinder
{
    public class TrainingSet
    {
        public string Name;
        public TrainingSettings Training;
        public TrainingDataSettings TrainingData;
        public TrainingNetworkSettings Network;

        public TrainingSet(string name, TrainingSettings training, TrainingDataSettings trainingData, TrainingNetworkSettings network)
        {
            Name = name;
            Training = training;
            TrainingData = trainingData;
            Network = network;
        }
    }
}
