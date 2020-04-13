using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public partial class Form1 : Form
    {
        Simulation Simulation;
        SimulationTask SimulationTask;
        IPainter EnvironmentPainter;

        public Form1()
        {
            InitializeComponent();
        }

        private void RefreshEnvironment()
        {
            using (Graphics graphics = Graphics.FromImage(pictureBox1.Image))
            {
                graphics.Clear(BackColor);
                EnvironmentPainter.Paint(graphics);
            }

            if (Simulation.History.Any())
            {
                var historyItem = Simulation.History.Last();

                using (Graphics graphics = Graphics.FromImage(stateControl.Image))
                {
                    graphics.Clear(BackColor);
                    EnvironmentPainter.Paint(graphics);
                    int offset = AgentNetworkInput.ObstaclesOffset;

                    for (int y = 0; y < AgentNetworkInput.V; y++)
                    {
                        for (int x = 0; x < AgentNetworkInput.V; x++)
                        {
                            var field = historyItem.Input.Encoded[offset++];
                            var pen = field > 0 ? Brushes.Black : Brushes.White;
                            graphics.FillRectangle(pen, x * 10, y * 10, 9, 9);
                        }
                    }
                }

                stateControl.InvokeAction(stateControl.Refresh);
            }

            pictureBox1.InvokeAction(pictureBox1.Refresh);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    SimulationTask.PauseOrResume();
                    break;
            }
        }

        private void resumeButton_Click(object sender, EventArgs e)
        {
            resumeButton.Enabled = false;
            pauseButton.Enabled = true;
            SimulationTask.Resume();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            resumeButton.Enabled = true;
            pauseButton.Enabled = false;
            SimulationTask.Pause();
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            Simulation = CreateSimulation(200, 150, 1, 0);
            var environment = Simulation.Environment;
            pictureBox1.Image = new Bitmap(environment.Width * 3, environment.Height * 3);
            stateControl.Image = new Bitmap(AgentNetworkInput.V * 10, AgentNetworkInput.V * 10);
            SimulationTask = new SimulationTask(Simulation.Step, RefreshEnvironment);
            EnvironmentPainter = new Action2EnvironmentPainter<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState, MarkovAgentInteraction<Agent, Action2, InteractionResult>>(environment);
            RefreshEnvironment();
            newButton.Enabled = false;
            resumeButton.Enabled = true;
            pauseButton.Enabled = false;
        }

        static Simulation CreateSimulation(int environmentWidth, int environmentHeight, int numberOfAgents, int numberOfObstacles)
        {
            var environment = new MarkovEnvironment2<Agent, AgentState>(new Random(0), environmentWidth, environmentHeight);
            EnvironmentGenerator.GenerateObstacles(environment, numberOfObstacles, 1, 20);
            return new Simulation(environment, numberOfAgents);
        }
    }
}
