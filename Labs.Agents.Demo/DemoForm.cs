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
    public partial class DemoForm : SimulationForm
    {
        IPainter EnvironmentPainter;
        DemoEnvironment Environment;
        IEnumerable<DemoInteraction> EnvironmentIteractions;

        public DemoForm()
        {
            InitializeComponent();
            InitializeMenu(toolStripContainer1);
            InitializeSimulation();
            IterationStatusLabel = iterationStatusLabel;
            SimulationTask = new SimulationTask(SimulationStep, this.CreateInvoker(RefreshEnvironment));
            EnvironmentPainter = new Action2EnvironmentPainter<DemoEnvironment, DemoAgent, DemoAgentState, DemoInteraction>(Environment);
            environmentControl.Paint += ContentPanel_Paint;
        }

        protected override void RefreshEnvironment()
        {
            base.RefreshEnvironment();
            environmentControl.Refresh();
        }

        protected void SimulationStep()
        {
            foreach (var iteraction in EnvironmentIteractions)
            {
                iteraction.Action = iteraction.Agent.GetAction();
            }

            Environment.Apply(EnvironmentIteractions);
        }

        private void InitializeSimulation()
        {
            var environmentWidth = 200;
            var environmentHeight = 150;
            var numberOfAgents = 80;
            var numberOfObstacles = 180;
            var random = new Random(0);
            Environment = new DemoEnvironment(random, environmentWidth, environmentHeight);
            var obstacles = new bool[environmentWidth, environmentHeight];
            var agents = new bool[environmentWidth, environmentHeight];
            EnvironmentGenerator.GenerateObstacles(random, obstacles, numberOfObstacles, 1, 20);
            EnvironmentGenerator.GenerateAgents(random, obstacles, agents, numberOfAgents);
            Environment.AddObstacles(obstacles);
            EnvironmentIteractions = Environment.AddAgents(() => new DemoAgent(Environment), agents);
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            EnvironmentPainter.Paint(e.Graphics);
        }
    }
}
