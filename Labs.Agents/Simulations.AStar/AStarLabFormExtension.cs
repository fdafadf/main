using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents.Simulations.AStar
{
    public class AStarLabFormExtension : LabFormExtension<AStarSimulationPluginFactory>
    {
        public AStarLabFormExtension(ILabForm labForm, Workspace workspace) : base(labForm, workspace)
        {
            if (workspace.SimulationPlugins.OfType<AStarSimulationPluginFactory>().Any() == false)
            {
                var pluginFactory = new AStarSimulationPluginFactory();
                workspace.SimulationPlugins.Add(pluginFactory);
                Add(pluginFactory);
            }
        }

        protected override void Add(AStarSimulationPluginFactory pluginFactory)
        {
            var item = new ListViewItem(pluginFactory.Name);
            item.SubItems.Add("A-Star");
            item.SubItems.Add("");
            item.Tag = pluginFactory;
            LabForm.Agents.AddWithAutoResize(item);
        }
    }
}
