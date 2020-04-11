using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Labs.AI.NeuralNetworks.Ants
{
    public abstract class Training
    {
        public abstract void Episode();
    }

    public class Training<TInput> : Training where TInput : AntNetworkInput
    {
        Environment Environment;
        AntNetwork<TInput> Model;

        public Training(Environment environment, AntNetwork<TInput> model)
        {
            Environment = environment;
            Model = model;
        }

        public override void Episode()
        {
            Agent agent = Environment.Agents[0];
            double epsilon = 0.2;
            TInput input;
            AgentAction action;
            AgentState state = agent.State;

            if (Environment.Random.NextDouble() < epsilon)
            {
                action = Environment.Random.Next(Environment.Actions);
                input = Model.CreateInput(state, action);
            }
            else
            {
                Prediction<TInput> prediction = Model.Predict(state);
                input = prediction.Input;
                action = prediction.BestAction;
            }

            double reward = Environment.DoAction(agent, action);
            Model.History.Add(state, agent.State, action, reward, input);

            if (Model.History.Items.Count > 128)
            {
                Fit(0.99, Model.History.Items.Subset(128, Environment.Random), Environment.Random);
            }
        }

        private void Fit(double gamma, IEnumerable<History<TInput>.Item> batch, Random random)
        {
            var optimizer = new SGDMomentum(Model, 0.001, 0.04);
            var trainer = new Trainer(optimizer, random);
            var nextQ = batch.Select(item => new Projection(item.Input.Encoded, new double[] { item.Reward + gamma * Model.Predict(item.NextState).BestValue })).ToArray();
            trainer.Train(nextQ, 10, 8);
        }
    }
}
