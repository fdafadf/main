using Basics.Games.TicTacToe;
using Basics.Main;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
{
    public class TicTacToeTestDataGenerator
    {
        TicTacToeGame game = new TicTacToeGame();

        public TreeNode<TicTacToeNeuralIO> Generate()
        {
            return Generate(new GameState());
        }

        private TreeNode<TicTacToeNeuralIO> Generate(GameState gameState)
        {
            TreeNode<TicTacToeNeuralIO> result;

            if (gameState.IsFinal)
            {
                double output = gameState.GetWinner().ToOutput();
                TicTacToeNeuralIO testData = new TicTacToeNeuralIO(gameState, output);
                result = new TreeNode<TicTacToeNeuralIO>(testData);
            }
            else
            {
                result = new TreeNode<TicTacToeNeuralIO>();

                foreach (GameAction action in game.GetAllowedActions(gameState))
                {
                    TreeNode<TicTacToeNeuralIO> child = Generate(game.Play(gameState, action));
                    result.Children.Add(child);
                }

                bool winOutputOccured = false;
                bool drawOutputOccured = false;
                double winOutput = gameState.CurrentPlayer.ToOutput();
                double loseOutput = gameState.CurrentPlayer.Opposite.ToOutput();

                foreach (TreeNode<TicTacToeNeuralIO> child in result.Children)
                {
                    if (child.Data.Output == winOutput)
                    {
                        winOutputOccured = true;
                    }
                    else if (child.Data.Output == 0)
                    {
                        drawOutputOccured = true;
                    }
                }

                double output = winOutputOccured ? winOutput : (drawOutputOccured ? 0 : loseOutput);
                result.Data = new TicTacToeNeuralIO(gameState, output);
            }

            //counter++;
            //Console.Title = $"{counter}";
            //Console.WriteLine(result.Data);
            //Console.WriteLine();
            //Console.ReadLine();
            return result;
        }
    }
}
