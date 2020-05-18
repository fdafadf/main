using AI.NeuralNetworks.Games;
using AI.NeuralNetworks.TicTacToe;
using AI.TicTacToe;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo
{
    public class PythonInterop
    {
        public double[][][] GetAllUniqueStates()
        {
            var trainingData = TicTacToeLabeledStateGenerator<TicTacToeValue>.Instance.GetAllUniqueStates(new TicTacToeValueEvaluator(TicTacToeLabeledStateLoader.InputTransforms.Bipolar));
            return ConvertToPython(trainingData);
        }

        public double[][][] GetTrainingDataFromFile()
        {
            var trainingDataFilePath = @"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Workspace\TicTacToe.NeuralNetwork\TrainData\BipolarInputThreeOutputs.txt";
            var trainingDataFile = new FileInfo(trainingDataFilePath);
            var trainingData = TicTacToeLabeledStateLoader.LoadPositions(trainingDataFile.OpenText(), TicTacToeLabeledStateLoader.InputTransforms.Bipolar);
            return ConvertToPython(trainingData);
        }

        public static double[][][] ConvertToPython<TGameState>(IEnumerable<LabeledState<TGameState, TicTacToeValue>> trainingData)
        {
            double[][] inputs = trainingData.Select(d => d.Input).ToArray();
            double[][] outputs = trainingData.Select(d => d.Label.Probabilities).ToArray();

            return new double[][][] {
                inputs,
                outputs
            };
        }
    }
}
