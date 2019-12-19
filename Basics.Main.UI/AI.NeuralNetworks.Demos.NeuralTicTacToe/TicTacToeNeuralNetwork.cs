namespace Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe
{
    public class TicTacToeNeuralNetwork
    {
        public static NeuralNetwork Create()
        {
            NeuralNetwork network = new NeuralNetwork();
            network.AddLayer(9, 9);
            network.AddLayer(9, 9);
            network.AddLayer(9, 9);
            return network;
        }
    }
}
