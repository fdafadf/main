using AI;
using AI.NeuralNetworks;
using AI.NeuralNetworks.TicTacToe;
using AI.TicTacToe;
using Games.TicTacToe;
using System.Linq;

namespace Demos.TicTacToe
{
    public class TicTacToeAI : IActionGenerator<GameState, GameAction, Player>
    {
        public TicTacToeValueNetwork Network { get; }

        public TicTacToeAI(TicTacToeValueNetwork network)
        {
            Network = network;
        }

        public GameAction GenerateAction(GameState state)
        {
            var actions = TicTacToeGame.Instance.GetAllowedActions(state);
            var states = actions.Select(action => TicTacToeGame.Instance.Play(state, action));
            var predictions = states.Select(Network.Evaluate).Select(prediction => prediction.Probabilities);
            int best = TicTacToeValue.FindBest(predictions, state.CurrentPlayer);
            return actions.ElementAt(best);
        }
    }
}
