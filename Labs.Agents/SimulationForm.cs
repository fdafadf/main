using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class SimulationForm : Form
    {
        protected SimulationTask SimulationTask;
        protected ToolStripStatusLabel IterationStatusLabel;
        protected EnvironmentGeneratorForm EnvironmentGeneratorForm = new EnvironmentGeneratorForm();
        private ToolStrip MainMenuControl;
        private ToolStripButton PauseButton;
        private ToolStripButton ResumeButton;

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SimulationTask?.IsStarted == true)
            {
                e.Cancel = true;
                SimulationTask?.Stop().ContinueWith(task => this.InvokeAction(Close));
            }
        }

        protected virtual void RefreshEnvironment()
        {
            IterationStatusLabel.Text = $"Iteration: {SimulationTask.Step}";
        }

        protected void InitializeMenu(ToolStripContainer container)
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

        protected void InitializeMenuItems(ToolStrip menu)
        {
            InitializeMenuEnvironment(menu);
            menu.AddSeparator();
            InitializeMenuStart(menu);
        }

        protected void InitializeMenuStart(ToolStrip menu)
        {
            PauseButton = menu.AddButton("Pause", Pause);
            ResumeButton = menu.AddButton("Resume", Resume);
        }

        protected void InitializeMenuEnvironment(ToolStrip menu)
        {
            var environmentMenu = menu.AddDropDownButton("Environment");
            environmentMenu.AddMenuItem("&Load...", LoadEnvironment);
            environmentMenu.AddSeparator();
        }

        protected void LoadEnvironment()
        {
            if (EnvironmentGeneratorForm.ShowDialog() == DialogResult.OK)
            {
                LoadEnvironment(EnvironmentGeneratorForm.EnvironmentBitmap);
            }
        }

        protected virtual void LoadEnvironment(EnvironmentGeneratorBitmap generatorBitmap)
        {
        }

        protected void Pause()
        {
            ResumeButton.Enabled = true;
            PauseButton.Enabled = false;
            SimulationTask?.Pause();
        }

        protected void Resume()
        {
            ResumeButton.Enabled = false;
            PauseButton.Enabled = true;
            SimulationTask?.Resume();
        }
    }
}
