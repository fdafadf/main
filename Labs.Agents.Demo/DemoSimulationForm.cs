using System.Drawing;
using System.Windows.Forms;

namespace Labs.Agents.Demo
{
    public class DemoSimulationForm : SimulationForm
    {
        DemoSimulation Simulation;

        public DemoSimulationForm(DemoSimulation simulation)
        {
            Simulation = simulation;
        }

        protected override void SimulationStep()
        {
            Simulation.Step();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int scale = 3;
            var environment = Simulation.Environment;

            for (int y = 0; y < environment.Height; y++)
            {
                for (int x = 0; x < environment.Width; x++)
                {
                    var field = environment[x, y];

                    if (field.IsObstacle)
                    {
                        e.Graphics.FillRectangle(Brushes.DarkGray, x * scale, y * scale, scale, scale);
                    }
                }
            }

            foreach (var agent in environment.Agents)
            {
                var agentState = agent.State;

                if (agentState.Goal != Point.Empty)
                {
                    var ax = agentState.Field.X;
                    var ay = agentState.Field.Y;
                    var bx = agentState.Goal.X;
                    var by = agentState.Goal.Y;
                    e.Graphics.DrawLine(Pens.LightBlue, ax * scale, ay * scale, bx * scale, by * scale);
                }
            }

            foreach (var agent in environment.Agents)
            {
                var agentState = agent.State;
                var x = agentState.Field.X;
                var y = agentState.Field.Y;
                Brush brush = agentState.IsDestroyed ? Brushes.Red : Brushes.Black;
                e.Graphics.FillRectangle(brush, x * scale, y * scale, scale, scale);
            }
        }
    }
}
