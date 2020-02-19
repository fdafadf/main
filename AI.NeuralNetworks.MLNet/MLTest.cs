using AI.TicTacToe;
using AI.TicTacToe.NeuralNetworks;
using Microsoft.ML;
using Microsoft.ML.Transforms;
using System;
using System.IO;
using System.Linq;

namespace Basics.MLNet
{
    public class MLTest
    {
        public static void LoadModel(string modelPath)
        {
            //TensorflowCatalog.LoadTensorFlowModel(new ModelOperationsCatalog(), )
            //var trainingData = TicTacToeNeuralIOGenerator<TicTacToeResultProbabilities>.Instance.GetAllUniqueStates(TicTacToeNeuralIOLoader.InputFunctions.Bipolar, new TicTacToeResultProbabilitiesEvaluator());
            //var inputs = trainingData.Select(d => d.Input).ToArray();
            //var outputs = trainingData.Select(d => d.Output).ToArray();
            MLContext mlContext = new MLContext();
            TensorFlowModel model = mlContext.Model.LoadTensorFlowModel(modelPath);
            var estimator = mlContext.Transforms.ApplyOnnxModel(modelPath);
            //var model = mlContext.Model.Load(modelPath, out DataViewSchema inpuSchema);
            //var estimator = model.ScoreTensorFlowModel("Probabilities", "Fields");
            var transformer = estimator.Fit(null);
            var predictionEngine = mlContext.Model.CreatePredictionEngine< MLTestInput, MLTestOutput>(transformer);
            //MLTestInput input = new MLTestInput() { Fields = new double[] { 1, -1, 1, -1, 1, 0, 0, 0, 0 } };
            //MLTestOutput output = predictionEngine.Predict(input);
            //mlContext.Transforms.ApplyOnnxMode
            //mlContext.Model.Load()
        }

        public void Confirm(Func<bool> rule)
        {
            if (rule())
            {
                throw new Exception();
            }
        }
    }

    class Rule
    {
        public bool Confirm()
        {
            throw new Exception();
        }
    }

    public class MLTestInput
    {
        public double[] Fields;
    }

    public class MLTestOutput
    {
        public double[] Probabilities;
    }
}
