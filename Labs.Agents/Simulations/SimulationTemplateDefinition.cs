using Labs.Agents.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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

    public class SimulationTemplateDefinition : IValidatable, INamed
    {
        [Category("General")]
        public string Name { get; set; }
        [Category("Simulation")]
        [DisplayName("Configuration")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public SimulationModelConfiguration Model { get; set; } = new SimulationModelConfiguration();
        [Category("Space")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Movement System")]
        public string MovementSystem { get; set; } = typeof(CardinalMovement).FullName;
        [Category("Space")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Goal System")]
        public string GoalSystem { get; set; } = "Random Renewable";
        [Category("Space")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Template")]
        public string Space { get; set; }
        [Category("Agent")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Definition")]
        public string SimulationPlugin { get; set; }
        [Category("Visualisation")]
        [DisplayName("Animation Interval")]
        public int AnimationInterval { get; set; } = 50;
        [Browsable(false)]
        [JsonIgnore]
        public Workspace Workspace { get; private set; }

        public SimulationTemplateDefinition(Workspace workspace)
        {
            Workspace = workspace;
        }

        public void Validate()
        {
            Assert.NotNullOrWhiteSpace(Name, $"Property 'Name' in category 'General' can not be empty.");
            Assert.NotNullOrWhiteSpace(MovementSystem, $"Property 'System' in category 'Movement' can not be empty.");
            Assert.NotNullOrWhiteSpace(GoalSystem, $"Property 'System' in category 'Goal' can not be empty.");
            Assert.NotNullOrWhiteSpace(Space, $"Property 'Name' in category 'Space' can not be empty.");
            Assert.NotNullOrWhiteSpace(SimulationPlugin, $"Property 'Name' in category 'Agent' can not be empty.");
        }

        public IEnumerable<string> GetMovementSystems()
        {
            return new string[] {
                typeof(CardinalMovement).FullName,
                typeof(RotableMovement).FullName,
            };
        }

        public IEnumerable<string> GetGoalSystems()
        {
            return new string[] {
                "Random Renewable",
                "Two Points",
            };
        }

        public IEnumerable<string> GetSpaces()
        {
            return Workspace.Spaces.Select(space => space.Name);
        }

        public IEnumerable<string> GetSimulationPlugins()
        {
            return Workspace.SimulationPlugins.Select(plugin => plugin.Name);
        }

        public ISpaceTemplateFactory GetSpaceTemplateFactory() => Workspace.Spaces.GetByName(Space);
        public ISimulationPluginFactory GetSimulationPluginFactory() => Workspace.GetSimulationPluginFactory(SimulationPlugin);

        public override string ToString()
        {
            return $"{Model.AgentsCollisionModel}";
        }
    }
}
