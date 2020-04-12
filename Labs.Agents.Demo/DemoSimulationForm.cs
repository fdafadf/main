using System;
using System.Drawing;
using System.Windows.Forms;
using DemoEnvironment = Labs.Agents.Environment2<Labs.Agents.Demo.DemoAgent, Labs.Agents.Demo.DemoAgentState>;
using DemoInteraction = Labs.Agents.AgentInteraction<Labs.Agents.Demo.DemoAgent, Labs.Agents.Action2>;

namespace Labs.Agents.Demo
{
    public class DemoSimulationForm : Action2EnvironmentForm<DemoSimulation, DemoEnvironment, DemoAgent, DemoAgentState, DemoInteraction>
    {
        public DemoSimulationForm(DemoSimulation simulation) : base(simulation)
        {
        }
    }
}
