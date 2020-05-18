using Labs.Agents.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Labs.Agents.NeuralNetworks
{
    public class NetworkSimulationDefinition : ISimulationDefinition
    {
        public string Name { get; set; }
        [TypeConverter(typeof(DropDownStringConverter))]
        public string Space { get; set; }
        [TypeConverter(typeof(DropDownStringConverter))]
        public string AgentDriver { get; set; }
        public int Iterations { get; set; }

        public NetworkSimulationDefinition(string name, string space, string agentDriver)
        {
            Name = name;
            Space = space;
            AgentDriver = agentDriver;
        }

        public ISimulation CreateSimulation()
        {
            var spaceTemplate = Workspace.Instance.Spaces.GetByName(Space).CreateSpaceTemplate();
            var agentsDrivers = Workspace.Instance.AgentsDrivers.First(driver => driver.Name == AgentDriver);
            return null;
            //return new CardinalMovement_Destructible_RandomRenewableGoals_Simulation<NetworkAgent>(spaceTemplate, agentsDrivers);
        }

        //public CardinalMovement_Destructible_RandomRenewableGoals_Simulation<TAgent> CreateSimulation<TAgent>(SpaceTemplate spaceTemplate, SimulationAgentDriver<TAgent> agentDriver) 
        //    where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructible, IGoalAgent
        //{
        //    return new CardinalMovement_Destructible_RandomRenewableGoals_Simulation<TAgent>(spaceTemplate, agentDriver);
        //}

        //public void ShowSimulation()
        //{
        //    var spaceTemplate = Workspace.Instance.Spaces.GetByName(Space).CreateSpaceTemplate();
        //    var agentsDrivers = Workspace.Instance.GetAgentsDriver<NetworkSimulationAgentDriverDefinition>(AgentDriver).Create();
        //    var simulation = new CardinalMovement_Destructible_RandomRenewableGoals_Simulation<NetworkAgent>(spaceTemplate, agentsDrivers);
        //    var painter = new Painter(simulation.Space.InteractiveSpace);
        //    var form = new SimulationForm2();
        //    form.Simulation = simulation;
        //    form.Layers.Add(graphics => {
        //        painter.PaintObstacles(graphics);
        //        painter.PaintAgents(graphics, simulation.Agents);
        //        painter.PaintGoals(graphics, simulation.Agents);
        //    });
        //    form.Show();
        //}

        public IEnumerable<string> GetSpaces()
        {
            return Workspace.Instance.Spaces.Select(space => space.Name);
        }

        public IEnumerable<string> GetAgentsDrivers()
        {
            return Workspace.Instance.AgentsDrivers.OfType<NetworkSimulationAgentDriverDefinition>().Select(driver => driver.Name);
        }
    }
}
