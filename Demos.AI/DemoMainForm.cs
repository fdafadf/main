using System;
using System.Windows.Forms;
using Demos.Forms.TicTacToe.NeuralNetwork;
using Demos.Forms.Xor.NeuralNetwork;
using Demos.Forms.TicTacToe.MonteCarlo;
using Demos.Forms.TicTacToe.Game;
using Demos.Forms.Go.Game;

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
            //ShowForm(new PerceptronDemoForm());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowForm(new GoGameForm());
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
            //ShowForm(new PerceptronDemoForm());
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

        private void monteCarloTrainerDemoMenuItem_Click(object sender, EventArgs e)
        {
            //TicTacToeGame.Instance;
        }

        private void ticTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var engines = TicTacToeAI.GetEngines();

            //IGameAI<GameState, Player, GameAction> GetAI(string name)
            //{
            //    if (name.Equals("Human"))
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        var result = ais[name];
            //
            //        if (result == null)
            //        {
            //            throw new Exception($"{name} is not initialized.");
            //        }
            //
            //        return result;
            //    }
            //}

            using (var settingsForm = new TicTacToeGameSettingsForm(engines))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ShowForm(new TicTacToeGameForm(settingsForm.Nought, settingsForm.Cross));
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
