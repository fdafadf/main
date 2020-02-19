using AI.NeuralNetworks.Games;
using AI.TicTacToe;
using Games.TicTacToe;
using SimpleNeuralNetwork;
using System.Linq;

namespace TicTacToe
{
    public class TicTacToeAI : IGameAI<GameState, Player, GameAction>
    {
        public Network Network { get; }

        public TicTacToeAI(Network network)
        {
            Network = network;
        }

        public GameAction GenerateMove(GameState state)
        {
            var actions = TicTacToeGame.Instance.GetAllowedActions(state);
            var states = actions.Select(action => TicTacToeGame.Instance.Play(state, action));
            var features = states.Select(nextState => nextState.ToArray(DataLoader.InputTransform));
            var predictions = features.Select(Network.Evaluate);
            int best = TicTacToeResultProbabilities.FindBest(predictions, state.CurrentPlayer);
            return actions.ElementAt(best);
        }
    }
}
