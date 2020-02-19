using Games;
using System.Collections.Generic;

namespace AI.MonteCarlo
{
    public class PVNetworkMock<TState, TAction> : IPVNetwork<TState, TAction>
        where TState : IPeriodState
    {
        int outputSize;

        public PVNetworkMock(int outputSize)
        {
            this.outputSize = outputSize;
        }

        public PVNetworkOutput<TAction> Predict(TState gameState)
        {
            return new PVNetworkMockOutput<TAction>();
            //return new PVNetworkOutputMock<TState, TAction>(gameState, networkOutput);
            //double[] networkOutput = new double[outputSize];
            //
            //for (int i = 0; i < outputSize; i++)
            //{
            //    networkOutput[i] = 0.5;
            //}
            //Dictionary<TAction, double> probabilities = new Dictionary<TAction, double>();
            //
            //foreach (TAction allowedAction in actions)
            //{
            //    // Game.Play(gameState, allowedAction)
            //    probabilities.Add(allowedAction, 0.5);
            //}
        }

        public IEnumerable<PVNetworkOutput<TAction>> Predict(IEnumerable<TState> states)
        {
            throw new System.NotImplementedException();
        }
    }
}
