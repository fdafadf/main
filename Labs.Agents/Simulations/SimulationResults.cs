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

        public string[] ToHeaderStrings()
        {
            return new[]
            {
                string.Format("{0,-12}  {1,-14}  {2,-10}", "ReachedGoals", "TotalReward", "Collisions"),
                string.Format("{0,-12}  {1,-14}  {2,-10}", new string('=', 12), new string('=', 14), new string('=', 10)),
            };
        }

        public string ToDataString()
        {
            double? reachedGoals = Series.ContainsKey("Reached Goals") ? (double?)Series["Reached Goals"].Last() : null;
            double? totalReward = Series.ContainsKey("Total Reward") ? (double?)Series["Total Reward"].Last() : null;
            double? collisions = Series.ContainsKey("Collisions") ? (double?)Series["Collisions"].Last() : null;
            return string.Format("{0,12}  {1,14:f5}  {2,10}", reachedGoals, totalReward, collisions);
        }
    }
}
