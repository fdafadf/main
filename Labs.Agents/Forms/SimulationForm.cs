using Games.Utilities;
using Labs.Agents.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Labs.Agents.Forms
{
    public partial class SimulationForm : SimulationWorkerForm
    {
        public readonly AnimatedLayersControl Space;
        private ToolStripButton SlowButton;
        private ToolStripButton AccButton;
        int AnimationDuration = 4;

        public SimulationForm()
        {
            InitializeComponent();
            InitializeMenu(this.toolStripContainer1);
            Space = new AnimatedLayersControl();
            Space.Dock = DockStyle.Fill;
            Space.Location = new Point(0, 0);
            Space.TabIndex = 0;
            toolStripContainer1.ContentPanel.Controls.Add(Space);
        }

        protected override bool IterateSimulation()
        {
            bool result = base.IterateSimulation();
            Space.Start();
            this.InvokeAction(() =>
            {
                simulationStatusLabel.Text = Simulation.ToString();
            });
            return result;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SimulationWorker.Start();

            //if (Simulation is IShufflable shufflable)
            //{
            //    CreateMenu(this.toolStripContainer1, menu => menu.AddButton("&Shuffle", shufflable.Shuffle));
            //}
            
            Simulation.Initialise();
        }

        protected override void InitializeMenuStart(ToolStrip menu)
        {
            base.InitializeMenuStart(menu);
            SlowButton = menu.AddButton(Icons.control_double_180, Slow);
            AccButton = menu.AddButton(Icons.control_double, Accelerate);
        }

        private void ApplyAnimationDuration()
        {
            Space.Layers.OfType<AnimatedLayer>().SelectMany(l => l.Objects).OfType<AnimatedShape>().ForEach(o => o.Duration = AnimationDuration);
        }

        private void Accelerate()
        {
            AnimationDuration--;
            AccButton.Enabled = AnimationDuration > 1;
            ApplyAnimationDuration();
        }

        private void Slow()
        {
            AnimationDuration++;
            AccButton.Enabled = true;
            ApplyAnimationDuration();
        }
    }
}
