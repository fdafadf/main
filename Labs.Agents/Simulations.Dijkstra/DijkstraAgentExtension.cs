using System;
using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents.Dijkstra
{
    public class DijkstraAgentExtension : LabFormExtension<SimulationDijkstraAgentDriverDefinition>
    {
        public DijkstraAgentExtension(ILabForm labForm, Workspace workspace) : base(labForm, workspace)
        {
            if (workspace.AgentsDrivers.OfType<SimulationDijkstraAgentDriverDefinition>().Any() == false)
            {
                var driverDefinition = new SimulationDijkstraAgentDriverDefinition();
                workspace.AgentsDrivers.Add(driverDefinition);
                Add(driverDefinition);
            }
        }

        protected override void Add(SimulationDijkstraAgentDriverDefinition driverDefinition)
        {
            var item = new ListViewItem(driverDefinition.Name);
            item.SubItems.Add("Dijkstra");
            item.SubItems.Add("");
            item.Tag = driverDefinition;
            LabForm.AgentDrivers.Items.Add(item);
        }
    }
}
