using Basics.AI.NeuralNetworks;
using Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe;
using Basics.AI.NeuralNetworks.TicTacToe;
using Basics.Games.TicTacToe;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.Main.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
               /*
               Random random = new Random(0);
               NeuralNetwork2 network = new NeuralNetwork2(2, new[] { 5, 1 }, random);
               network.WriteWeights(Console.Out);
               Console.WriteLine();

               double[][] inputs =
               {
                   new [] { -1.0, -1.0 },
                   new [] { -1.0, +1.0 },
                   new [] { +1.0, -1.0 },
                   new [] { +1.0, +1.0 },
               };

               double[][] outputs =
               {
                   new [] { 0.0 },
                   new [] { 1.0 },
                   new [] { 1.0 },
                   new [] { 0.0 },
               };

               for (int i = 0; i < 10000; i++)
               {
                   for (int j = 0; j < inputs.Length; j++)
                   {
                       network.Train(inputs[j], outputs[j]);
                   }
               }

               for (int j = 0; j < inputs.Length; j++)
               {
                   network.Evaluate(inputs[j]);
                   Console.WriteLine(network.Output[0]);
               }
               */

               //var fullTree = new TicTacToeGameStateGenerator().FullTree();
               //var fullTreeFlatten = fullTree.Flatten();
               //Dictionary<int, GameState> uniqueStates = new Dictionary<int, GameState>();
               //
               //foreach (var gameState in fullTreeFlatten)
               //{
               //    int hashCode = gameState.GetHashCode();
               //
               //    if (uniqueStates.ContainsKey(hashCode) == false)
               //    {
               //        uniqueStates.Add(hashCode, gameState);
               //    }
               //}
               //
               //Console.WriteLine("All possible states: {0}", fullTreeFlatten.Count());
               //Console.WriteLine("Unique states: {0}", uniqueStates.Count());

               //var test = new PythonInterop().GetAllUniqueStates();

            //   var inputFunction = TicTacToeNeuralIO.InputFunctions.Bipolar;
            //var outputFunction = new TicTacToeMinMaxWinnerEvaluator();
            //var trainingData = TicTacToeNeuralIOGenerator.Instance.GetAllUniqueStates(inputFunction, outputFunction);
            //var csvLines = trainingData.Select(d => d.Input.Concatenate(",") + "," + d.Output.Concatenate(","));
            //File.WriteAllLines(@"C:\Deployment\TicTacToeTrainingData.txt", csvLines);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }


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
