using Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe;
using Basics.AI.NeuralNetworks.Demos.Xor;
using Basics.AI.NeuralNetworks.TicTacToe;
using Basics.Main.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos
{
    public partial class NeuralNetworkDemoForm : Form
    {
        protected NeuralNetworkDemoFormProperties Properties = new NeuralNetworkDemoFormProperties();
        public NeuralNetwork NeuralNetwork { get; private set; }
        public IEnumerable<NeuralIO> LoadedTrainingSet { get; private set; }
        protected Control OutputControl { get; private set; }

        public NeuralNetworkDemoForm()
        {
            InitializeComponent();
            InitializeProperties();

            OutputControl = CreateOutputControl();
            OutputControl.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(OutputControl);

            InitializeMenu();

            propertyGrid1.SelectedObject = Properties;
        }

        protected virtual void InitializeProperties()
        {
            Properties.NetworkInputSize = 2;
            Properties.NetworkLayersSize = new [] { 2, 5, 1 };
            Properties.TrainingAlpha = 0.3;
            Properties.TrainingEpoches = 500;
        }

        protected virtual Control CreateOutputControl()
        {
            return new Panel();
        }

        protected virtual void NetworkChanged()
        {
        }

        protected string NetworksPath
        {
            set
            {
                saveFileDialog1.InitialDirectory = value;
                openFileDialog1.InitialDirectory = value;
            }
        }

        protected virtual void InitializeMenuItems(ToolStrip toolStrip)
        {
            toolStrip.Items.Add<ToolStripMenuItem>(networkMenuStrip);
            toolStrip.Items.Add<ToolStripSplitButton>(trainMenuStrip).ButtonClick += trainMenuStrip_Click;
        }

        private void InitializeMenu()
        {
            foreach (Control control in propertyGrid1.Controls)
            {
                ToolStrip toolStrip = control as ToolStrip;

                if (toolStrip != null)
                {
                    InitializeMenuItems(toolStrip);
                    break;
                }
            }
        }

        protected virtual NeuralNetwork CreateNeuralNetwork()
        {
            Random random = new Random(Properties.Seed);
            return new NeuralNetwork(Properties.NetworkInputSize, Properties.NetworkLayersSize, ActivationFunctions.Sigmoid, random);
        }

        private void NeuralNetworkDemoForm_Load(object sender, EventArgs e)
        {
            NeuralNetwork = CreateNeuralNetwork();
        }

        private void loadTrainingSetOnClick(object sender, EventArgs e)
        {
            if (Properties.TrainingSet != null)
            {
                LoadedTrainingSet = Properties.TrainingSets.First(s => s.Name == Properties.TrainingSet).Value();
                NetworkChanged();
            }
        }

        private void trainMenuStrip_Click(object sender, EventArgs e)
        {
            if (LoadedTrainingSet == null)
            {
                LoadedTrainingSet = Properties.TrainingSets.First(s => s.Name == Properties.TrainingSet).Value();
            }

            if (LoadedTrainingSet != null)
            {
                Properties.TrainingLastError = NeuralNetwork.Train(LoadedTrainingSet, Properties.TrainingEpoches, Properties.TrainingAlpha);
                NetworkChanged();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NeuralNetwork = CreateNeuralNetwork();
            NetworkChanged();
        }

        private void loadMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TicTacToeNeuralNetwork.LoadWeights(NeuralNetwork, openFileDialog1.FileName);
                NetworkChanged();
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TicTacToeNeuralNetwork.SaveWeights(NeuralNetwork, saveFileDialog1.FileName);
            }
        }
    }
}
