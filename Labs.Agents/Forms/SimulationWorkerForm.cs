using Labs.Agents.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Labs.Agents.Forms
{
    public class SimulationWorkerForm : Form
    {
        public ISimulation Simulation;
        public BackgroundWorker SimulationWorker { get; }
        private ToolStrip MainMenuControl;
        private ToolStripButton PauseButton;
        private ToolStripButton ResumeButton;
        private ToolStripButton StepButton;

        public SimulationWorkerForm()
        {
            SimulationWorker = new BackgroundWorker(IterateSimulation);
            SimulationWorker.Paused += OnPaused;
            SimulationWorker.Resumed += OnResumed;
        }

        protected virtual bool IterateSimulation()
        {
            return Simulation.Iterate();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SimulationWorker.IsCompleted)
            {
                Simulation.Complete();
            }
            else
            {
                e.Cancel = true;
                SimulationWorker.Stop().ContinueWith(task => this.InvokeAction(Close));
            }
        }

        protected virtual void InitializeMenu(ToolStripContainer container)
        {
            CreateMenu(container, InitializeMenuItems);
        }

        protected virtual void InitializeMenuItems(ToolStrip menu)
        {
            InitializeMenuStart(menu);
        }

        protected virtual void InitializeMenuStart(ToolStrip menu)
        {
            ResumeButton = menu.AddButton(Icons.control, SimulationWorker.Resume);
            PauseButton = menu.AddButton(Icons.control_pause, SimulationWorker.Pause);
            StepButton = menu.AddButton(Icons.control_stop, () =>
            {
                SimulationWorker.SingleStep = true;
                SimulationWorker.Resume(); 
            });
        }

        protected virtual void OnPaused()
        {
            Simulation?.Pause();
            this.InvokeAction(() =>
            {
                ResumeButton.Enabled = StepButton.Enabled = true;
                PauseButton.Enabled = false;
            });
        }

        protected virtual void OnResumed()
        {
            this.InvokeAction(() =>
            {
                ResumeButton.Enabled = StepButton.Enabled = false;
                PauseButton.Enabled = true;
            });
        }

        protected void CreateMenu(ToolStripContainer container, Action<ToolStrip> itemsInitializer)
        {
            MainMenuControl = new ToolStrip();
            MainMenuControl.SuspendLayout();
            container.TopToolStripPanel.Controls.Add(MainMenuControl);
            MainMenuControl.Dock = DockStyle.None;
            itemsInitializer(MainMenuControl);
            MainMenuControl.Location = new Point(3, 0);
            MainMenuControl.Name = "mainMenuControl";
            MainMenuControl.Size = new Size(478, 25);
            MainMenuControl.TabIndex = 1;
            MainMenuControl.ResumeLayout(false);
            MainMenuControl.PerformLayout();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SimulationWorkerForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "SimulationWorkerForm";
            this.ResumeLayout(false);

        }
    }
}
