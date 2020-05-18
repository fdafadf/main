using System;
using System.Linq;
using System.Reflection;

namespace Labs.Agents
{
    public class Workspace
    {
        public WorkspaceItemsFile<SimulationDefinition> Simulations { get; private set; }
        public WorkspaceItemsFile<ISimulationAgentDriverDefinition> AgentsDrivers { get; private set; }
        public WorkspaceSpaces Spaces { get; private set; }
        public WorkspaceItemsDirectory<SimulationResults> SimulationResults { get; private set; }

        public T GetAgentsDriver<T>(string name) where T : ISimulationAgentDriverDefinition
        {
            return AgentsDrivers.OfType<T>().FirstOrDefault(driver => driver.Name == name);
        }

        public ISimulationAgentDriverDefinition GetAgentsDriver(string name)
        {
            var result = AgentsDrivers.FirstOrDefault(driver => driver.Name == name);

            if (result == null)
            {
                throw new ArgumentException($"Agent '{name}' does not exist.");
            }

            return result;
        }

        protected void Load()
        {
            Spaces = new WorkspaceSpaces(Settings.ProductDirectory.GetFile("Spaces.json"), Settings.SpacesDirectory);
            AgentsDrivers = new WorkspaceItemsFile<ISimulationAgentDriverDefinition>(Settings.ProductDirectory.GetFile("AgentsDrivers.json"));
            Simulations = new WorkspaceItemsFile<SimulationDefinition>(Settings.ProductDirectory.GetFile("Simulations.json"));
            SimulationResults = new WorkspaceItemsDirectory<SimulationResults>(Settings.ProductDirectory.GetDirectory("SimulationResults"));
            FieldInfo workspaceProperty = typeof(SimulationDefinition).GetField("Workspace", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var simulation in Simulations)
            {
                workspaceProperty.SetValue(simulation, this);
            }
        }
    }
}
