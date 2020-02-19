using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Games.TicTacToe;
using Games.Utilities;
using Keras.Models;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Keras
{
    public class TicTacToeKerasExamples
    {
        public static void AllCases()
        {
            global::Keras.Keras.DisablePySysConsoleLog = true;
            //ConsoleUtility.WriteLine(ConsoleColor.Cyan, "[TicTacToe] Load Value Network And Test");
            //LoadValueNetworkAndTestOnAllStates();
            //ConsoleUtility.WriteLine(ConsoleColor.Cyan, "[TicTacToe] Train Value Network And Test");
            //TrainValueNetworkFromScratchAndTestOnAllStates(100);
            //ConsoleUtility.WriteLine(ConsoleColor.Cyan, "[TicTacToe] Train Policy Network And Test");
            //TrainPolicyNetworkFromScratchAndTestOnAllStates(100);
            ConsoleUtility.WriteLine(ConsoleColor.Cyan, "[TicTacToe] Train Unsupervised Policy-Value Network");
            TrainModelFromScratchWithMonteCarloAndTest();
        }

        public static void PredictTimeTest()
        {
            global::Keras.Keras.DisablePySysConsoleLog = true;
            string modelPath = @"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Workspace\TicTacToeKerasModel.bin";
            Console.WriteLine($"Loading Model");
            var model = new KerasModel(() => BaseModel.LoadModel(modelPath));
            var startTime = DateTime.Now;
            //Parallel.ForEach(Enumerable.Range(1, 100), (a) =>
            //{
            //});

            for (int i = 0; i < 100; i++)
            {
                model.Predict(new double[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 });
            }

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"{elapsedTime}");
            List<double[]> inputs = new List<double[]>();
            var startTime2 = DateTime.Now;

            for (int i = 0; i < 100; i++)
            {
                int a = i & 1;
                int b = i & 2;
                int c = i & 4;
                int d = i & 8;
                int e = i & 16;
                int f = i & 32;
                int g = i & 64;
                inputs.Add(new double[] { a, b, c, d, e, f, g, 0, 1 });
            }

            model.Predict(inputs.ToArray());
            var elapsedTime2 = DateTime.Now - startTime2;
            Console.WriteLine($"{elapsedTime2}");
        }

        public static void LoadValueNetworkAndTestOnAllStates()
        {
            string modelPath = @"C:\Users\pstepnowski\Source\Repos\fdafadf\basics\Workspace\TicTacToeKerasModel.bin";
            Console.WriteLine($"Loading Testing Data");
            TicTacToeTrainingData.Load(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, out double[][] inputs, out TicTacToeResultProbabilities[] outputs);
            Console.WriteLine($"Loading Model");
            var model = new KerasModel(() => BaseModel.LoadModel(modelPath));
            Console.WriteLine($"Testing Model (All Possible States)");
            TestModel(model, inputs, outputs, TicTacToeTrainingData.IsPredictionCorrect, out int correct, out int wrong);
            Console.WriteLine(string.Format("Correct: {0}", correct));
            Console.WriteLine(string.Format("Wrong: {0}", wrong));
        }

        public static void TrainValueNetworkFromScratchAndTestOnAllStates(int epoches)
        {
            Console.WriteLine($"Loading Training And Testing Data");
            TicTacToeTrainingData.Load(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, out double[][] inputs, out TicTacToeResultProbabilities[] outputs);
            Console.WriteLine($"Building Model");
            var model = new KerasModel(KerasModel.Loss_CrossEntropy, 3);
            Console.WriteLine($"Training Model (All Possible States, {epoches} Epoches)");
            model.Train(inputs, outputs.Select(o => o.Probabilities).ToArray(), epoches);
            Console.WriteLine($"Testing Model (All Possible States)");
            TestModel(model, inputs, outputs, TicTacToeTrainingData.IsPredictionCorrect, out int correct, out int wrong);
            Console.WriteLine(string.Format("Correct: {0}", correct));
            Console.WriteLine(string.Format("Wrong: {0}", wrong));
        }

        public static void TrainValueNetworkFromScratchAndSaveModel(int epoches, string modelPath)
        {
            Console.WriteLine($"Loading Training Data");
            TicTacToeTrainingData.Load(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, out double[][] inputs, out TicTacToeResultProbabilities[] outputs);
            Console.WriteLine($"Building Model");
            var model = new KerasModel(KerasModel.Loss_CrossEntropy, 3);
            Console.WriteLine($"Training Model (All Possible States, {epoches} Epoches)");
            model.Train(inputs, outputs.Select(o => o.Probabilities).ToArray(), epoches);
            Console.WriteLine($"Saving Model Graph");

            switch (Path.GetExtension(modelPath))
            {
                case ".keras":
                    model.Model.Save(modelPath);
                    break;
                case ".tf":
                    model.Model.SaveTensorflowJSFormat(modelPath);
                    break;
                default:
                    Console.WriteLine($"Unknown file extension");
                    break;
            }

            // modelPath.Replace(".keras", "")
            System.Diagnostics.Process.Start(@"C:\Users\pstepnowski\AppData\Local\Programs\Python\Python36\python.exe ""C:\Deployment\Convert.py""");
            //model.Model.Save()
        }

        public static void TrainPolicyNetworkFromScratchAndTestOnAllStates(int epoches)
        {
            Console.WriteLine($"Loading Training And Testing Data");
            TicTacToeTrainingData.Load(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, out double[][] inputs, out TicTacToeActionProbabilities[] outputs);
            Console.WriteLine($"Building Model");
            var model = new KerasModel(KerasModel.Loss_MeanSquared, 9);
            Console.WriteLine($"Training Model (All Possible States, {epoches} Epoches)");
            model.Train(inputs, outputs.Select(o => o.Probabilities).ToArray(), epoches);
            Console.WriteLine($"Testing Model (All Possible States)");
            TestModel(model, inputs, outputs, CompareOutputs, out int correct, out int wrong);
            Console.WriteLine(string.Format("Correct: {0}", correct));
            Console.WriteLine(string.Format("Wrong: {0}", wrong));
        }

        public static void TrainModelFromScratchWithMonteCarloAndTest()
        {
            Console.WriteLine("Loading Testing Data");
            TicTacToeTrainingData.Load(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, out double[][] inputs, out TicTacToePVNetworkOutput[] outputs);
            Console.WriteLine("Building Model");
            var mctsNetwork = new TicTacToeKerasPVNetwork();

            void TestCurrentModel()
            {
                Console.WriteLine("Testing Model  (All Possible States)");
                TestModel(mctsNetwork.Model, inputs, outputs, CompareOutputs, out int correct, out int wrong);
                Console.WriteLine(string.Format("Good: {0}", correct));
                Console.WriteLine(string.Format("Bad: {0}", wrong));
            }

            //PythonEngine.BeginAllowThreads();
            TestCurrentModel();
            PVNetworkTrainer<TicTacToeGame, GameState, GameAction, Player> trainer = new PVNetworkTrainer<TicTacToeGame, GameState, GameAction, Player>(TicTacToeGame.Instance, new GameState(), mctsNetwork);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Training Model");
                trainer.Epoch();
                TestCurrentModel();
            }
        }

        public static bool CompareOutputs(TicTacToePVNetworkOutput expectedOutput, float[] networkOutputs, int numberOfSample)
        {
            return ComparePolicyOutputs(expectedOutput.Raw, networkOutputs, numberOfSample);
        }

        public static bool CompareOutputs(TicTacToeActionProbabilities expectedOutput, float[] networkOutputs, int numberOfSample)
        {
            return ComparePolicyOutputs(expectedOutput.Probabilities, networkOutputs, numberOfSample);
        }

        public static bool ComparePolicyOutputs(double[] expectedOutput, float[] networkOutputs, int numberOfSample)
        {
            float[] output = new float[9];

            for (int i = 0; i < 9; i++)
            {
                output[i] = networkOutputs[numberOfSample * 9 + i];
            }

            int max = output.IndexOfMax();
            int expectedMax = expectedOutput.IndexOfMax();

            if (expectedMax == max)
            {
                return true;
            }
            else
            {
                return expectedOutput[max] == expectedOutput[expectedMax];
            }
        }

        private static void TestModel<T>(KerasModel model, double[][] inputs, T[] expectedOutputs, Func<T, float[], int, bool> outputComparer, out int correct, out int wrong)
        {
            correct = 0;
            wrong = 0;

            string boardLineToString(double[] input, int line)
            {
                string result = string.Empty;

                for (int x = 0; x < 3; x++)
                {
                    double v = input[line * 3 + x];
                    result += v < 0 ? "X" : (v > 0 ? "O" : ".");
                }

                return result;
            }

            float[] outputs = model.Predict(inputs);

            for (int i = 0; i < inputs.Length; i++)
            {
                double[] input = inputs[i];
                T expectedOutput = expectedOutputs[i];

                if (outputComparer(expectedOutput, outputs, i))
                {
                    correct++;
                }
                else
                {
                    //Console.WriteLine($"{boardLineToString(input, 0)}   O: {output[0]:f3}   O: {expectedOutput[0]:f3}");
                    //Console.WriteLine($"{boardLineToString(input, 1)}   X: {output[1]:f3}   X: {expectedOutput[1]:f3}");
                    //Console.WriteLine($"{boardLineToString(input, 2)}      {output[2]:f3}      {expectedOutput[2]:f3}");
                    wrong++;
                }
            }
        }
    }
}
