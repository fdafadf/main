using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            simulation = new Simulation(1600, 1200, 120, 80, new Random());
            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.ScaleTransform(ClientSize.Width / simulation.Scene.Size.X, ClientSize.Height / simulation.Scene.Size.Y);

            foreach (Rectangle obstacle in simulation.Scene.Rectangles)
            {
                e.Graphics.FillRectangle(Brushes.Black, obstacle.Position, obstacle.Size);
            }

            foreach (Agent agent in simulation.Agents)
            {
                if (Vector2.Zero.Equals(agent.Target) == false)
                {
                    e.Graphics.DrawLine(Pens.Yellow, agent.SceneObject.Position, agent.Target);
                }

                Pen pen = simulation.Scene.Collide(agent.SceneObject) ? Pens.Red : Pens.Blue;
                e.Graphics.DrawEllipse(pen, agent.SceneObject.Position.X - agent.SceneObject.Radius, agent.SceneObject.Position.Y - agent.SceneObject.Radius, agent.SceneObject.Radius * 2, agent.SceneObject.Radius * 2);

                //foreach (Vector2 rotation in simulation.Rotations)
                //{
                //    e.Graphics.DrawLine(pen, agent.SceneObject.Position, agent.SceneObject.Position + rotation * 15);
                //}
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = $"Elapsed Time: {simulation.ElapsedTime:f0}, Targets Achieved: {simulation.TargetsAchieved}";
            simulation.Update(1);
            simulation.Update(1);
            simulation.Update(1);
            simulation.Update(1);
            simulation.Update(1);
            Refresh();
        }
    }
}
