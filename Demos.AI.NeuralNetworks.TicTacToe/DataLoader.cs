using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Games.TicTacToe;
using System;
using System.Data;
using System.Linq;

namespace Demos.TicTacToe
{
    public class DataLoader
    {
        public static readonly Func<FieldState, double> InputTransform = TicTacToeNeuralIOLoader.InputFunctions.Bipolar;

        static double[][] trainingFeatures;
        static double[][] trainingLabels;
        static double[][] testingFeatures;
        static TicTacToeResultProbabilities[] testingLabels;

        public static void LoadData()
        {
            if (testingFeatures == null)
            {
                TicTacToeTrainingData.Load(InputTransform, out testingFeatures, out testingLabels);
                trainingFeatures = testingFeatures.Clone() as double[][];
                trainingLabels = testingLabels.Select(o => o.Probabilities).ToArray();
            }
        }

        public static double[][] TrainingFeatures
        {
            get
            {
                LoadData();
                return trainingFeatures;
            }
        }

        public static double[][] TrainingLabels
        {
            get
            {
                LoadData();
                return trainingLabels;
            }
        }

        public static double[][] TestingFeatures
        {
            get
            {
                LoadData();
                return testingFeatures;
            }
        }

        public static TicTacToeResultProbabilities[] TestingLabels
        {
            get
            {
                LoadData();
                return testingLabels;
            }
        }
    }
}
