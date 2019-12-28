using System;
using System.Collections.Generic;
using System.IO;
using AI.NeuralNetworks;
using Games.TicTacToe;

namespace AI.TicTacToe.NeuralNetworks
{
    public class TicTacToeNeuralNetwork : NeuralNetwork
    {
        public static Func<FieldState, double> DefaultInputFunction = TicTacToeNeuralIOLoader.InputFunctions.Unipolar;

        public static GameAction GetBestAction(NeuralNetwork network, GameState gameState)
        {
            double[] input = new double[9];
            gameState.ToArray(TicTacToeNeuralIOLoader.InputFunctions.Unipolar, input);
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
            gameState.ToArray(InputFunction, input);
            Evaluate(input);
        }
    }
}
