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

            foreach (DemoAgent agent in Simulation.Agents)
            {
                if (agent.State.IsDestroyed)
                {
                    e.Graphics.FillRectangle(Brushes.Red, agent.State.Field.X * scale, agent.State.Field.Y * scale, scale, scale);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Black, agent.State.Field.X * scale, agent.State.Field.Y * scale, scale, scale);
                }
            }
        }
    }
}
