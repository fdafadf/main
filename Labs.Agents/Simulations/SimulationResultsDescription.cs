using System;

namespace Labs.Agents
{
    public class SimulationResultsDescription : INamed
    {
        public DateTime Date;
        public string Agent;
        public string Environment;
        public int Length;
        public string Name => $"{Agent}_{Environment}_{Date:yyyyMMdd-HHmmss}";

        public SimulationResultsDescription(string agent, string environment)
        {
            Date = DateTime.Now;
            Agent = agent;
            Environment = environment;
        }
    }
}
