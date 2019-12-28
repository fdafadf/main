using System;
using System.Collections.Generic;
using System.Linq;

namespace Basics.AI.NeuralNetworks
{
    public class NeuralNetwork
    {
        List<NeuralNetworkLayer> Layers = new List<NeuralNetworkLayer>();
        List<double[]> LayersOutput = new List<double[]>();
        List<double[]> LayersOutputDerivatives = new List<double[]>();
        List<double[]> LayersErrors = new List<double[]>();

        public void AddLayer(int inputSize, int outputSize, Random weightRandomizer)
        {
            NeuralNetworkLayer layer = new NeuralNetworkLayer(inputSize, outputSize, weightRandomizer);
            LayersOutput.Add(new double[outputSize]);
            LayersOutputDerivatives.Add(new double[outputSize]);
            LayersErrors.Add(new double[outputSize]);
            Layers.Add(layer);
        }

        public double[] Evaluate(double[] input)
        {
            double[] layerInput = input;

            for (int i = 0; i < Layers.Count; i++)
            {
                Layers[i].Evaluate(layerInput, LayersOutput[i], LayersOutputDerivatives[i]);
                layerInput = LayersOutput[i];
            }

            return LayersOutput.Last();
        }

        public void Train(double[] input, double[] desiredOutput, double alpha)
        {
            double[] output = Evaluate(input);
            double[] layerErrors = LayersErrors.Last();
            NeuralNetworkLayer lastLayer = Layers.Last();
            double[] lastLayerOutputDerivatives = LayersOutputDerivatives.Last();
            double[] layerInput;

            for (int k = 0; k < lastLayer.Neurons.Length; k++)
            {
                layerErrors[k] = -(desiredOutput[k] - output[k]) * lastLayerOutputDerivatives[k];
            }

            for (int l = Layers.Count - 2; l >= 0; l--)
            {
                NeuralNetworkLayer layer = Layers[l];
                NeuralNetworkLayer nextLayer = Layers[l + 1];

                for (int n = 0; n < layer.Neurons.Length; n++)
                {
                    INeuron neuron = layer.Neurons[n];

                    for (int w = 0; w < neuron.Weights.Length; w++)
                    {

                    }
                }

                double[] nextLayerErrors = LayersErrors[l];
                layerInput = l - 1 == 0 ? input : LayersOutput[l - 2];
                layerErrors = LayersErrors[l - 1];

                for (int w = 0; w < layerErrors.Length; w++)
                {
                    layerErrors[w] = 0;

                    for (int n = 0; n < nextLayerErrors.Length; n++)
                    {
                        layerErrors[w] += nextLayer.Neurons[n].Weights[w] * nextLayerErrors[n];
                    }

                    layerErrors[w] *= layerInput[w];
                }
            }

            layerInput = input;

            for (int k = 0; k < Layers.Count; k++)
            {
                NeuralNetworkLayer layer = Layers[k];
                double[] layerOutputDerivatives = LayersOutputDerivatives[k];

                for (int n = 0; n < layer.Neurons.Length; n++)
                {
                    double[] weights = layer.Neurons[n].Weights;
                    double derivative = layerOutputDerivatives[n];

                    for (int w = 0; w < weights.Length; w++)
                    {
                        weights[w] += alpha * derivative * layerInput[w];
                    }
                }

                layerInput = LayersOutput[k];
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Layers.Select(l => l.ToString()));
        }
    }

    class NeuralNetworkEvaluator
    {

    }
}
