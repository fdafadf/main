using Keras.Models;
using Numpy;
using System;
using System.Collections.Generic;

namespace AI.Keras
{
    public class KerasModel
    {
        public const string Loss_CrossEntropy = "categorical_crossentropy";
        public const string Loss_MeanSquared = "mean_squared_error";

        public BaseModel Model;

        public KerasModel(Func<BaseModel> modelLoader)
        {
            Model = modelLoader();
        }

        public KerasModel(string lossFunction, int outcomes)
        {
            var model = new Sequential();
            model.Add(new global::Keras.Layers.Dense(200, activation: "relu", input_dim: 9));
            model.Add(new global::Keras.Layers.Dropout(0.2));
            model.Add(new global::Keras.Layers.Dense(125, activation: "relu"));
            model.Add(new global::Keras.Layers.Dense(75, activation: "relu"));
            model.Add(new global::Keras.Layers.Dropout(0.1));
            model.Add(new global::Keras.Layers.Dense(25, activation: "relu"));
            model.Add(new global::Keras.Layers.Dense(outcomes, activation: "softmax"));
            model.Compile(loss: lossFunction, optimizer: "rmsprop", metrics: new[] { "acc" });
            Model = model;
        }

        public float[] Predict(double[] input)
        {
            List<NDarray> numpyInputList = new List<NDarray>();
            NDarray<double> a = new NDarray<double>(new double[input.Length]);
            numpyInputList.Add(a);
            NDarray predictBuffer = np.array(numpyInputList);
            return Model.Predict(predictBuffer).GetData<float>();
        }

        public float[] Predict(double[][] inputs)
        {
            return Model.Predict(ToNDarray(inputs)).GetData<float>();
        }

        public void Train(double[][] inputs, double[][] outputs, int epoches)
        {
            Model.Fit(ToNDarray(inputs), ToNDarray(outputs), epochs: epoches);
        }

        public static NDarray ToNDarray(double[][] data)
        {
            List<NDarray> numpyData = new List<NDarray>();

            for (int i = 0; i < data.Length; i++)
            {
                numpyData.Add(np.array(data[i]));
            }

            return np.array(numpyData);
        }
    }
}
