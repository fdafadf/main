using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basics.AI.NeuralNetworks;
using Newtonsoft.Json;
using System.IO;

namespace Basics.AI.NeuralNetworks.Demos
{
    public partial class NeuralNetworkDemoSettingsControl : UserControl
    {
        [Browsable(true)]
        public int NetworkInputSize { get; set; }

        [Browsable(true)]
        public int[] NetworkLayersSize { get; set; }

        public NeuralNetwork NeuralNetwork { get; private set; }
        public IEnumerable<NeuralIO> LoadedTrainingSet { get; private set; }

        public event EventHandler OnChanged;

        public NeuralNetworkDemoSettingsControl()
        {
            InitializeComponent();
            Changed();
        }

        public string NetworkStoragePath
        {
            set
            {
                saveFileDialog1.InitialDirectory = value;
                openFileDialog1.InitialDirectory = value;
            }
        }

        public void NewRandomNetwork()
        {
            Random random = new Random((int)seedControl.Value);
            NeuralNetwork = new NeuralNetwork(NetworkInputSize, NetworkLayersSize, ActivationFunctions.Sigmoid, random);
        }

        public void AddTrainingSet(string name, Func<IEnumerable<NeuralIO>> loader)
        {
            trainingSetsControl.Items.Add(new NamedObject<Func<IEnumerable<NeuralIO>>>(name, loader));

            if (trainingSetsControl.SelectedIndex == -1)
            {
                trainingSetsControl.SelectedIndex = 0;
            }
        }

        private void Changed()
        {
            trainButton.Enabled = LoadedTrainingSet != null;
            OnChanged?.Invoke(this, EventArgs.Empty);
        }

        private void loadTrainingSetButton_Click(object sender, EventArgs e)
        {
            var selectedItem = trainingSetsControl.SelectedItem as NamedObject<Func<IEnumerable<NeuralIO>>>;
            LoadedTrainingSet = selectedItem.Value();
            Changed();
        }

        private void trainButton_Click(object sender, EventArgs e)
        {
            if (LoadedTrainingSet != null)
            {
                int epoches = (int)epochesControl.Value;
                double alpha = (double)alphaControl.Value;
                double error = NeuralNetwork.Train(LoadedTrainingSet, epoches, alpha);
                lastErrorLabel.Text = $"{error:F4}";
                Changed();
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            NewRandomNetwork();
            Changed();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileContent = JsonConvert.SerializeObject(NeuralNetwork.GetWeights());
                File.WriteAllText(saveFileDialog1.FileName, fileContent);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(openFileDialog1.FileName);
                double[][][] weights = JsonConvert.DeserializeObject<double[][][]>(fileContent);
                NeuralNetwork.SetWeights(weights);
                Changed();
            }
        }
    }
}
