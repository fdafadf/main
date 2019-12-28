using Games;
using Games.Utilities;

namespace AI.NeuralNetworks.Games
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
