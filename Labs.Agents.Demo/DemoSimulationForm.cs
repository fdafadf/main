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
            int scale = 2;
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
                    else if (field.IsAgent)
                    {
                        var agentState = field.Agent.State;
                        Brush brush = agentState.IsDestroyed ? Brushes.Red : Brushes.Black;
                        e.Graphics.FillRectangle(brush, x * scale, y * scale, scale, scale);
                    }
                }
            }
        }
    }
}
