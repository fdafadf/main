using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents.Simulations.Dijkstra
{
    public class DijkstraLabFormExtension : LabFormExtension<DijkstraSimulationPluginFactory>
    {
        public DijkstraLabFormExtension(ILabForm labForm, Workspace workspace) : base(labForm, workspace)
        {
            if (workspace.SimulationPlugins.OfType<DijkstraSimulationPluginFactory>().Any() == false)
            {
                var driverDefinition = new DijkstraSimulationPluginFactory();
                workspace.SimulationPlugins.Add(driverDefinition);
                Add(driverDefinition);
            }
        }

        protected override void Add(DijkstraSimulationPluginFactory driverDefinition)
        {
            var item = new ListViewItem(driverDefinition.Name);
            item.SubItems.Add("Dijkstra");
            item.SubItems.Add("");
            item.Tag = driverDefinition;
            LabForm.Agents.AddWithAutoResize(item);
        }
    }
}
