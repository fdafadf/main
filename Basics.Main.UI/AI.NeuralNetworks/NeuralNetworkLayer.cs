namespace Basics.AI.NeuralNetworks
{
    public class NeuralNetworkLayer
    {
        INeuron[] Neurons;

        public NeuralNetworkLayer(int inputSize, int layerSize)
        {
            Neurons = new INeuron[layerSize];

            for (int i = 0; i < layerSize; i++)
            {
                Neurons[i] = new Perceptron(inputSize, null, -50, 50);
            }
        }

        public void Evaluate(double[] input, double[] output)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                output[i] = Neurons[i].Output(input);
            }
        }
    }
}
