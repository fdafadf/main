using AI.NeuralNetworks;
using System;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AntNetwork : Network
    {
        public static readonly int NetworkInputSize = Environment.EncodedStateSize + AgentAction.EncodedSize;

        public AntNetwork() : base(Function.ReLU, NetworkInputSize, 1, 8, 8)
        {
        }

        public Prediction Predict(AgentState state)
        {
            double[] input = new double[NetworkInputSize];
            double bestQ = double.MinValue;
            AgentAction bestAction = null;
            Array.Copy(state.Encoded, input, state.Encoded.Length);

            for (int i = 0; i < Environment.Actions.Length; i++)
            {
                AgentAction action = Environment.Actions[i];
                Array.Copy(action.Encoded, 0, input, state.Encoded.Length, action.Encoded.Length);
                double predictQ = Evaluate(input)[0];

                if (predictQ > bestQ)
                {
                    bestQ = predictQ;
                    bestAction = action;
                }
            }

            Array.Copy(bestAction.Encoded, 0, input, state.Encoded.Length, bestAction.Encoded.Length);
            return new Prediction(bestAction, bestQ, input);
        }

        public double[] CreateInput(AgentState state, AgentAction action)
        {
            double[] input = new double[NetworkInputSize];
            Array.Copy(state.Encoded, input, state.Encoded.Length);
            Array.Copy(action.Encoded, 0, input, state.Encoded.Length, action.Encoded.Length);
            return input;
        }
    }
}
