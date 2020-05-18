using System.Windows.Forms;

namespace Labs.Agents
{
    public interface ILabForm
    {
        MenuStrip MenuAgentDrivers { get; }
        ToolStripMenuItem MenuNewSimulation { get; }
        ToolStripMenuItem MenuNewAgent { get; }
        ListView Simulations { get; }
        ListView AgentDrivers { get; }
    }
}
