using Basics.AI.NeuralNetworks;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public class TicTacToePerceptron : Basics.AI.NeuralNetworks.Perceptron
    {
        public static double DefaultActivationFunction(double output)
        {
            return output < -1 ? -2 : (output > 1 ? 2 : 0);
        }

        public TicTacToePerceptron() : base(9, DefaultActivationFunction, -50, 50)
        {
        }
    }
}
