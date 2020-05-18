using System.Linq;

namespace AI.NeuralNetworks
{
    public class NetworkConfiguration
    {
        public FunctionName ActivationFunction { get; set; }
        public int InputSize { get; set; }
        public int OutputSize { get; set; }
        public int[] HiddenLayerSizes { get; set; }

        public NetworkConfiguration(FunctionName activationFunction, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        {
            ActivationFunction = activationFunction;
            InputSize = inputSize;
            OutputSize = outputSize;
            HiddenLayerSizes = hiddenLayerSizes;
        }
    }

    public class NetworkDescriptor
    {
        public FunctionName ActivationFunction { get; set; }
        public double[][][] Weights { get; set; }

        public NetworkDescriptor(FunctionName activationFunction, Layer[] layers)
        {
            ActivationFunction = activationFunction;
            Weights = layers.Select(layer => layer.Neurons.Select(neuron => neuron.Weights).ToArray()).ToArray();
        }
    }
}
