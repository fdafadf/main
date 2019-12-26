using Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe;
using Basics.AI.NeuralNetworks.Demos.Perceptron;
using Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe;
using Basics.AI.NeuralNetworks.Demos.Xor;
using Basics.AI.NeuralNetworks.TicTacToe;
using Basics.Games.Demos.TicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI.Games.Demos.TicTacToe;
using System;
using System.Collections.Generic;
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
            ShowForm(new PerceptronDemoForm());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //CreateAndShowForm<TicTacToeNeuralNetworkDemoForm>();

            //childForm = new PerceptronDemoForm();
            //childForm.MdiParent = this;
            //childForm.WindowState = FormWindowState.Maximized;
            //childForm.Show();
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

        private void ticTacToeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ais = TicTacToeNeuralNetwork.LoadAIs();

            Func<GameState, GameAction> GetAI(string name)
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
                        Func<GameState, GameAction> noughtAI = GetAI(settingsForm.Nought);
                        Func<GameState, GameAction> crossAI = GetAI(settingsForm.Cross);
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
