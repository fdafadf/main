namespace Basics.AI.NeuralNetworks
{
    public class GameStateNeuralIO<TState> : NeuralIO
    {
        public readonly TState GameState;

        public GameStateNeuralIO(TState gameState, double[] input, double output) : base(input, output)
        {
            this.GameState = gameState;
        }
    }
}
