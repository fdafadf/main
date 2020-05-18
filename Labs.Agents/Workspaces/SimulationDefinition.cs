using Labs.Agents.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Labs.Agents
{
    public enum AgentsCollisionModel
    {
        Destroy,
        Ghost,
    }

    public class SimulationDefinition : IValidatable
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
        public string Driver { get; set; }
        [Category("Agent")]
        public AgentsCollisionModel AgentsCollisionModel { get; set; }
        Workspace Workspace;

        public SimulationDefinition(Workspace workspace)
        {
            Workspace = workspace;
        }

        public void Validate()
        {
            Assert.NotNullOrWhiteSpace(Name, $"Property 'Name' in category 'General' can not be empty.");
            Assert.NotNullOrWhiteSpace(MovementSystem, $"Property 'System' in category 'Movement' can not be empty.");
            Assert.NotNullOrWhiteSpace(GoalSystem, $"Property 'System' in category 'Goal' can not be empty.");
            Assert.NotNullOrWhiteSpace(Space, $"Property 'Name' in category 'Space' can not be empty.");
            Assert.NotNullOrWhiteSpace(Driver, $"Property 'Name' in category 'Agent' can not be empty.");
        }

        public ISimulation CreateSimulation()
        {
            var spaceDefinition = Workspace.Spaces.GetByName(Space);
            var agentDriverDefinition = Workspace.GetAgentsDriver(Driver);
            var agentDriver = agentDriverDefinition.CreateDriver();
            var method = GetType().GetMethods().Where(m => m.Name == "CreateSimulation").Skip(1).First();
            method = method.MakeGenericMethod(agentDriver.GetType(), agentDriver.AgentType);
            return method.Invoke(this, new object[] { spaceDefinition, agentDriver, agentDriverDefinition.Name }) as ISimulation;
        }

        public ISimulation CreateSimulation<TDriver, TAgent>(ISpaceDefinition spaceDefinition, TDriver agentDriver, string agentName)
            where TDriver : SimulationAgentDriver<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> 
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            return new SimulationModel1<TDriver, TAgent>(spaceDefinition, agentDriver, agentName, AgentsCollisionModel);
        }

        public ISimulationViualisation CreateSimulationForm()
        {
            var spaceDefinition = Workspace.Spaces.GetByName(Space);
            var agentDriverDefinition = Workspace.GetAgentsDriver(Driver);
            var agentDriver = agentDriverDefinition.CreateDriver();
            var method = GetType().GetMethods().Where(m => m.Name == "CreateSimulationForm").Skip(1).First();
            method = method.MakeGenericMethod(agentDriver.GetType(), agentDriver.AgentType);
            return method.Invoke(this, new object[] { spaceDefinition, agentDriver, agentDriverDefinition.Name }) as ISimulationViualisation;
        }

        public ISimulationViualisation CreateSimulationForm<TDriver, TAgent>(ISpaceDefinition spaceDefinition, TDriver agentDriver, string agentName)
            where TDriver : SimulationAgentDriver<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent>
            where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
        {
            var simulation = new SimulationModel1<TDriver, TAgent>(spaceDefinition, agentDriver, agentName, AgentsCollisionModel);
            return new SimulationModel1Visualisation<TDriver, TAgent>(simulation);
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

        public IEnumerable<string> GetDrivers()
        {
            return Workspace.AgentsDrivers.Select(driver => driver.Name);
        }
    }
}
