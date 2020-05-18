//#define MOMENTUM

namespace AI.NeuralNetworks
{
    // Stochastic Gradient Descent
    public class SGD : Optimizer
    {
        public Gradient gradient;

        public SGD(Network network, double learningRate) : base(network, learningRate)
        {
            gradient = new Gradient(network);
        }

        public override double[] Evaluate(double[] features, double[] labels)
        {
            double[] output = Network.Evaluate(features);
            gradient.Accumulate(features, output, labels);
            return output;
        }

        public override void Update(int batchSize)
        {
            for (int layerIndex = 0; layerIndex < gradient.Values.Length; layerIndex++)
            {
                UpdateLayerWeights(layerIndex, batchSize);
            }

            gradient.Clear();
        }

        protected virtual void UpdateLayerWeights(int layerIndex, int batchSize)
        {
            Layer layer = Network.Layers[layerIndex];
            Neuron[] layerNeurons = layer.Neurons;

            for (int neuronIndex = 0; neuronIndex < layerNeurons.Length; neuronIndex++)
            {
                double[] weights = layerNeurons[neuronIndex].Weights;
                double[] layerGradient = gradient.Accumulation[layerIndex][neuronIndex];

                for (int i = 0; i < weights.Length - 1; i++)
                {
                    weights[i] -= LearningRate * layerGradient[i];
                }

                weights[weights.Length - 1] -= LearningRate * layerGradient[weights.Length - 1];
            }
        }

        public override string ToString()
        {
            return $"[{Network}] {Network.Layers[0].ActivationFunction} SGD({LearningRate:f4})";
        }
    }
}
