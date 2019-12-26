using System;
using System.Collections.Generic;
using System.IO;
using Basics.Games.Demos.TicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using Basics.MLNet;
using Newtonsoft.Json;

namespace Basics.AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeNeuralNetwork : NeuralNetwork
    {
        public static Func<FieldState, double> DefaultInputFunction = TicTacToeNeuralIO.InputFunctions.Unipolar;

        public static Dictionary<string, Func<GameState, GameAction>> LoadAIs()
        {
            Dictionary<string, Func<GameState, GameAction>> result = new Dictionary<string, Func<GameState, GameAction>>();

            if (TicTacToePythonInterop.IsInitialized)
            {
                result.Add("Computer (Keras)", gameState => TicTacToePythonInterop.Predict(gameState).BestAction);
            }
            else
            {
                result.Add("Computer (Keras)", null);
            }

            var mlNet = new TicTacToeMLNet(Program.TicTacToeMLModelPath);

            GameAction mlNetPredict(GameState gameState)
            {
                var predictions = TicTacToeGameActionPrediction.Predict(gameState, TicTacToeNeuralIO.InputFunctions.Bipolar, i => mlNet.Predict(i));
                int index;

                if (gameState.CurrentPlayer.IsCross)
                {
                    index = predictions.IndexOfMin(p => p.Value[0]);
                }
                else
                {
                    index = predictions.IndexOfMax(p => p.Value[0]);
                }

                return predictions[index].Key;
            }

            result.Add("Computer (ML.NET)", mlNetPredict);
            return result;
        }

        public static void LoadWeights(NeuralNetwork network, string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            double[][][] weights = JsonConvert.DeserializeObject<double[][][]>(fileContent);
            network.SetWeights(weights);
        }

        public static void SaveWeights(NeuralNetwork network, string filePath)
        {
            string fileContent = JsonConvert.SerializeObject(network.GetWeights());
            File.WriteAllText(filePath, fileContent);
        }

        public static GameAction GetBestAction(NeuralNetwork network, GameState gameState)
        {
            double[] input = new double[9];
            TicTacToeNeuralIO.ToInput(gameState, TicTacToeNeuralIO.InputFunctions.Unipolar, input);
            network.Evaluate(input);
            GameAction bestAction = null;
            double bestActionValue = double.MinValue;

            foreach (GameAction action in TicTacToeGame.Instance.GetAllowedActions(gameState))
            {
                double actionOutput = network.Output[action.Y * 3 + action.X];

                if (actionOutput > bestActionValue)
                {
                    bestAction = action;
                    bestActionValue = actionOutput;
                }
            }

            return bestAction;
        }

        public Func<FieldState, double> InputFunction;

        public TicTacToeNeuralNetwork(int inputSize, int[] layersSize, IActivationFunction activationFunction, Random random) : base(inputSize, layersSize, activationFunction, random)
        {
            InputFunction = DefaultInputFunction;
        }

        public TicTacToeNeuralNetwork() : base(9, new int[] { 9, 9 }, ActivationFunctions.Sigmoid, new Random())
        {
            InputFunction = DefaultInputFunction;
        }

        public void Evaluate(GameState gameState)
        {
            double[] input = new double[9];
            TicTacToeNeuralIO.ToInput(gameState, InputFunction, input);
            Evaluate(input);
        }
    }
}
