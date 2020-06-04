using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationResults : INamed
    {
        public SimulationResultsDescription Description;
        public Dictionary<string, List<double>> Series = new Dictionary<string, List<double>>();
        public string Name => Description.Name;

        public SimulationResults(string agent, string environment)
        {
            Description = new SimulationResultsDescription(agent, environment);
        }

        public SimulationResults(SimulationResultsDescription description, Dictionary<string, List<double>> series)
        {
            Description = description;
            Series = series;
        }

        public override string ToString()
        {
            double? reachedGoals = Series.ContainsKey("Reached Goals") ? (double?)Series["Reached Goals"].Last() : null;
            double? totalReward = Series.ContainsKey("Total Reward") ? (double?)Series["Total Reward"].Last() : null;
            double? collisions = Series.ContainsKey("Collisions") ? (double?)Series["Collisions"].Last() : null;
            return $"ReachedGoals: {reachedGoals,3} TotalReward: {totalReward,14:f5} Collisions: {collisions,4}";
        }
    }
}
