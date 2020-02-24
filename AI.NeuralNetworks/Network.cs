using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class Network
    {
        public Layer[] Layers { get; }
        public double[][] LastEvaluation { get; private set; }

        public Network(IEnumerable<Layer> layers)
        {
            Layers = layers.ToArray();
            InitializeLastEvaluation();
        }

        public Network(IFunction activationFunction, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        {
            List<Layer> layers = new List<Layer>();
            ILayerInitializer initializer = new He(0);

            for (int i = 0; i < hiddenLayerSizes.Length; i++)
            {
                int layerInputSize = i == 0 ? inputSize : hiddenLayerSizes[i - 1];
                var layerNeurons = Enumerable.Range(0, hiddenLayerSizes[i]).Select(k => new Neuron(layerInputSize)).ToArray();
                layers.Add(new Layer(layerNeurons, activationFunction, initializer));
            }

            var lastLayerNeurons = Enumerable.Range(0, outputSize).Select(k => new Neuron(hiddenLayerSizes.Last())).ToArray();
            layers.Add(new Layer(lastLayerNeurons, activationFunction, initializer));
            Layers = layers.ToArray();
            InitializeLastEvaluation();
        }

        private void InitializeLastEvaluation()
        {
            LastEvaluation = Layers.Select(layer => new double[layer.Neurons.Length]).ToArray();
        }

        public double[] Evaluate(double[] input)
        {
            double[] layerInput = input;

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Evaluate(layerInput, LastEvaluation[i]);
                layerInput = LastEvaluation[i];
            }

            return layerInput;
        }

        public void LoadWeights(string path)
        {

        }

        public void SaveWeights(string path)
        {

        }

        public override string ToString()
        {
            return string.Join(",", Layers.Select(layer => layer.Neurons.Length));
        }
    }
}
