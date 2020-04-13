using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Labs.Agents
{
    public partial class EnvironmentForm : Form 
    {
        public EnvironmentForm()
        {
            InitializeComponent();
        }

        protected virtual void SimulationStep()
        {

        }

        private void StartStopSimulation()
        {
        }

        protected override void OnClosing(CancelEventArgs e)
        {
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    StartStopSimulation();
                    break;
            }
        }
    }
}
