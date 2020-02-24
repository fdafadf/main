using System.Linq;

namespace AI.NeuralNetworks
{
    public class Gradient
    {
        public Network Network { get; }
        public double[][] Values { get; }
        public double[][][] Accumulation { get; }

        public Gradient(Network network)
        {
            Network = network;
            Values = network.Layers.Select(layer => new double[layer.Neurons.Length]).ToArray();
            Accumulation = network.Layers.Select(layer => layer.Neurons.Select(neurons => new double[neurons.Weights.Length]).ToArray()).ToArray();
        }

        public void Clear()
        {
            for (int i = 0; i < Values.Length; i++)
            {
                double[] layerValues = Values[i];

                for (int j = 0; j < layerValues.Length; j++)
                {
                    layerValues[j] = 0;
                }
            }

            for (int i = 0; i < Accumulation.Length; i++)
            {
                double[][] layerAccumulation = Accumulation[i];

                for (int j = 0; j < layerAccumulation.Length; j++)
                {
                    double[] neuronAccumulation = layerAccumulation[j];

                    for (int k = 0; k < neuronAccumulation.Length; k++)
                    {
                        neuronAccumulation[k] = 0;
                    }
                }
            }
        }

        public void Accumulate(double[] features, double[] prediction, double[] labels)
        {
            AccumulateOutputLayer(features, prediction, labels);

            for (int i = Network.Layers.Length - 2; i >= 0; i--)
            {
                AccumulateHiddenLayer(features, layerIndex: i);
            }
        }

        private void AccumulateOutputLayer(double[] features, double[] output, double[] labels)
        {
            double[] layerGradient = Values[Values.Length - 1];
            double[] layerInput = Network.LastEvaluation[Values.Length - 2];
            IFunction activationFunction = Network.Layers.Last().ActivationFunction;

            for (int i = 0; i < layerGradient.Length; i++)
            {
                double error = -(labels[i] - output[i]);
                layerGradient[i] = error * activationFunction.Derivative(output[i]);

                double[] layerGradient2 = Accumulation[Values.Length - 1][i];

                for (int j = 0; j < layerGradient2.Length - 1; j++)
                {
                    layerGradient2[j] = layerGradient[i] * layerInput[j];
                }

                layerGradient2[layerGradient2.Length - 1] += layerGradient[i];
            }
        }

        private void AccumulateHiddenLayer(double[] features, int layerIndex)
        {
            double[] layerGradient = Values[layerIndex];
            double[] layerInput = layerIndex == 0 ? features : Network.LastEvaluation[layerIndex - 1];
            double[] layerOutput = Network.LastEvaluation[layerIndex];
            double[] nextLayerGradient = Values[layerIndex + 1];
            Neuron[] nextLayerNeurons = Network.Layers[layerIndex + 1].Neurons;
            IFunction activationFunction = Network.Layers[layerIndex].ActivationFunction;

            for (int i = 0; i < layerGradient.Length; i++)
            {
                double error = 0;

                for (int j = 0; j < nextLayerNeurons.Length; j++)
                {
                    error += nextLayerNeurons[j].Weights[i] * nextLayerGradient[j];
                }

                layerGradient[i] = error * activationFunction.Derivative(layerOutput[i]);
                double[] layerAccumulation = Accumulation[layerIndex][i];

                for (int j = 0; j < layerAccumulation.Length - 1; j++)
                {
                    layerAccumulation[j] += layerGradient[i] * layerInput[j];
                }

                layerAccumulation[layerAccumulation.Length - 1] += layerGradient[i];
            }
        }

        //private void AddOutputLayer(double[] features, double[] output, double[] labels)
        //{
        //    double[] layerGradient = Values[Values.Length - 1];
        //    IFunction activationFunction = Network.Layers.Last().ActivationFunction;
        //
        //    for (int i = 0; i < layerGradient.Length; i++)
        //    {
        //        double error = -(labels[i] - output[i]);
        //        layerGradient[i] = error * activationFunction.Derivative(output[i]);
        //    }
        //}

        //private void AddHiddenLayer(double[] features, int layerIndex)
        //{
        //    double[] layerGradient = Values[layerIndex];
        //    double[] layerOutput = Network.LastEvaluation[layerIndex];
        //    double[] nextLayerGradient = Values[layerIndex + 1];
        //    Neuron[] nextLayerNeurons = Network.Layers[layerIndex + 1].Neurons;
        //    IFunction activationFunction = Network.Layers[layerIndex].ActivationFunction;
        //
        //    for (int i = 0; i < layerGradient.Length; i++)
        //    {
        //        double error = 0;
        //
        //        for (int j = 0; j < nextLayerNeurons.Length; j++)
        //        {
        //            error += nextLayerNeurons[j].Weights[i] * nextLayerGradient[j];
        //        }
        //
        //        layerGradient[i] = error * activationFunction.Derivative(layerOutput[i]);
        //    }
        //}
    }
}
