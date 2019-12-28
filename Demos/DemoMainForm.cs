using AI.NeuralNetworks;
using Games.TicTacToe;
using System;
using System.Windows.Forms;
using AI.NeuralNetworks.Games;
using Demos.Forms.Perceptron;
using Demos.Forms.TicTacToe.NeuralNetwork;
using Demos.Forms.Xor.NeuralNetwork;
using Demos.Forms.TicTacToe.MonteCarlo;
using Demos.Forms.TicTacToe.Game;

namespace Demo
{
    public partial class DemoMainForm : Form
    {
        public DemoMainForm()
        {
            InitializeComponent();
        }

        private void newPerceptronDemoButton_Click(object sender, EventArgs e)
        {
            ShowForm(new PerceptronDemoForm());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void ticTacToePerceptronButton_Click(object sender, EventArgs e)
        {
            ShowForm(new TicTacToeNeuralNetworkDemoForm());
        }

        private void newXorDemoButton_Click(object sender, EventArgs e)
        {
            ShowForm(new XorDemoForm());
        }

        private void ShowForm(Form form)
        {
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }

        private void linearFunctionSolverMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new PerceptronDemoForm());
        }

        private void neuralXorMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new XorDemoForm());
        }

        private void neuralTicTacToeTMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new TicTacToeNeuralNetworkDemoForm());
        }

        private void ticTacToeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowForm(new TicTacToeMonteCarloDemoForm());
        }

        private void ticTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ais = TicTacToeAI.GetEngines();

            IGameAI<GameState, Player, GameAction> GetAI(string name)
            {
                if (name.Equals("Human"))
                {
                    return null;
                }
                else
                {
                    var result = ais[name];

                    if (result == null)
                    {
                        throw new Exception($"{name} is not initialized.");
                    }

                    return result;
                }
            }

            using (var settingsForm = new TicTacToeGameSettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        IGameAI<GameState, Player, GameAction> noughtAI = GetAI(settingsForm.Nought);
                        IGameAI<GameState, Player, GameAction> crossAI = GetAI(settingsForm.Cross);
                        ShowForm(new TicTacToeGameForm(noughtAI, crossAI));
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }
    }
}
