using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationResults : INamed
    {
        public DateTime Date;
        public string Agent;
        public string Environment;
        public int Length => Series.First().Value.Count;
        public Dictionary<string, List<double>> Series = new Dictionary<string, List<double>>();

        public SimulationResults(string agent, string environment)
        {
            Date = DateTime.Now;
            Agent = agent;
            Environment = environment;
        }

        string INamed.Name => $"{Agent}_{Environment}_{Date:yyyyMMdd-HHmmss}";
    }
}
