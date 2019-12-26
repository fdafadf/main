using Basics.Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Basics.AI.NeuralNetworks.TicTacToe
{
    public class TicTacToeNeuralIO : GameStateNeuralIO<GameState>
    {
        public static class InputFunctions
        {
            public static Func<FieldState, double> Unipolar = Map(0.5, 1, 0);
            public static Func<FieldState, double> Bipolar = Map(0, 1, -1);

            public static Func<FieldState, double> Map(double empty, double nought, double cross)
            {
                return fieldState =>
                {
                    switch (fieldState)
                    {
                        case FieldState.Cross:
                            return cross;
                        case FieldState.Nought:
                            return nought;
                        default:
                            return empty;
                    }
                };
            }
        }

        public static IEnumerable<TicTacToeNeuralIO> LoadBipolarThreeOutputs(FileInfo fileInfo, Func<FieldState, double> inputFunction)
        {
            string fileContent = File.ReadAllText(fileInfo.FullName);
            return ParseBipolarThreeOutputs(fileContent, inputFunction);
        }

        public static IEnumerable<TicTacToeNeuralIO> ParseBipolarThreeOutputs(string text, Func<FieldState, double> inputFunction)
        {
            List<TicTacToeNeuralIO> result = new List<TicTacToeNeuralIO>();
            string[] items = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                string[] components = item.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                GameState gameState = GameState.Parse(components[0], components[3], components[6]);
                double[] output = new double[3];
                output[0] = double.Parse(components[0 * 3 + 2]);
                output[1] = double.Parse(components[1 * 3 + 2]);
                output[2] = double.Parse(components[2 * 3 + 2]);
                result.Add(new TicTacToeNeuralIO(gameState, inputFunction, output));
            }

            return result;
        }

        private static double[] ToInput(GameState gameState, Func<FieldState, double> inputFunction)
        {
            double[] result = new double[9];
            ToInput(gameState, inputFunction, result);
            return result;
        }

        public static void ToInput(GameState gameState, Func<FieldState, double> inputFunction, double[] input)
        {
            for (int y = 0; y < gameState.BoardSize; y++)
            {
                for (int x = 0; x < gameState.BoardSize; x++)
                {
                    input[y * gameState.BoardSize + x] = inputFunction(gameState[x, y]);
                    //input[y * self.BoardSize + x] = self[x, y] == FieldState.Cross ? 0 : (self[x, y] == FieldState.Nought ? 1 : 0.5);
                }
            }
        }

        public static IEnumerable<TicTacToeNeuralIO> Parse(string text, Func<FieldState, double> inputFunction)
        {
            List<TicTacToeNeuralIO> result = new List<TicTacToeNeuralIO>();
            string[] testDataItemsAsText = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string testDataItemAsText in testDataItemsAsText)
            {
                string[] components = testDataItemAsText.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                GameState gameState = GameState.Parse(components[0], components[2], components[4]);
                double[] output = new double[9];

                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        output[y * 3 + x] = components[y * 2 + 1][x] == '1' ? 1 : 0;
                    }
                }

                result.Add(new TicTacToeNeuralIO(gameState, inputFunction, output));
            }

            return result;
        }

        public static IEnumerable<TicTacToeNeuralIO> Load(FileInfo fileInfo, Func<FieldState, double> inputFunction)
        {
            string fileContent = File.ReadAllText(fileInfo.FullName);
            return Parse(fileContent, inputFunction);
        }

        public TicTacToeNeuralIO(GameState gameState, Func<FieldState, double> inputFunction, double[] output) : base(gameState, ToInput(gameState, inputFunction), output)
        {
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GameState.ToString());
            builder.AppendLine($"Current Player: {GameState.CurrentPlayer.FieldState}");
            builder.AppendLine($"Output: {Output}");
            return builder.ToString();
        }
    }
}
