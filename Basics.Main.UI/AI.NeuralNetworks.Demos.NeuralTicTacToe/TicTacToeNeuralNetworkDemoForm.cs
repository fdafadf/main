using Basics.AI.NeuralNetworks.TicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe
{
    public partial class TicTacToeNeuralNetworkDemoForm : NeuralNetworkDemoForm
    {
        TicTacToeNeuralBoardControl boardControl;

        public TicTacToeNeuralNetworkDemoForm()
        {
            InitializeComponent();
        }

        protected override NeuralNetwork CreateNeuralNetwork()
        {
            Random random = new Random(Properties.Seed);
            return new TicTacToeNeuralNetwork(Properties.NetworkInputSize, Properties.NetworkLayersSize, ActivationFunctions.Sigmoid, random);
        }

        protected override Control CreateOutputControl()
        {
            boardControl = new TicTacToeNeuralBoardControl();
            boardControl.OnAction += BoardControl_OnAction;
            return boardControl;
        }

        protected override void InitializeProperties()
        {
            base.InitializeProperties();

            Properties.NetworkInputSize = 9;
            Properties.NetworkLayersSize = new[] { 9, 9, 1 };

            foreach (string trainDataFilePath in Directory.EnumerateFiles(Program.TicTacToeNeuralNetworkTrainDataDirectoryPath, "*.txt"))
            {
                FileInfo trainDataFile = new FileInfo(trainDataFilePath);
                Properties.AddTrainingSet(trainDataFile.Name, () => TicTacToeNeuralIO.Load(trainDataFile, (NeuralNetwork as TicTacToeNeuralNetwork).InputFunction));
            }

            Properties.AddTrainingSet("All States", () =>
            {
                return TicTacToeNeuralIOGenerator.Instance.GetAllUniqueStates(null, null);
            });
        }

        protected override void NetworkChanged()
        {
            base.NetworkChanged();
            boardControl.NeuralNetwork = NeuralNetwork as TicTacToeNeuralNetwork;
            boardControl.BoardState = boardControl.GameState;
        }

        protected override void InitializeMenuItems(ToolStrip toolStrip)
        {
            base.InitializeMenuItems(toolStrip);
            var resetButton = new ToolStripButton("&Reset Board");
            resetButton.Click += ResetButton_Click;
            toolStrip.Items.Add(resetButton);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            boardControl.BoardState = new GameState();
        }

        private void BoardControl_OnAction(GameAction action)
        {
            boardControl.BoardState = TicTacToeGame.Instance.Play(boardControl.GameState, action);
        }

        private void TicTacToeNeuralNetworkDemoForm_Load(object sender, EventArgs e)
        {
            string defaultNetworkPath = Path.Combine(Program.TicTacToeNeuralNetworkNetworksDirectoryPath, "Game.txt");

            try
            {
                TicTacToeNeuralNetwork.LoadWeights(NeuralNetwork, defaultNetworkPath);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            NetworkChanged();
        }
    }
}
