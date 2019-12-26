using Basics.Games;

namespace Basics.AI.NeuralNetworks
{
    public class GameStateNeuralIO<TGameState> : NeuralIO, IGameStateOutput<TGameState>
    {
        public TGameState GameState { get; }

        public GameStateNeuralIO(TGameState gameState, double[] input, double[] output) : base(input, output)
        {
            this.GameState = gameState;
        }
    }
}
