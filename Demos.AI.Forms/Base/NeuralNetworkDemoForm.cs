using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AI.NeuralNetworks;

namespace Demos.Forms.Base
{
    public class NeuralNetworkDemoForm : DemoForm<NeuralNetworkDemoFormProperties>
    {
        public Network NeuralNetwork { get; private set; }
        public Projection[] LoadedTrainingSet { get; private set; }

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ContextMenuStrip networkMenuStrip;
        private System.Windows.Forms.ContextMenuStrip trainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTrainingSetToolStripMenuItem;

        public NeuralNetworkDemoForm()
        {
        }

        protected override void InitializeMenu()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.networkMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trainMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTrainingSetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkMenuStrip.SuspendLayout();
            this.trainMenuStrip.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem1.Text = "&Randomize";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // loadMenuItem
            // 
            this.loadMenuItem.Name = "loadMenuItem";
            this.loadMenuItem.Size = new System.Drawing.Size(133, 22);
            this.loadMenuItem.Text = "&Load";
            this.loadMenuItem.Click += new System.EventHandler(this.loadMenuItem_Click);
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.Size = new System.Drawing.Size(133, 22);
            this.saveMenuItem.Text = "&Save";
            this.saveMenuItem.Click += new System.EventHandler(this.saveMenuItem_Click);
            // 
            // loadTrainingSetToolStripMenuItem
            // 
            this.loadTrainingSetToolStripMenuItem.Name = "loadTrainingSetToolStripMenuItem";
            this.loadTrainingSetToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.loadTrainingSetToolStripMenuItem.Text = "&Load Training Set";
            this.loadTrainingSetToolStripMenuItem.Click += new System.EventHandler(this.loadTrainingSetOnClick);
            // 
            // networkMenuStrip
            // 
            this.networkMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.loadMenuItem,
            this.saveMenuItem});
            this.networkMenuStrip.Name = "networkMenuStrip";
            this.networkMenuStrip.Size = new System.Drawing.Size(134, 70);
            this.networkMenuStrip.Text = "Network";
            // 
            // trainMenuStrip
            // 
            this.trainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTrainingSetToolStripMenuItem});
            this.trainMenuStrip.Name = "trainMenuStrip";
            this.trainMenuStrip.Size = new System.Drawing.Size(165, 26);
            this.trainMenuStrip.Text = "Train";
            this.trainMenuStrip.Click += new System.EventHandler(this.trainMenuStrip_Click);
            this.networkMenuStrip.ResumeLayout(false);
            this.trainMenuStrip.ResumeLayout(false);

            base.InitializeMenu();
        }

        protected override void InitializeMenuItems(ToolStrip toolStrip)
        {
            toolStrip.Items.Add<ToolStripMenuItem>(networkMenuStrip);
            toolStrip.Items.Add<ToolStripSplitButton>(trainMenuStrip).ButtonClick += trainMenuStrip_Click;
        }

        protected virtual Network CreateNeuralNetwork()
        {
            Random random = new Random(Properties.Seed);
            return new Network(Function.Sigmoidal, Properties.NetworkInputSize, 1, Properties.NetworkLayersSize);
        }

        protected override NeuralNetworkDemoFormProperties InitializeProperties()
        {
            NeuralNetworkDemoFormProperties properties = new NeuralNetworkDemoFormProperties();
            properties.NetworkInputSize = 2;
            properties.NetworkLayersSize = new [] { 2, 5, 1 };
            properties.TrainingAlpha = 0.3;
            properties.TrainingEpoches = 500;
            return properties;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NeuralNetwork = CreateNeuralNetwork();
            NetworkChanged();
        }

        private void loadTrainingSetOnClick(object sender, EventArgs e)
        {
            if (Properties.TrainingSet != null)
            {
                LoadedTrainingSet = Properties.TrainingSets.First(s => s.Name == Properties.TrainingSet).Value().ToArray();
                NetworkChanged();
            }
        }

        private void trainMenuStrip_Click(object sender, EventArgs e)
        {
            if (LoadedTrainingSet == null)
            {
                LoadedTrainingSet = Properties.TrainingSets.First(s => s.Name == Properties.TrainingSet).Value().ToArray();
            }

            if (LoadedTrainingSet != null)
            {
                var features = LoadedTrainingSet.Select(s => s.Input).ToArray();
                var labels = LoadedTrainingSet.Select(s => s.Output).ToArray();
                new Trainer(new SGD(NeuralNetwork, Properties.TrainingAlpha), new Random()).Train(LoadedTrainingSet, Properties.TrainingEpoches, 1);
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
                NeuralNetwork.LoadWeights(openFileDialog1.FileName);
                NetworkChanged();
            }
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NeuralNetwork.SaveWeights(saveFileDialog1.FileName);
            }
        }
    }
}
