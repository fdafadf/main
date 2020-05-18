using Labs.Agents.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationFactory : IValidatable
    {
        [Category("General")]
        public string Name { get; set; }
        [Category("General")]
        public int Iterations { get; set; } = 1000;
        [Category("Movement")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("System")]
        public string MovementSystem { get; set; }
        [Category("Goal")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("System")]
        public string GoalSystem { get; set; }
        [Category("Space")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Name")]
        public string Space { get; set; }
        [Category("Agent")]
        [TypeConverter(typeof(DropDownStringConverter))]
        [DisplayName("Name")]
        public string SimulationPlugin { get; set; }
        [Category("Agent")]
        public AgentsCollisionModel AgentsCollisionModel { get; set; }
        Workspace Workspace;

        public SimulationFactory(Workspace workspace)
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

        public ISimulation CreateSimulation()
        {
            var spaceDefinition = Workspace.Spaces.GetByName(Space);
            var simulationPluginFactory = Workspace.GetSimulationPluginFactory(SimulationPlugin);
            var simulationPlugin = simulationPluginFactory.CreatePlugin();
            var method = GetType().GetMethods().Where(m => m.Name == "CreateSimulation").Skip(1).First();
            method = method.MakeGenericMethod(simulationPlugin.GetType(), simulationPlugin.AgentType);
            return method.Invoke(this, new object[] { spaceDefinition, simulationPlugin, simulationPluginFactory.Name }) as ISimulation;
        }

        public ISimulation CreateSimulation<TPlugin, TAgent>(ISpaceTemplateFactory spaceDefinition, TPlugin simulationPlugin, string pluginName)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> 
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            return new SimulationModel1<TPlugin, TAgent>(spaceDefinition, simulationPlugin, pluginName, AgentsCollisionModel);
        }

        public ISimulationViualisation CreateSimulationForm()
        {
            var spaceDefinition = Workspace.Spaces.GetByName(Space);
            var simulationPluginFactory = Workspace.GetSimulationPluginFactory(SimulationPlugin);
            var simulationPlugin = simulationPluginFactory.CreatePlugin();
            var method = GetType().GetMethods().Where(m => m.Name == "CreateSimulationForm").Skip(1).First();
            method = method.MakeGenericMethod(simulationPlugin.GetType(), simulationPlugin.AgentType);
            return method.Invoke(this, new object[] { spaceDefinition, simulationPlugin, simulationPluginFactory.Name }) as ISimulationViualisation;
        }

        public ISimulationViualisation CreateSimulationForm<TPlugin, TAgent>(ISpaceTemplateFactory spaceDefinition, TPlugin simulationPlugin, string pluginName)
            where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            var simulation = new SimulationModel1<TPlugin, TAgent>(spaceDefinition, simulationPlugin, pluginName, AgentsCollisionModel);
            return new SimulationModel1Visualisation<TPlugin, TAgent>(simulation);
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
    }
}
