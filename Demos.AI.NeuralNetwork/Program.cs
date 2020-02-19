using AI.NeuralNetwork;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demos.AI.NeuralNetwork
{
    static class Program
    {
        static void Main()
        {
            //// Influence of network size on required epoches

            XorExamples.Train(2000, 0.3, 4);
            XorExamples.Train(2000, 0.3, 4, 6);
            XorExamples.Train(5000, 0.3, 4, 6);

            //// MSE on chart

            XorExamples.Compare();

            //// 2 categories visualisation

            Application.Run(new PlaneForm(Function.Sigmoidal));
            Application.Run(new PlaneForm(Function.ReLU));

            // TicTacToe with different hyperparameters

            TicTacToeExamples.Compare1();
            TicTacToeExamples.Compare2();
            TicTacToeExamples.Compare3();
        }
    }
}
