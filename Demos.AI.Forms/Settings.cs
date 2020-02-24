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
        public static string AssetsPath => ConfigurationManager.AppSettings["AssetsPath"];
        public static string TicTacToeMLModelPath => Path.Combine(AssetsPath, "TicTacToeMLNetModel.zip");
        public static string TicTacToeKerasModelPath => Path.Combine(AssetsPath, "TicTacToeKerasModel.bin");
        public static string TicTacToeNeuralNetworkTrainDataDirectoryPath => Path.Combine(AssetsPath, "TicTacToe.NeuralNetwork", "TrainData");
        public static string TicTacToeNeuralNetworkNetworksDirectoryPath => Path.Combine(AssetsPath, "TicTacToe.NeuralNetwork", "Networks");
        public static string TicTacToePerceptronTrainDataDirectoryPath => Path.Combine(AssetsPath, "TicTacToe.Perceptron", "TrainData");
        public static string TicTacToePerceptronNetworksDirectoryPath => Path.Combine(AssetsPath, "TicTacToe.Perceptron", "Networks");
        public static string XorNeuralNetworkTrainDataDirectoryPath => Path.Combine(AssetsPath, "Xor.NeuralNetwork", "TrainData");
        public static string XorNeuralNetworkNetworksDirectoryPath => Path.Combine(AssetsPath, "Xor.NeuralNetwork", "Networks");
        public static string GoPreparedPositionsDirectoryPath => Path.Combine(AssetsPath, "Go");
    }
}
