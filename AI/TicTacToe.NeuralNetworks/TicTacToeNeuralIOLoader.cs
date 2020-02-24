using AI.NeuralNetworks;
using AI.NeuralNetworks.Games;
using AI.TicTacToe;
using Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AI.NeuralNetworks.TicTacToe
{
    public abstract class TicTacToeNeuralIOLoader
    {
        public static int ToIndex(GameAction action)
        {
            return action.X + action.Y * 3;
        }

        public static class InputTransforms
        {
            public static double[] Bipolar(GameState gameState)
            {
                return gameState.ToArray(InputFunctions.Bipolar);
            }

            public static double[] Unipolar(GameState gameState)
            {
                return gameState.ToArray(InputFunctions.Unipolar);
            }

            public static double[] Relu(GameState gameState)
            {
                return gameState.ToArray();
            }
        }

        private static class InputFunctions
        {
            /// <summary>
            /// Maps field states to 0 for cross, to 1 for nought, to 0.5 for empty;
            /// </summary>
            public static Func<FieldState, double> Unipolar = FieldStateExtensions.Map(0.5, 1, 0);
            /// <summary>
            /// Maps field states to -1 for cross, to 1 for nought, to 0 for empty;
            /// </summary>
            public static Func<FieldState, double> Bipolar = FieldStateExtensions.Map(0.0, 1, -1);
        }

        public static IEnumerable<LabeledState<GameState, TicTacToeResultProbabilities>> LoadPositions(StreamReader fileInfo, Func<GameState, double[]> inputTransform)
        {
            string fileContent = fileInfo.ReadToEnd();
            Func<string, LabeledState<GameState, TicTacToeResultProbabilities>> positionParser = position => ParsePosition(position, inputTransform);
            return ParsePositions(fileContent, positionParser);
        }

        public static LabeledState<GameState, TicTacToeResultProbabilities> ParsePosition(string position, Func<GameState, double[]> inputTransform)
        {
            string[] components = position.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            GameState gameState = GameState.Parse(components[0], components[3], components[6]);
            double[] output = new double[3];
            output[0] = double.Parse(components[0 * 3 + 2]);
            output[1] = double.Parse(components[1 * 3 + 2]);
            output[2] = double.Parse(components[2 * 3 + 2]);
            var probabilities = new TicTacToeResultProbabilities(output);
            return new LabeledState<GameState, TicTacToeResultProbabilities>(gameState, inputTransform(gameState), output, probabilities);
        }

        public static IEnumerable<LabeledState<GameState, TOutput>> ParsePositions<TOutput>(string text, Func<string, LabeledState<GameState, TOutput>> positionParser)
        {
            List<LabeledState<GameState, TOutput>> result = new List<LabeledState<GameState, TOutput>>();
            string[] items = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                result.Add(positionParser(item));
            }

            return result;
        }

        //private static double[] ToInput(GameState gameState, Func<FieldState, double> inputFunction)
        //{
        //    double[] result = new double[9];
        //    ToInput(gameState, inputFunction, result);
        //    return result;
        //}
        //
        //public static void ToInput(GameState gameState, Func<FieldState, double> inputFunction, double[] input)
        //{
        //    for (int y = 0; y < gameState.BoardSize; y++)
        //    {
        //        for (int x = 0; x < gameState.BoardSize; x++)
        //        {
        //            input[y * gameState.BoardSize + x] = inputFunction(gameState[x, y]);
        //        }
        //    }
        //}

        public static IEnumerable<LabeledState<GameState, TOutput>> Parse<TOutput>(string text, Func<FieldState, double> inputFunction, Func<TOutput, double[]> outputFunction, Func<GameState, TOutput> typedOutputFunction)
        {
            List<LabeledState<GameState, TOutput>> result = new List<LabeledState<GameState, TOutput>>();
            string[] testDataItemsAsText = text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string testDataItemAsText in testDataItemsAsText)
            {
                string[] components = testDataItemAsText.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
                GameState gameState = GameState.Parse(components[0], components[2], components[4]);
                //double[] output = new double[9];
                //
                //for (int y = 0; y < 3; y++)
                //{
                //    for (int x = 0; x < 3; x++)
                //    {
                //        output[y * 3 + x] = components[y * 2 + 1][x] == '1' ? 1 : 0;
                //    }
                //}

                var input = gameState.ToArray(inputFunction);
                var typedOutput = typedOutputFunction(gameState);
                var output = outputFunction(typedOutput);
                result.Add(new LabeledState<GameState, TOutput>(gameState, input, output, typedOutput));
            }

            return result;
        }

        public static IEnumerable<LabeledState<GameState, TOutput>> Load<TOutput>(StreamReader fileInfo, Func<FieldState, double> inputFunction, Func<TOutput, double[]> outputFunction, Func<GameState, TOutput> typedOutputFunction)
        {
            string fileContent = fileInfo.ReadToEnd();
            return Parse(fileContent, inputFunction, outputFunction, typedOutputFunction);
        }

        //public override string ToString()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    builder.Append(GameState.ToString());
        //    builder.AppendLine($"Current Player: {GameState.CurrentPlayer.FieldState}");
        //    builder.AppendLine($"Output: {Output}");
        //    return builder.ToString();
        //}
    }
}
