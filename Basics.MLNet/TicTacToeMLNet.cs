using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace Basics.MLNet
{
    public class TicTacToeMLNet
    {
        PredictionEngine<ModelInput, ModelOutput> predictionEngine;

        public TicTacToeMLNet(string modelPath)
        {
            Type t0 = typeof(DataKind);
            Type t1 = typeof(StandardTrainersCatalog);
            Type t2 = typeof(LightGbmExtensions);
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        public double[] Predict(double[] input)
        {
            ModelInput modelInput = new ModelInput
            {
                Col1 = (float)input[0],
                Col2 = (float)input[1],
                Col3 = (float)input[2],
                Col4 = (float)input[3],
                Col5 = (float)input[4],
                Col6 = (float)input[5],
                Col7 = (float)input[6],
                Col8 = (float)input[7],
                Col9 = (float)input[8],
            };

            var prediction = predictionEngine.Predict(modelInput);
            float output = prediction.Score[1];
            return new double[] { output };
        }
    }

    public class ModelInput
    {
        [ColumnName("1"), LoadColumn(0)]
        public float Col1 { get; set; }


        [ColumnName("2"), LoadColumn(1)]
        public float Col2 { get; set; }


        [ColumnName("3"), LoadColumn(2)]
        public float Col3 { get; set; }


        [ColumnName("4"), LoadColumn(3)]
        public float Col4 { get; set; }


        [ColumnName("5"), LoadColumn(4)]
        public float Col5 { get; set; }


        [ColumnName("6"), LoadColumn(5)]
        public float Col6 { get; set; }


        [ColumnName("7"), LoadColumn(6)]
        public float Col7 { get; set; }


        [ColumnName("8"), LoadColumn(7)]
        public float Col8 { get; set; }


        [ColumnName("9"), LoadColumn(8)]
        public float Col9 { get; set; }


        [ColumnName("R"), LoadColumn(9)]
        public bool R { get; set; }
    }

    public class ModelOutput
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        [ColumnName("PredictedLabel")]
        public Boolean Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
