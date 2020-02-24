using AI;
using AI.NeuralNetworks;
using AI.TicTacToe;
using Games.TicTacToe;
using System.Linq;

namespace Demos.TicTacToe
{
    public class TicTacToeAI : IActionGenerator<GameState, Player, GameAction>
    {
        public Network Network { get; }

        public TicTacToeAI(Network network)
        {
            Network = network;
        }

        public GameAction GenerateAction(GameState state)
        {
            var actions = TicTacToeGame.Instance.GetAllowedActions(state);
            var states = actions.Select(action => TicTacToeGame.Instance.Play(state, action));
            var features = states.Select(DataLoader.InputTransform);
            var predictions = features.Select(Network.Evaluate);
            int best = TicTacToeResultProbabilities.FindBest(predictions, state.CurrentPlayer);
            return actions.ElementAt(best);
        }
    }
}
