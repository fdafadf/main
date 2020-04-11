using AI.NeuralNetworks;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.AI.NeuralNetworks.Ants
{
    public partial class AntsForm : Form
    {
        Training Trainer;
        Environment Environment;
        Brush SandBrush;
        CancellationTokenSource TrainingCancellation;
        bool TrainingPaused;
        EventWaitHandle PauseHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

        public AntsForm()
        {
            InitializeComponent();
            InitializeEnvironmentV2();
        }

        private void InitializeEnvironmentV1()
        {
            Environment = new Environment(ClientSize.Width, ClientSize.Height, new Random(0));
            Trainer = new Training<AntNetworkV1Input>(Environment, new AntNetworkV1());
            SandBrush = new SolidBrush(Environment.SandColor);
        }

        private void InitializeEnvironmentV2()
        {
            Environment = new Environment(ClientSize.Width, ClientSize.Height, new Random(0));
            Trainer = new Training<AntNetworkV2Input>(Environment, new AntNetworkV2(16, 32, 16));
            SandBrush = new SolidBrush(Environment.SandColor);
        }

        private void Train()
        {
            if (TrainingCancellation == null)
            {
                TrainingCancellation = new CancellationTokenSource();

                Task.Run(() =>
                {
                    // Pretrain without visualisation
                    //
                    //for (int episode = 0; episode < 1000; episode++)
                    //{
                    //    if (episode % 100 == 0)
                    //    {
                    //        Console.WriteLine(episode);
                    //    }
                    //
                    //    Trainer.Episode();
                    //}

                    while (TrainingCancellation.IsCancellationRequested == false)
                    {
                        if (TrainingPaused)
                        {
                            PauseHandle.Set();
                        }
                        else
                        {
                            Trainer.Episode();
                            this.InvokeAction(Refresh);
                        }
                    }

                    TrainingCancellation = null;
                    this.InvokeAction(Close);
                });
            }
            else
            {
                if (TrainingPaused)
                {
                    TrainingPaused = false;
                    PauseHandle.Reset();
                }
                else
                {
                    TrainingPaused = true;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (TrainingCancellation != null)
            {
                e.Cancel = true;
                TrainingCancellation.Cancel();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lock (Environment.Bitmap)
                {
                    using (Graphics g = Graphics.FromImage(Environment.Bitmap))
                    {
                        g.FillEllipse(SandBrush, e.X - 4, e.Y - 4, 8, 8);
                    }
                }

                Refresh();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Train();
                    break;
                case Keys.R:
                    Environment.Randomize(BackColor); 
                    Refresh();
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            lock (Environment.Bitmap)
            {
                e.Graphics.DrawImage(Environment.Bitmap, 0, 0);
                DrawAgent(e.Graphics, Environment.Agents[0]);
            }
        }

        private void DrawAgent(Graphics g, Agent item)
        {
            Ant agent = item.Ant;
            g.DrawLine(Pens.LightGreen, agent.Position.X, agent.Position.Y, agent.Goal.X, agent.Goal.Y);
            g.DrawLine(Pens.Black, agent.Position.X, agent.Position.Y, agent.Position.X + agent.Velocity.X * 20, agent.Position.Y + agent.Velocity.Y * 20);
            g.DrawEllipse(Pens.Green, agent.Goal.X - 2, agent.Goal.Y - 2, 4, 4);
            g.FillRectangle(Brushes.Blue, agent.Position.X - agent.Size, agent.Position.Y - agent.Size, agent.Size * 2, agent.Size * 2);
            DrawSensor(g, agent.Sensors[0], item.State.Signals[0]);
            DrawSensor(g, agent.Sensors[1], item.State.Signals[0]);
            DrawSensor(g, agent.Sensors[2], item.State.Signals[0]); 
        }

        private void DrawSensor(Graphics g, AntSensor sensor, double signal)
        {
            int size = sensor.Sensor.Size;

            if (signal > 0)
            {
                g.DrawRectangle(Pens.Red, sensor.Position.X - size, sensor.Position.Y - size, size * 2, size * 2);
            }
            else
            {
                g.DrawRectangle(Pens.DarkGray, sensor.Position.X - size, sensor.Position.Y - size, size * 2, size * 2);
            }
        }
    }
}
