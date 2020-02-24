using System;
using System.Collections.Generic;
using System.IO;
using AI.NeuralNetworks;
using Games.TicTacToe;

namespace AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeResultProbabilitiesNeuralNetwork
    {
        public static Func<GameState, double[]> DefaultInputFunction = TicTacToeNeuralIOLoader.InputTransforms.Unipolar;

        //public static GameAction GetBestAction(TicTacToeResultProbabilitiesNeuralNetwork network, GameState gameState)
        //{
        //    //double[] input = new double[9];
        //    //gameState.ToArray(TicTacToeNeuralIOLoader.InputFunctions.Unipolar, input);
        //    double[] prediction = network.Evaluate(gameState);
        //    GameAction bestAction = null;
        //    double bestActionValue = double.MinValue;
        //
        //    foreach (GameAction action in TicTacToeGame.Instance.GetAllowedActions(gameState))
        //    {
        //        double actionOutput = network.Output[action.Y * 3 + action.X];
        //
        //        if (actionOutput > bestActionValue)
        //        {
        //            bestAction = action;
        //            bestActionValue = actionOutput;
        //        }
        //    }
        //
        //    return bestAction;
        //}

        public Func<FieldState, double> InputFunction;
        public Network Network { get; }

        public TicTacToeResultProbabilitiesNeuralNetwork(int inputSize, int[] layersSize, IFunction activationFunction, Random random) 
        {
            Network = new Network(activationFunction, inputSize, 9, layersSize);
        }
        
        public TicTacToeResultProbabilitiesNeuralNetwork()
        {
            Network = new Network(Function.Sigmoidal, 9, 9, new int[] { 9, 9 });
        }

        public double[] Evaluate(GameState gameState)
        {
            double[] input = new double[9];
            gameState.ToArray(InputFunction, input);
            return Network.Evaluate(input);
        }
    }
}
