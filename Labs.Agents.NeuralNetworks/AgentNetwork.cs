using AI.NeuralNetworks;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentNetwork : Network
    {
        public AgentNetwork() : base(Function.ReLU, AgentNetworkInput.Size, 1, 114, 114, 114)
        {
        }

        public Prediction Predict(AgentNetworkInput input)
        {
            double bestValue = double.MinValue;
            Action2 bestAction = null;

            foreach (Action2 action in Action2.All)
            {
                input.EncodeAction(action);
                double predictedValue = Evaluate(input.Encoded)[0];

                if (predictedValue > bestValue)
                {
                    bestValue = predictedValue;
                    bestAction = action;
                }
            }

            input.EncodeAction(bestAction);
            return new Prediction(bestAction, bestValue, input);
        }
    }
}
