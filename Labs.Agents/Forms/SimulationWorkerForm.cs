using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.Forms
{
    public class SimulationWorkerForm : Form
    {
        public ISimulation Simulation;
        protected BackgroundWorker SimulationWorker;
        private ToolStrip MainMenuControl;
        private ToolStripButton PauseButton;
        private ToolStripButton ResumeButton;

        public SimulationWorkerForm()
        {
            SimulationWorker = new BackgroundWorker(IterateSimulation);
            SimulationWorker.Paused += OnPaused;
            SimulationWorker.Resumed += OnResumed;
        }

        int Iterations;

        protected virtual bool IterateSimulation()
        {
            Iterations++;
            var result = Simulation.Iterate();
            //this.InvokeAction(() => {
            //simulationStatusLabel.Text = $"Iteration: {Iterations} Reached Goals: Simulation.Goals.TotalReachedGoals";

            //});
            return result;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SimulationWorker.IsCompleted == false)
            {
                e.Cancel = true;
                SimulationWorker.Stop().ContinueWith(task => this.InvokeAction(Close));
            }
        }

        protected virtual void InitializeMenu(ToolStripContainer container)
        {
            MainMenuControl = new ToolStrip();
            MainMenuControl.SuspendLayout();
            container.TopToolStripPanel.Controls.Add(MainMenuControl);
            MainMenuControl.Dock = DockStyle.None;
            InitializeMenuItems(MainMenuControl);
            MainMenuControl.Location = new Point(3, 0);
            MainMenuControl.Name = "mainMenuControl";
            MainMenuControl.Size = new Size(478, 25);
            MainMenuControl.TabIndex = 1;
            MainMenuControl.ResumeLayout(false);
            MainMenuControl.PerformLayout();
        }

        protected virtual void InitializeMenuItems(ToolStrip menu)
        {
            InitializeMenuStart(menu);
        }

        protected void InitializeMenuStart(ToolStrip menu)
        {
            PauseButton = menu.AddButton("&Pause", SimulationWorker.Pause);
            ResumeButton = menu.AddButton("&Resume", SimulationWorker.Resume);
        }

        protected virtual void OnPaused()
        {
            this.InvokeAction(() =>
            {
                ResumeButton.Enabled = true;
                PauseButton.Enabled = false;
            });
        }

        protected virtual void OnResumed()
        {
            this.InvokeAction(() =>
            {
                ResumeButton.Enabled = false;
                PauseButton.Enabled = true;
            });
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SimulationForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SimulationForm";
            this.ResumeLayout(false);

        }
    }
}
