using System.ComponentModel;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentTrainerConfiguration
    {
        [Category("General")]
        public int ViewRadius { get; set; } = 3;
        [Category("General")]
        public int[] LayersSizes { get; set; } = new int[] { 84, 42, 21, 7, 1 };
        [Category("General")]
        public string Space { get; set; }
        [Category("General")]
        public int NetworkSeed { get; set; } = 0;
        [Category("Shortcuts")]
        [DisplayName("TrainingSimulationConfiguration.IterationLimit")]
        public int TrainingIterations
        {
            get
            {
                return TrainingSimulationConfiguration.IterationLimit;
            }
            set
            {
                TrainingSimulationConfiguration.IterationLimit = value;
            }
        }
        [Category("Shortcuts")]
        [DisplayName("TrainingSimulationConfiguration.Seed")]
        public int? TrainingSeed
        {
            get
            {
                return TrainingSimulationConfiguration.Seed;
            }
            set
            {
                TrainingSimulationConfiguration.Seed = value;
            }
        }
        [Category("Shortcuts")]
        [DisplayName("TrainingProgressTrackerConfiguration.IterationLimit")]
        public int TestingIterations
        {
            get
            {
                return TrainingProgressTrackerConfiguration.IterationLimit;
            }
            set
            {
                TrainingProgressTrackerConfiguration.IterationLimit = value;
            }
        }
        [Category("Shortcuts")]
        [DisplayName("TrainingProgressTrackerConfiguration.Interval")]
        public int TestingInterval
        {
            get
            {
                return TrainingProgressTrackerConfiguration.Interval;
            }
            set
            {
                TrainingProgressTrackerConfiguration.Interval = value;
            }
        }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AgentNetworkTrainingConfiguration TrainingPluginConfiguration { get; set; }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public SimulationModelConfiguration TrainingSimulationConfiguration { get; set; }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public TrainingProgressTrackerConfiguration TrainingProgressTrackerConfiguration { get; set; }
        [Category("Advanced")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public SimulationModelConfiguration TrainingProgressSimulationConfiguration { get; set; }

        public NeuralAgentTrainerConfiguration()
        {
            TrainingPluginConfiguration = new AgentNetworkTrainingConfiguration();
            TrainingSimulationConfiguration = new SimulationModelConfiguration();
            TrainingProgressTrackerConfiguration = new TrainingProgressTrackerConfiguration();
            TrainingProgressSimulationConfiguration = new SimulationModelConfiguration();
            TrainingSimulationConfiguration.AgentDestructionModel.RemoveDestoryed = true;
            TrainingSimulationConfiguration.IterationLimit = 50000;
            TrainingProgressSimulationConfiguration.AgentDestructionModel.RemoveDestoryed = true;
        }
    }
}
