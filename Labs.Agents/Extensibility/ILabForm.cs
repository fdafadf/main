using System.Windows.Forms;

namespace Labs.Agents
{
    public interface ILabForm
    {
        MenuStrip MenuAgents { get; }
        ToolStripMenuItem MenuNewSimulation { get; }
        ToolStripMenuItem MenuNewAgent { get; }
        ListView Simulations { get; }
        ListView Agents { get; }
    }
}
