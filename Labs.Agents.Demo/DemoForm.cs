using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoEnvironment = Labs.Agents.Environment2<Labs.Agents.Demo.DemoAgent, Labs.Agents.Demo.DemoAgentState>;
using DemoInteraction = Labs.Agents.AgentInteraction<Labs.Agents.Demo.DemoAgent, Labs.Agents.Action2, Labs.Agents.InteractionResult>;

namespace Labs.Agents.Demo
{
    public partial class DemoForm : Form
    {
        SimulationTask SimulationTask;
        IPainter EnvironmentPainter;

        public DemoForm(DemoSimulation simulation)
        {
            InitializeComponent();
            SimulationTask = new SimulationTask(simulation.Step, RefreshEnvironment);
            EnvironmentPainter = new Action2EnvironmentPainter<DemoEnvironment, DemoAgent, DemoAgentState, DemoInteraction>(simulation.Environment);
        }

        protected void RefreshEnvironment()
        {
            this.InvokeAction(Refresh);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            EnvironmentPainter.Paint(e.Graphics);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    SimulationTask.PauseOrResume();
                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SimulationTask.IsStarted)
            {
                e.Cancel = true;
                SimulationTask?.Stop().ContinueWith(task => this.InvokeAction(Close));
            }
        }
    }
}
