using System;
using System.ComponentModel;

namespace Labs.Agents
{
    public class SimulationModelConfiguration
    {
        [Category("General")]
        public int IterationLimit { get; set; } = 1000;
        [Category("Agent")]
        [DisplayName("Collision Model")]
        public AgentsCollisionModel AgentsCollisionModel { get; set; }
        [Category("Agent")]
        [DisplayName("Destruction Behaviour")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AgentDestructionModel AgentDestructionModel { get; set; } = new AgentDestructionModel();
        [Category("Determinism")]
        [DisplayName("Seed")]
        public int? Seed { get; set; } = 0;

        public Random CreateRandom()
        {
            return new Random(Seed ?? Guid.NewGuid().GetHashCode());
        }

        public SimulationModelConfiguration Clone()
        {
            return new SimulationModelConfiguration()
            {
                IterationLimit = IterationLimit,
                AgentsCollisionModel = AgentsCollisionModel,
                AgentDestructionModel = AgentDestructionModel,
                Seed = Seed,
            };
        }
    }
}
