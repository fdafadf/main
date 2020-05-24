using Labs.Agents.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationTemplateDefinition : IValidatable, INamed
    {
        [Category("General")]
        public string Name { get; set; }
        [Category("General")]
        public int Iterations { get; set; } = 1000;
        [Category("Movement")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("System")]
        public string MovementSystem { get; set; } = typeof(CardinalMovement).FullName;
        [Category("Goal")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("System")]
        public string GoalSystem { get; set; } = "Random Renewable";
        [Category("Space")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Name")]
        public string Space { get; set; }
        [Category("Agent")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Name")]
        public string SimulationPlugin { get; set; }
        [Category("Agent")]
        [DisplayName("Collision Model")]
        public AgentsCollisionModel AgentsCollisionModel { get; set; }
        [Category("Agent")]
        [DisplayName("Destruction Behaviour")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public AgentDestructionModel AgentDestructionModel { get; set; } = new AgentDestructionModel();
        [Category("Visualisation")]
        [DisplayName("Animation Interval")]
        public int AnimationInterval { get; set; } = 100;
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
            return Workspace.SimulationPlugins.Select(driver => driver.Name);
        }

        public ISpaceTemplateFactory GetSpaceTemplateFactory() => Workspace.Spaces.GetByName(Space);
        public ISimulationPluginFactory GetSimulationPluginFactory() => Workspace.GetSimulationPluginFactory(SimulationPlugin);

        public override string ToString()
        {
            return $"{AgentsCollisionModel}";
        }
    }
}
