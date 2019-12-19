using Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe;
using Basics.AI.NeuralNetworks.Demos.Perceptron;
using Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe;
using System;
using System.Windows.Forms;

namespace Basics.Main.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void newPerceptronDemoButton_Click(object sender, EventArgs e)
        {
            Form perceptronForm = new PerceptronDemoForm();
            perceptronForm.MdiParent = this;
            perceptronForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TicTacToeNeuralNetworkForm childForm = new TicTacToeNeuralNetworkForm();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ticTacToePerceptronButton_Click(object sender, EventArgs e)
        {
            TicTacToePerceptronForm childForm = new TicTacToePerceptronForm();
            childForm.MdiParent = this;
            childForm.Show();
        }
    }
}
