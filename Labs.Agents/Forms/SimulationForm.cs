using System.Drawing;
using System.Windows.Forms;

namespace Labs.Agents.Forms
{
    public partial class SimulationForm : SimulationWorkerForm
    {
        Control environmentControl;

        public SimulationForm()
        {
            InitializeComponent();
            InitializeMenu(this.toolStripContainer1);
            SimulationWorker.Start();
        }

        public Control EnvironmentControl
        {
            set
            {
                environmentControl = value;
                environmentControl.Dock = DockStyle.Fill;
                environmentControl.Location = new Point(0, 0);
                environmentControl.TabIndex = 0;
                toolStripContainer1.ContentPanel.Controls.Add(environmentControl);
            }
        }

        protected override bool IterateSimulation()
        {
            bool result = base.IterateSimulation();
            this.InvokeAction(environmentControl.Refresh);
            return result;
        }
    }
}
