using Games.TicTacToe;
using Games.Utilities;

namespace AI.TicTacToe
{
    public class TicTacToeActionProbabilities
    {
        public static void CalculateProbabilities(double[] probabilities, GameTreeEvaluationNode<GameState, GameAction, TicTacToeValue> node)
        {
            for (int i = 0; i < probabilities.Length; i++)
            {
                probabilities[i] = 0;
            }

            double crossResult;
            double noughtResult;

            if (node.GameState.CurrentPlayer.IsCross)
            {
                crossResult = 1;
                noughtResult = 0;
            }
            else
            {
                crossResult = 0;
                noughtResult = 1;
            }

            foreach (var child in node.Children)
            {
                double p = 0.5;

                switch (child.Value.Evaluation.Max)
                {
                    case FieldState.Cross:
                        p = crossResult;
                        break;
                    case FieldState.Nought:
                        p = noughtResult;
                        break;
                }

                probabilities[child.Key.X + child.Key.Y * 3] = p;
            }
        }

        public static double CalculateProbability(GameTreeEvaluationNode<GameState, GameAction, TicTacToeValue> node)
        {
            double crossResult;
            double noughtResult;

            if (node.GameState.CurrentPlayer.IsCross)
            {
                crossResult = 1;
                noughtResult = 0;
            }
            else
            {
                crossResult = 0;
                noughtResult = 1;
            }

            switch (node.Evaluation.Max)
            {
                case FieldState.Cross:
                    return crossResult;
                case FieldState.Nought:
                    return noughtResult;
                default:
                    return 0;
            }
        }

        public readonly double[] Probabilities;
        //public readonly TicTacToeResultProbabilities Result;
        //public Func<GameAction, int> OutputActionMap { get; }

        //public TicTacToeActionProbabilities(uint size, TicTacToeResultProbabilities result)
        //{
        //    Probabilities = new double[size];
        //    Result = result;
        //}

        public TicTacToeActionProbabilities(double[] probabilities)
        {
            Probabilities = probabilities;
        }

        public TicTacToeActionProbabilities(GameTreeEvaluationNode<GameState, GameAction, TicTacToeValue> evaluation)
        {
            Probabilities = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            CalculateProbabilities(Probabilities, evaluation);
        }

        public double this[GameAction action]
        {
            get
            {
                return Probabilities[action.X + action.Y * 3];
            }
            set
            {
                Probabilities[action.X + action.Y * 3] = value;
            }
        }
    }
}
