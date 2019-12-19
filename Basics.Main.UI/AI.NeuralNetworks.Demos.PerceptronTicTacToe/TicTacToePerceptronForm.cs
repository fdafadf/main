using Basics.AI.NeuralNetworks;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public partial class TicTacToePerceptronForm : Form
    {
        public TicTacToePerceptronForm()
        {
            InitializeComponent();
        }

        private void TicTacToePerceptronForm_Load(object sender, EventArgs e)
        {
            string workspacePath = ConfigurationManager.AppSettings["WorkspacePath"];
            string trainDataDirectoryPath = Path.Combine(workspacePath, "Perceptron.TicTacToe", "TrainData");
            trainDataSetsControl.SetItems(new DirectoryInfo(trainDataDirectoryPath).EnumerateFiles());
            string networksDirectoryPath = Path.Combine(workspacePath, "Perceptron.TicTacToe", "Networks");
            perceptronsControl.SetItems(new DirectoryInfo(networksDirectoryPath).EnumerateFiles());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //boardControl.BoardState = gameStates.First();
        }

        private void perceptronTicTacToeBoardControl1_OnAction(GameAction action)
        {
            GameState currentGameState = boardControl.GameState;
            GameState nextGameState = boardControl.Game.Play(currentGameState, action);
            boardControl.BoardState = nextGameState;
        }

        private void trainButton_Click(object sender, EventArgs e)
        {
            List<NeuralIO> loadedTrainData = new List<NeuralIO>();
            FileInfo fileInfo = trainDataSetsControl.SelectedItem as FileInfo;
            string[] gameStatesAsText = File.ReadAllText(fileInfo.FullName).Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<GameState> gameStates = gameStatesAsText.Select(GameState.Parse);

            foreach (GameState gameState in gameStates)
            {
                foreach (GameAction action in boardControl.Game.GetAllowedActions(gameState))
                {
                    GameState gameNextState = boardControl.Game.Play(gameState, action);
                    double output = gameNextState.GetWinner().ToOutput();
                    var testDataItem = new TicTacToeNeuralIO(gameNextState, output);
                    Console.WriteLine(testDataItem);
                    loadedTrainData.Add(testDataItem);
                }
            }

            boardControl.Perceptron.Train(loadedTrainData, 0.1, 10000);
            boardControl.BoardState = boardControl.GameState;
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            boardControl.Perceptron.Rand(-50, 50);
            boardControl.BoardState = boardControl.GameState;
        }
    }
}
