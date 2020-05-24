using System;
using System.Linq;
using System.Reflection;

namespace Labs.Agents
{
    public class Workspace
    {
        public WorkspaceItemsDirectory<SimulationTemplateDefinition> SimulationTemplates { get; private set; }
        public WorkspaceItemsDirectory<ISimulationPluginFactory> SimulationPlugins { get; private set; }
        public WorkspaceSpaces Spaces { get; private set; }
        public WorkspaceSimulationResults SimulationResults { get; private set; }

        public T GetSimulationPluginFactory<T>(string name) where T : ISimulationPluginFactory
        {
            return SimulationPlugins.OfType<T>().FirstOrDefault(driver => driver.Name == name);
        }

        public ISimulationPluginFactory GetSimulationPluginFactory(string name)
        {
            var result = SimulationPlugins.FirstOrDefault(plugin => plugin.Name == name);

            if (result == null)
            {
                throw new ArgumentException($"Agent '{name}' does not exist.");
            }

            return result;
        }

        protected void Load()
        {
            Spaces = new WorkspaceSpaces(Settings.ProductDirectory.GetFile("Spaces.json"), Settings.SpacesDirectory);
            SimulationPlugins = new WorkspaceItemsDirectory<ISimulationPluginFactory>(this, Settings.ProductDirectory.GetDirectory("Agents"));
            SimulationTemplates = new WorkspaceItemsDirectory<SimulationTemplateDefinition>(this, Settings.ProductDirectory.GetDirectory("SimulationTemplates"));
            SimulationResults = new WorkspaceSimulationResults(this, Settings.ProductDirectory.GetDirectory("SimulationResults"));
        }
    }
}
