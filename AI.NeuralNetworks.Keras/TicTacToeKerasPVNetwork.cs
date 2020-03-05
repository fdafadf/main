using System;
using System.Collections.Generic;
using System.Linq;
using AI.MonteCarlo;
using AI.NeuralNetworks.TicTacToe;
using Games.TicTacToe;

namespace AI.Keras
{
    class TicTacToeKerasPVNetwork : KerasPVNetwork<GameState, GameAction, PVNetworkOutput<GameAction>>
    {
        public TicTacToeKerasPVNetwork() : base(new KerasModel(KerasModel.Loss_MeanSquared, TicTacToePVNetworkOutput.OutputSize))
        {
        }

        public override PVNetworkOutput<GameAction> Predict(GameState state)
        {
            double[] input = TransformInput(state);
            float[] output = Model.Predict(input);
            return new TicTacToePVNetworkOutput(output.Select(v => (double)v).ToArray());
        }

        public override IEnumerable<PVNetworkOutput<GameAction>> Predict(IEnumerable<GameState> states)
        {
            var inputs = states.Select(TransformInput).ToArray();
            var outputs = Model.Predict(inputs).Select(v => (double)v).ToArray();
            return Enumerable.Range(0, states.Count()).Select(i => {
                double[] output = new double[TicTacToePVNetworkOutput.OutputSize];
                Array.Copy(outputs, i * TicTacToePVNetworkOutput.OutputSize, output, 0, TicTacToePVNetworkOutput.OutputSize);
                return new TicTacToePVNetworkOutput(output);
            });
        }

        protected override double[] TransformInput(GameState state)
        {
            return TicTacToeLabeledStateLoader.InputTransforms.Bipolar(state);
        }

        protected override double[] GetTrainingOutput(PVNetworkBasedMCTreeSearchNode<GameState, GameAction> node, PVNetworkBasedMCTreeSearchNode<GameState, GameAction> finalNode)
        {
            double[] result = new double[TicTacToePVNetworkOutput.OutputSize];

            for (int i = 0; i < TicTacToePVNetworkOutput.OutputSize; i++)
            {
                result[i] = 0;
            }

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    //double pn = node.NetworkOutput.GetProbability(child.Key);
                    //double pm = (double)child.Value.Visited / node.Visited;
                    //result[child.Key.X + child.Key.Y * 3] = (pn + pm) / 2.0;

                    double pn = node.NetworkOutput.GetProbability(child.Key);

                    if (child.Value.Visits == 0)
                    {
                        result[child.Key.X + child.Key.Y * 3] = pn;
                    }
                    else
                    {
                        double pm = child.Value.Value / child.Value.Visits;
                        result[child.Key.X + child.Key.Y * 3] = (pn + pm) / 2.0;
                    }
                }
            }

            Player winner = finalNode.State.GetWinner();
            result[9] = winner == null ? 0.5 : winner == node.State.CurrentPlayer ? 1 : 0;
            return result;
        }
    }
}
