using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class NeuralNetwork<TOutput>
    {
        NeuralNetworkLayer[] Layers;

        public double[] Output => Layers.Last().Output;

        public NeuralNetwork(int inputSize, int[] layersSize, IActivationFunction activationFunction, Random random)
        {
            double[] input = new double[inputSize];
            Layers = new NeuralNetworkLayer[layersSize.Length];

            for (int i = 0; i < layersSize.Length; i++)
            {
                double[] output = new double[layersSize[i]];
                Layers[i] = new NeuralNetworkLayer(input, output, activationFunction, random);
                input = output;
            }
        }

        public void Evaluate(double[] input)
        {
            Layers.First().Input = input;
            Layers.ForEach(layer => layer.Evaluate());
        }

        public double Train(IEnumerable<NeuralIO<TOutput>> trainData, int maxEpoches, double alpha)
        {
            double meanSquaredError = double.MaxValue;

            for (int i = 0; i < maxEpoches; i++)
            {
                meanSquaredError = Train(trainData, alpha);

                if (i % 500 == 0)
                {
                    //Console.WriteLine($"MSQ: {meanSquaredError}");
                }
            }

            return meanSquaredError;
        }

        public double Train(IEnumerable<NeuralIO<TOutput>> trainData, double alpha)
        {
            double errorSum = 0;

            foreach (var item in trainData)
            {
                errorSum += Train(item, alpha);
            }

            return 1.0 / (2.0 * trainData.Count()) * errorSum;
        }

        public double Train(NeuralIO<TOutput> trainData, double alpha)
        {
            return Train(trainData.Input, trainData.Output, alpha);
        }

        public double Train(double[] trainingInput, TOutput trainingOutput, double alpha)
        {
            double errorSum = 0;

            //Evaluate(trainingInput);
            //
            //for (int i = 0; i < trainingOutput.Length; i++)
            //{
            //    double outputError = trainingOutput[i] - Layers.Last().Output[i];
            //    errorSum += outputError * outputError;
            //}
            //
            //Layers.Last().CalculateError(trainingOutput);
            //
            //for (int l = Layers.Length - 2; l >= 0; l--)
            //{
            //    Layers[l].CalculateError(Layers[l + 1]);
            //}
            //
            //for (int l = Layers.Length - 1; l >= 0; l--)
            //{
            //    NeuralNetworkLayer layer = Layers[l];
            //    Neuron[] neurons = layer.Neurons;
            //
            //    for (int n = 0; n < neurons.Length; n++)
            //    {
            //        for (int w = 0; w < layer.Input.Length; w++)
            //        {
            //            neurons[n].Weights[w] -= alpha * neurons[n].Error * layer.Input[w];
            //        }
            //    }
            //}

            return errorSum;
        }

        public double[][][] GetWeights()
        {
            return Layers.Select(layer => layer.GetWeights()).ToArray();
        }

        public void SetWeights(double[][][] weights)
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].SetWeights(weights[i]);
            }
        }
    }
}
