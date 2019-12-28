using AI.NeuralNetworks;
using AI.NeuralNetworks.Games;
using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo
{
    public class PythonInterop
    {
        public double[][][] GetAllUniqueStates()
        {
            var trainingData = TicTacToeNeuralIOGenerator.Instance.GetAllUniqueStates(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, new TicTacToeMinMaxProbabilitiesEvaluator());
            return ConvertToPython(trainingData);
        }

        public double[][][] GetTrainingDataFromFile()
        {
            var trainingDataFilePath = @"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Workspace\TicTacToe.NeuralNetwork\TrainData\BipolarInputThreeOutputs.txt";
            var trainingDataFile = new FileInfo(trainingDataFilePath);
            var trainingData = TicTacToeNeuralIOLoader.LoadBipolarThreeOutputs(trainingDataFile.OpenText(), TicTacToeNeuralIOLoader.InputFunctions.Bipolar);
            return ConvertToPython(trainingData);
        }

        public static double[][][] ConvertToPython<TGameState>(IEnumerable<GameStateNeuralIO<TGameState>> trainingData)
        {
            double[][] inputs = trainingData.Select(d => d.Input).ToArray();
            double[][] outputs = trainingData.Select(d => d.Output).ToArray();

            return new double[][][] {
                inputs,
                outputs
            };
        }
    }
}
