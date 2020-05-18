using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public partial class EnvironmentForm : SimulationForm
    {
        Simulation<Agent> Simulation;
        Painter Painter;
        AgentNetworkConfiguration networkConfiguration;
        TrainingConfiguration TrainingProperties;
        ToolTip toolTip = new ToolTip();
        PropertyGridForm networkForm = new PropertyGridForm();
        int Iterations;
        string Mode;

        public EnvironmentForm()
        {
            InitializeComponent();
            InitializeMenu(this.toolStripContainer1);

            var networksDirectory = Settings.ProductDirectory.EnsureDirectory("Networks");
            var defaultNetworkFile = new FileInfo(Path.Combine(networksDirectory.FullName, "Default.model"));
            AgentNetwork network;

            if (defaultNetworkFile.Exists)
            {
                network = new AgentNetwork(defaultNetworkFile);
                NetworkConfiguration = new AgentNetworkConfiguration(network);
            }
            else
            {
                NetworkConfiguration = AgentNetworkConfigurations.Small3;
                network = new AgentNetwork(networkConfiguration);
            }

            TrainingProperties = new TrainingConfiguration();
            networkStatusLabel.Name = "Network Configuration";
            networkStatusLabel.MouseHover += OnStatusLabelMouseHover;
            trainingStatusLabel.Text = TrainingProperties.ToString();
            trainingStatusLabel.Name = "Training Configuration";
            trainingStatusLabel.ToolTipText = TrainingProperties.ToToolTipString();
            trainingStatusLabel.MouseHover += OnStatusLabelMouseHover;
            directionPanel.Paint += DirectionPanel_Paint;

            Simulation = new Simulation<Agent>(null, null);
            //Simulation.SetNetwork(network);
            //Simulation.SetTraining(TrainingProperties);

            SpaceTemplateGeneratorProperties spaceGeneratorProperties = new SpaceTemplateGeneratorProperties()
            {
                Width = 50,
                Height = 30,
                NumberOfAgents = 1,
                NumberOfObstacles = 0,
                ObstacleMinSize = 2,
                ObstacleMaxSize = 8,
                Seed = 0,
            };

            SpaceTemplate = SpaceTemplateGenerator.Generate(spaceGeneratorProperties);
            LoadEnvironment(SpaceTemplate);

            SimulationWorker.Start();
        }

        protected override void OnPaused()
        {
            base.OnPaused();

            using (ChartForm chartForm = new ChartForm())
            {
                chartForm.Add("ReachedGoals", Simulation.Statistics.ReachedGoals);
                chartForm.Add("DestroyedAgents", Simulation.Statistics.DestroyedAgents);
                chartForm.ShowDialog();
            }
        }

        protected override void InitializeMenuItems(ToolStrip menu)
        {
            InitializeMenuEnvironment(menu);
            menu.AddSeparator();
            InitializeMenuNetwork(menu);
            menu.AddSeparator();
            menu.AddDropDownList(ModeChanged, "Testing", "Training");
            menu.AddSeparator();
            InitializeMenuStart(menu);
        }

        protected void InitializeMenuNetwork(ToolStrip menu)
        {
            var networksDirectory = Settings.ProductDirectory.EnsureDirectory("Networks");
            openNetworkDialog.InitialDirectory = networksDirectory.FullName;
            saveNetworkDialog.InitialDirectory = networksDirectory.FullName;
            var environmentMenu = menu.AddDropDownButton("&Network");
            environmentMenu.AddMenuItem("&Load...", LoadNetwork);
            environmentMenu.AddMenuItem("&Save...", SaveNetwork);
            environmentMenu.AddSeparator();
            environmentMenu.AddMenuItem("&Create...", CreateNetwork);
        }

        protected void CreateNetwork()
        {
            AgentNetworkConfiguration configuration = new AgentNetworkConfiguration();
            networkForm.PropertyGrid.SelectedObject = configuration;

            if (networkForm.ShowDialog() == DialogResult.OK)
            {
                //Simulation.SetNetwork(new AgentNetwork(configuration));
            }
        }

        protected void LoadNetwork()
        {
            if (openNetworkDialog.ShowDialog() == DialogResult.OK)
            {
                Network = new AgentNetwork(new FileInfo(openNetworkDialog.FileName));
            }
        }

        protected void SaveNetwork()
        {
            if (saveNetworkDialog.ShowDialog() == DialogResult.OK)
            {
                //Simulation.Network.Save(new FileInfo(saveNetworkDialog.FileName));
            }
        }

        protected override void LoadEnvironment(SpaceTemplate generatorBitmap)
        {
            //Simulation.SetEnvironment(generatorBitmap);
            //pictureBox1.Image = new Bitmap(generatorBitmap.Width * 3, generatorBitmap.Height * 3);
            //int encodedInputSize = Simulation.Network.InputCoder.ViewLength;
            //stateControl.Image = new Bitmap(encodedInputSize * 10, encodedInputSize * 10);
            //Painter = new Painter(Simulation.Space.InteractiveSpace);
            //RefreshEnvironment();
        }

        protected override bool IterateSimulation()
        {
            Iterations++;
            Simulation.Iterate();
            this.InvokeAction(RefreshEnvironment);
            return true; // Simulation.Agents.Any(agent => agent.Fitness.IsDestroyed == false);
        }

        protected void RefreshEnvironment()
        {
            simuationStatusLabel.Text = $"Iteration: {Iterations} Reached Goals: {Simulation.Goals.TotalReachedGoals}";
            
            using (Graphics graphics = Graphics.FromImage(pictureBox1.Image))
            {
                graphics.Clear(BackColor);
                Painter.PaintObstacles(graphics);
                //Painter.PaintGoals(graphics, Simulation.Agents.Where(agent => agent.Fitness.IsDestroyed == false));
                //Painter.PaintAgents(graphics, Simulation.Agents);
            }

            pictureBox1.Refresh();

            //if (Simulation.History.Any())
            //{
            //    var historyItem = Simulation.History.Last();
            //
            //    using (Graphics graphics = Graphics.FromImage(stateControl.Image))
            //    {
            //        graphics.Clear(BackColor);
            //        PaintHistoryItem(graphics, historyItem);
            //    }
            //
            //
            //    stateControl.Refresh();
            //    directionPanel.Refresh();
            //}
        }

        protected void ModeChanged(string mode)
        {
            Mode = mode;

            if (Simulation != null)
            {
                //Simulation.TrainingMode = Mode == "Training";
            }
        }

        private void DirectionPanel_Paint(object sender, PaintEventArgs e)
        {
            //if (Simulation.History.Any())
            //{
            //    var historyItem = Simulation.History.Last();
            //    int directionOffset = Simulation.Network.InputCoder.DirectionOffset;
            //    double sin = historyItem.Input[directionOffset + 0];
            //    double cos = historyItem.Input[directionOffset + 1];
            //    int cx = directionPanel.Width / 2;
            //    int cy = directionPanel.Height / 2;
            //    float x = (float)sin;
            //    float y = (float)cos;
            //    e.Graphics.DrawLine(Pens.Black, cx, cy, cx + x * cx, cy + y * cy);
            //}
        }

        private void PaintHistoryItem(Graphics graphics, HistoryItem historyItem)
        {
            //int obstaclesOffset = Simulation.Network.InputCoder.ObstaclesOffset;
            //
            //for (int y = 0; y < Simulation.Network.InputCoder.ViewLength; y++)
            //{
            //    for (int x = 0; x < Simulation.Network.InputCoder.ViewLength; x++)
            //    {
            //        var field = historyItem.Input[obstaclesOffset++];
            //        var pen = field > 0 ? Brushes.Black : Brushes.White;
            //        graphics.FillRectangle(pen, x * 10, y * 10, 9, 9);
            //    }
            //}
        }

        private AgentNetwork Network
        {
            set
            {
                //Simulation.SetNetwork(value);
                //NetworkConfiguration = new AgentNetworkConfiguration(value);
            }
        }

        private AgentNetworkConfiguration NetworkConfiguration
        {
            set
            {
                networkConfiguration = value;
                networkStatusLabel.Text = networkConfiguration.ToString();
                networkStatusLabel.ToolTipText = networkConfiguration.ToToolTipString();
            }
        }

        private void OnStatusLabelMouseHover(object sender, EventArgs e)
        {
            if (sender is ToolStripStatusLabel label)
            {
                toolTip.ToolTipIcon = ToolTipIcon.Info;
                toolTip.ToolTipTitle = label.Name;
                toolTip.Tag = label.ToolTipText;
                toolTip.SetToolTip(label.Owner, label.ToolTipText);
            }
        }
    }
}
