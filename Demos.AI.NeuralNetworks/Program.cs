using Core.NetStandard;

namespace Demos.AI.NeuralNetwork
{
    static class Program
    {
        static void Main()
        {
            //// Influence of network size on required epoches

            XorExamples.Train1(1500, 0.3, 4);
            XorExamples.Train1(1500, 0.3, 4, 6);
            XorExamples.Train1(3000, 0.3, 4, 6);

            //// MSE on chart

            //XorExamples.Compare();

            //// 2 categories visualisation

            //Application.Run(new PlaneForm(Function.Sigmoidal));
            //Application.Run(new PlaneForm(Function.ReLU));

            // TicTacToe with different hyperparameters

            //TicTacToeExamples.Compare1();
            //TicTacToeExamples.Compare2();
            //TicTacToeExamples.Compare3();
            TicTacToeExamples.Compare4();
        }
    }

    public class Storage : IStorage
    {
        public static readonly Storage Instance = new Storage();

        public T Read<T>(string resourceName)
        {
            throw new System.NotImplementedException();
        }

        public void Write<T>(string resourceName, T resource)
        {
            throw new System.NotImplementedException();
        }
    }
}
