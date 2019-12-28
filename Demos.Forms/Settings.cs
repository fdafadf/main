using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Forms
{
    public class Settings
    {
        public static string WorkspacePath => ConfigurationManager.AppSettings["WorkspacePath"];
        public static string TicTacToeMLModelPath => Path.Combine(WorkspacePath, "TicTacToeMLNetModel.zip");
        public static string TicTacToeNeuralNetworkTrainDataDirectoryPath => Path.Combine(WorkspacePath, "TicTacToe.NeuralNetwork", "TrainData");
        public static string TicTacToeNeuralNetworkNetworksDirectoryPath => Path.Combine(WorkspacePath, "TicTacToe.NeuralNetwork", "Networks");
        public static string TicTacToePerceptronTrainDataDirectoryPath => Path.Combine(WorkspacePath, "TicTacToe.Perceptron", "TrainData");
        public static string TicTacToePerceptronNetworksDirectoryPath => Path.Combine(WorkspacePath, "TicTacToe.Perceptron", "Networks");
        public static string XorNeuralNetworkTrainDataDirectoryPath => Path.Combine(WorkspacePath, "Xor.NeuralNetwork", "TrainData");
        public static string XorNeuralNetworkNetworksDirectoryPath => Path.Combine(WorkspacePath, "Xor.NeuralNetwork", "Networks");
    }
}
