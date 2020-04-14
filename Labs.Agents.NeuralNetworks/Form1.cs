using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public partial class Form1 : SimulationForm
    {
        Simulation Simulation;
        IPainter EnvironmentPainter;

        public Form1()
        {
            InitializeComponent();
            InitializeMenu(this.toolStripContainer1);
            modeControl.SelectedIndex = 0;
        }

        protected override void LoadEnvironment(EnvironmentGeneratorBitmap generatorBitmap)
        {
            var environment = new MarkovEnvironment2<Agent, AgentState>(new Random(0), generatorBitmap.Width, generatorBitmap.Height);
            environment.AddObstacles(generatorBitmap.Obstacles);
            Simulation = new Simulation(environment, generatorBitmap.Agents);
            pictureBox1.Image = new Bitmap(environment.Width * 3, environment.Height * 3);
            stateControl.Image = new Bitmap(AgentNetworkInput.V * 10, AgentNetworkInput.V * 10);
            SimulationTask = new SimulationTask(Simulation.Step, RefreshEnvironment);
            EnvironmentPainter = new Action2EnvironmentPainter<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState, MarkovAgentInteraction<Agent, Action2, InteractionResult>>(environment);
            RefreshEnvironment();
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

            this.InvokeAction(() =>
            {
                toolStripStatusLabel1.Text = $"Step: {Simulation.StepNumber}";
                pictureBox1.Refresh();
            });
        }
    }
}
