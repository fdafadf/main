using System;
using AI.TicTacToe;
using Games.TicTacToe;

namespace AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeValueNetwork
    {
        public static Func<GameState, double[]> DefaultInputTransform = TicTacToeLabeledStateLoader.InputTransforms.Unipolar;

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

        public Network Network { get; }
        public Func<GameState, double[]> InputTransform;
        public Func<double[], TicTacToeValue> OutputTransform;

        public TicTacToeValueNetwork(int[] hiddenLayerSizes, FunctionName activationFunction, Random random) 
        {
            Network = NetworkBuilder.Build(new NetworkDefinition(activationFunction, 9, 3, hiddenLayerSizes), new He(0));
            InputTransform = DefaultInputTransform;
            OutputTransform = TicTacToeLabeledStateLoader.OutputTransforms.ResultProbabilities;
        }
        
        public TicTacToeValueNetwork()
        {
            Network = NetworkBuilder.Build(new NetworkDefinition(FunctionName.Sigmoidal, 9, 9, new int[] { 9, 9 }), new He(0));
        }

        public TicTacToeValue Evaluate(GameState gameState)
        {
            double[] input = InputTransform(gameState);
            double[] output = Network.Evaluate(input);
            return OutputTransform(output);
        }
    }
}
