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
    }
}
