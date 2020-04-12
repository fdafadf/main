using System;
using System.Drawing;
using System.Windows.Forms;
using DemoEnvironment = Labs.Agents.Environment2<Labs.Agents.Demo.DemoAgent, Labs.Agents.Demo.DemoAgentState>;

namespace Labs.Agents.Demo
{
    public class DemoSimulationForm : SimulationForm
    {
        DemoSimulation Simulation;
        int iterationNumber;

        public DemoSimulationForm(DemoSimulation simulation)
        {
            Simulation = simulation;
        }

        protected override void SimulationStep()
        {
            iterationNumber++;
            Simulation.Step();
            this.InvokeAction(() => { Text = $"Iteration: {iterationNumber}"; });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int scale = 3;
            var environment = Simulation.Environment;
            DrawObstacles<DemoEnvironment, DemoAgent, DemoAgentState>(e.Graphics, environment, scale);
            DrawGoals(e.Graphics, environment, scale);
            DrawAgents<DemoEnvironment, DemoAgent, DemoAgentState>(e.Graphics, environment, scale);
        }

        private void DrawGoals(Graphics graphics, DemoEnvironment environment, int scale)
        {
            foreach (var agent in environment.Agents)
            {
                var agentState = agent.State;

                if (agentState.Goal != Point.Empty)
                {
                    var ax = agentState.Field.X;
                    var ay = agentState.Field.Y;
                    var bx = agentState.Goal.X;
                    var by = agentState.Goal.Y;
                    graphics.DrawLine(Pens.LightBlue, ax * scale, ay * scale, bx * scale, by * scale);
                }
            }
        }
    }
}
