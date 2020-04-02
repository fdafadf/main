using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents
{
    public partial class AgentsForm : Form
    {
        Simulation simulation;

        public AgentsForm()
        {
            InitializeComponent();
            ClientSize = new Size(800, 600);
            simulation = new Simulation(800, 600, 120, 80, new Random());
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (Obstacle obstacle in simulation.Scene.Obstacles)
            {
                e.Graphics.DrawRectangle(Pens.Black, obstacle.Position.X, obstacle.Position.Y, obstacle.Size.X, obstacle.Size.Y);
            }

            foreach (Agent agent in simulation.Scene.Agents)
            {
                Pen pen = agent.Collide(simulation.Scene) ? Pens.Red : Pens.Blue;
                e.Graphics.DrawEllipse(pen, agent.Position.X - agent.Size, agent.Position.Y - agent.Size, agent.Size * 2, agent.Size * 2);

                if (Vector2.Zero.Equals(agent.Target) == false)
                {
                    e.Graphics.DrawLine(Pens.Yellow, agent.Position.X, agent.Position.Y, agent.Target.X, agent.Target.Y);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            simulation.Update(1);
            Refresh();
        }
    }
}
