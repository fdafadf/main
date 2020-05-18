using System;
using System.Linq;
using System.Reflection;

namespace Labs.Agents
{
    public class Workspace
    {
        public WorkspaceItemsFile<SimulationFactory> Simulations { get; private set; }
        public WorkspaceItemsFile<ISimulationPluginFactory> SimulationPlugins { get; private set; }
        public WorkspaceSpaces Spaces { get; private set; }
        public WorkspaceItemsDirectory<SimulationResults> SimulationResults { get; private set; }

        public T GetSimulationPluginFactory<T>(string name) where T : ISimulationPluginFactory
        {
            return SimulationPlugins.OfType<T>().FirstOrDefault(driver => driver.Name == name);
        }

        public ISimulationPluginFactory GetSimulationPluginFactory(string name)
        {
            var result = SimulationPlugins.FirstOrDefault(driver => driver.Name == name);

            if (result == null)
            {
                throw new ArgumentException($"Agent '{name}' does not exist.");
            }

            return result;
        }

        protected void Load()
        {
            Spaces = new WorkspaceSpaces(Settings.ProductDirectory.GetFile("Spaces.json"), Settings.SpacesDirectory);
            SimulationPlugins = new WorkspaceItemsFile<ISimulationPluginFactory>(Settings.ProductDirectory.GetFile("SimulationPlugins.json"));
            Simulations = new WorkspaceItemsFile<SimulationFactory>(Settings.ProductDirectory.GetFile("Simulations.json"));
            SimulationResults = new WorkspaceItemsDirectory<SimulationResults>(Settings.ProductDirectory.GetDirectory("SimulationResults"));
            FieldInfo workspaceProperty = typeof(SimulationFactory).GetField("Workspace", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var simulation in Simulations)
            {
                workspaceProperty.SetValue(simulation, this);
            }
        }
    }
}
