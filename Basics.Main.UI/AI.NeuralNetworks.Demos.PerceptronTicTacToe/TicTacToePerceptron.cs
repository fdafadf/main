using Basics.AI.NeuralNetworks;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public class TicTacToePerceptron : NeuralNetworks.Perceptron
    {
        public TicTacToePerceptron() : base(9, ActivationFunctions.Sigmoid, -50, 50)
        {
        }
    }
}
