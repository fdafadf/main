using AI.NeuralNetworks;
using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Demos.Forms.Base;
using Games.TicTacToe;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Demos.Forms.TicTacToe.NeuralNetwork
{
    public partial class TicTacToeNeuralNetworkDemoForm : NeuralNetworkDemoForm<TicTacToeResultProbabilities>
    {
        TicTacToeNeuralBoardControl boardControl;

        public TicTacToeNeuralNetworkDemoForm()
        {
            InitializeComponent();
        }

        protected override AI.NeuralNetworks.NeuralNetwork<TicTacToeResultProbabilities> CreateNeuralNetwork()
        {
            Random random = new Random(Properties.Seed);
            return new TicTacToeNeuralNetwork(Properties.NetworkInputSize, Properties.NetworkLayersSize, ActivationFunctions.Sigmoid, random);
        }

        protected override Control InitializeMainControl()
        {
            boardControl = new TicTacToeNeuralBoardControl();
            boardControl.OnAction += BoardControl_OnAction;
            return boardControl;
        }

        protected override NeuralNetworkDemoFormProperties<TicTacToeResultProbabilities> InitializeProperties()
        {
            var properties = base.InitializeProperties();

            properties.NetworkInputSize = 9;
            properties.NetworkLayersSize = new[] { 9, 9, 9 };

            foreach (string trainDataFilePath in Directory.EnumerateFiles(Settings.TicTacToeNeuralNetworkTrainDataDirectoryPath, "*.txt"))
            {
                FileInfo trainDataFile = new FileInfo(trainDataFilePath);
                properties.AddTrainingSet(trainDataFile.Name, () => TicTacToeNeuralIOLoader.LoadPositions(trainDataFile.OpenText(), (NeuralNetwork as TicTacToeNeuralNetwork).InputFunction));
            }

            properties.AddTrainingSet("All States", () =>
            {
                return null; // TicTacToeNeuralIOGenerator<TicTacToeResultProbabilities>.Instance.GetAllUniqueStates(null, null);
            });

            return properties;
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
            NetworksPath = Settings.TicTacToeNeuralNetworkNetworksDirectoryPath;

            //string defaultNetworkPath = Path.Combine("Program.TicTacToeNeuralNetworkNetworksDirectoryPath", "Game.txt");
            //
            //try
            //{
            //    NeuralNetwork.LoadWeights(defaultNetworkPath);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(exception.Message);
            //}
            //
            //NetworkChanged();
        }
    }
}
