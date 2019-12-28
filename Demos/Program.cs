using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Demo
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
            Application.Run(new DemoMainForm());
        }
    }
}
