using AI.NeuralNetworks;

namespace Labs.AI.NeuralNetworks.Ants
{
    public abstract class AntNetwork<TInput> : Network where TInput : AntNetworkInput
    {
        public History<TInput> History;

        public AntNetwork(int inputSize) : this(inputSize, 8, 8)
        {
        }

        public AntNetwork(int inputSize, params int[] hiddenLayerSizes) : base(Function.ReLU, inputSize, 1, hiddenLayerSizes)
        {
            History = new History<TInput>();
        }

        public abstract TInput CreateInput(AgentState state, AgentAction action);
        public abstract Prediction<TInput> Predict(AgentState state);

        public Prediction<TInput> Predict(TInput input)
        {
            double bestQ = double.MinValue;
            AgentAction bestAction = null;

            foreach (AgentAction action in Environment.Actions)
            {
                double predictQ = Evaluate(input.EncodeAction(action).Encoded)[0];

                if (predictQ > bestQ)
                {
                    bestQ = predictQ;
                    bestAction = action;
                }
            }

            input.EncodeAction(bestAction);
            return new Prediction<TInput>(bestAction, bestQ, input);
        }
    }
}
