using Games;
using Games.Utilities;

namespace AI.NeuralNetworks.Games
{
    public class GameStateNeuralIO<TGameState, TOutput> : NeuralIO<TOutput>, IGameStateOutput<TGameState, TOutput>
    {
        public TGameState GameState { get; }

        public GameStateNeuralIO(TGameState gameState, double[] input, TOutput output) : base(input, output)
        {
            this.GameState = gameState;
        }
    }
}
