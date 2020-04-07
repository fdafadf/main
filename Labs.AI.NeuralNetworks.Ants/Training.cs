using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class Training
    {
        Environment Environment;
        AntNetwork Model;
        History History;

        public Training(Environment environment)
        {
            Environment = environment;
            Model = new AntNetwork();
            History = new History();
        }

        public void Episode()
        {
            Agent item = Environment.Agents[0];
            AgentState state = item.State;
            double epsilon = 0.2;
            double[] input;

            if (Environment.Random.NextDouble() < epsilon)
            {
                item.Action = Environment.Random.Next(Environment.Actions);
                input = Model.CreateInput(state, item.Action);
            }
            else
            {
                var prediction = Model.Predict(state);
                item.Action = prediction.BestAction;
                input = prediction.Input;
            }

            Environment.DoAction(item);
            History.Add(state, item, input);

            if (History.Items.Count > 128)
            {
                Fit(0.99, History.Items.Subset(128, Environment.Random), Environment.Random);
            }
        }

        private void Fit(double gamma, IEnumerable<History.Item> batch, Random random)
        {
            var optimizer = new SGDMomentum(Model, 0.001, 0.04);
            var trainer = new Trainer(optimizer, random);
            var nextQ = batch.Select(item => new Projection(item.Input, new double[] { item.Reward + gamma * Model.Predict(item.NextState).BestValue })).ToArray();
            trainer.Train(nextQ, 10, 8);
        }
    }
}
