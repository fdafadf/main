using AI.MonteCarlo;
using AI.TicTacToe;
using Games.TicTacToe;
using Games.Utilities;

namespace AI.NeuralNetworks.TicTacToe
{
    public class TicTacToePVNetworkOutput : PVNetworkOutput<GameAction>
    {
        public static TicTacToePVNetworkOutput Create(GameTreeEvaluationNode<GameState, GameAction, TicTacToeResultProbabilities> evaluation)
        {
            double[] values = new double[OutputSize];
            TicTacToeActionProbabilities.CalculateProbabilities(values, evaluation);
            values[9] = TicTacToeActionProbabilities.CalculateProbability(evaluation);
            return new TicTacToePVNetworkOutput(values);
        }

        public const int OutputSize = 10;
        public override double Value => Raw[9];

        public TicTacToePVNetworkOutput(double[] raw) : base(raw)
        {
        }

        public override double GetProbability(GameAction action)
        {
            return Raw[action.X + action.Y * 3];
        }
    }

    public class TicTacToePVNetworkOutputCollection
    {
        public TicTacToePVNetworkOutputCollection(float[] raw)
        {

        }
    }
}
