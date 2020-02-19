using AI.Keras;
using AI.TFSharp;
using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Demos.Forms;
using Games;
using Games.Go;
using Games.Utilities;
using KelpNet.Common;
using KelpNet.Common.Functions.Container;
using KelpNet.Common.Tools;
using KelpNet.Functions.Activations;
using KelpNet.Functions.Connections;
using KelpNet.Functions.Noise;
using KelpNet.Loss;
using KelpNet.Optimizers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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
            var stack = new FunctionStack(
                //new Linear(9, 200),
                //new Linear(200, 125),
                //new Linear(125, 75),
                //new Linear(75, 25),
                //new Linear(25, 3)

                new Linear(9, 125),
                new ReLU(),
                new Linear(125, 75),
                new ReLU(),
                new Linear(75, 25),
                new ReLU(),
                new Linear(25, 3)

                //new Linear(2, 2, name: "l1 Linear"),
                //new Sigmoid(name: "l1 Sigmoid"),
                //new Linear(2, 2, name: "l2 Linear")
            );

            stack.SetOptimizer(new Adam());
            Random random = new Random();
            TicTacToeTrainingData.Load(TicTacToeNeuralIOLoader.InputTransforms.Relu, out double[][] inputs, out TicTacToeResultProbabilities[] expectedPredictions);
            inputs = inputs.Take(500).ToArray();
            var outputs = expectedPredictions.Take(500).Select(p => p.Probabilities).ToArray();

            void TrainModel(int epoches)
            {
                Console.WriteLine($"Training Model (All Possible States)");

                for (int epoch = 0; epoch < epoches; epoch++)
                {
                    random.Shuffle(inputs, expectedPredictions);
                    double errorSum = Trainer.Train(stack, inputs, outputs, new MeanSquaredError(), false);
                    stack.Update();
                    Console.WriteLine(string.Format("Error: {0}", errorSum));
                }
            }

            void TestModel()
            {
                Console.WriteLine($"Testing Model (All Possible States)");
                int correct = 0;
                int wrong = 0;
                //var predictions = stack.Predict(new NdArray(inputs));

                for (int i = 0; i < inputs.Length; i++)
                {
                    var prediction = stack.Forward(new NdArray(inputs[i]));
                    double[] output = prediction[0].Data.Select(v => v.Value).ToArray();

                    if (TicTacToeTrainingData.IsPredictionCorrect(expectedPredictions[i], output))
                    {
                        correct++;
                    }
                    else
                    {
                        wrong++;
                    }
                }

                Console.WriteLine(string.Format("Correct: {0}", correct));
                Console.WriteLine(string.Format("Wrong: {0}", wrong));
            }

            for (int i = 0; i < 10; i++)
            {
                TrainModel(10);
                TestModel();
            }
            //var input = new NdArray(new double[] { 1, 0, 1, 0, 1, 0, 0, 0, 0 });
            //var expectedOutput = new NdArray(new double[] { 0, 0, 1 });
            ////var output = stack.Forward(input);
            ////stack.Backward();
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(Trainer.Train(stack, input, expectedOutput, new SoftmaxCrossEntropy()));
            //    //stack.Update();
            //}

            //var prediction = stack.Predict(input);


            //Examples.Example1(Console.Out);
            //string modelPath = @"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Workspace\TicTacToeKerasModel.bin";
            //string modelPath = @"C:\Deployment\model.onnx";
            string kerasModelPath = @"C:\Deployment\model.tf";
            //
            //if (File.Exists(kerasModelPath) == false)
            //{
            //    TicTacToeKerasExamples.TrainValueNetworkFromScratchAndSaveModel(1, kerasModelPath);
            //}

            TicTacToeKerasExamples.AllCases();
            //TicTacToeKerasExamples.PredictTimeTest();
            ///TensorFlowSharp.PredictTimeTest(kerasModelPath);
            ///Basics.MLNet.MLTest.LoadModel(@"C:\Deployment\saved_model.pb");
            ///
            ///TensorFlowSharp.PredictTimeTest(kerasModelPath);
            //GoGameGeneratingPlayoutsTest();

            //var fullTree = Games.TicTacToe.TicTacToeGameStateGenerator.Instance.GetFullTree();
            //TicTacToeKerasModel.CreateModel(new FileInfo(Settings.TicTacToeKerasModelPath), 200);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoMainForm());
        }

        private static void GoGameGeneratingPlayoutsTest()
        {
            GameState gameState = new GameState(9);
            Stack<GameState> playout = null;
            var startTime = DateTime.Now;

            for (int i = 0; i < 1000; i++)
            {
                playout = gameState.RandomPlayout();
            }

            Console.WriteLine(DateTime.Now - startTime);
            List<string> joinedLines = new List<string>();

            for (int i = 0; i < playout.Count; i++)
            {
                string[] lines = playout.ElementAt(i).ToString().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (i % 10 == 0)
                {
                    joinedLines.Clear();
                    joinedLines.AddRange(lines);
                }
                else
                {
                    for (int j = 0; j < lines.Length; j++)
                    {
                        joinedLines[j] += "   ";
                        joinedLines[j] += lines[j];
                    }
                }

                if (i % 10 == 9)
                {
                    foreach (var joinedLine in joinedLines)
                    {
                        Console.WriteLine(joinedLine);
                    }

                    Console.WriteLine();
                }
            }
        }
    }

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

}
