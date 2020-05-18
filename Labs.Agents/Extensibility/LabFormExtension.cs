using Games.Utilities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents
{
    public abstract class LabFormExtension<TPluginFactory>
        where TPluginFactory : ISimulationPluginFactory
    {
        public ILabForm LabForm { get; }

        public LabFormExtension(ILabForm labForm, Workspace workspace)
        {
            LabForm = labForm;
            workspace.SimulationPlugins.OfType<TPluginFactory>().ForEach(Add);
        }

        protected abstract void Add(TPluginFactory pluginFactory);

        protected void AddNewAgentMenuItem(string text, Action<object, EventArgs> clickAction)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Size = new System.Drawing.Size(154, 22);
            menuItem.Text = text;
            menuItem.Click += new EventHandler(clickAction);
            LabForm.MenuNewAgent.DropDownItems.Add(menuItem);
        }
    }
}
