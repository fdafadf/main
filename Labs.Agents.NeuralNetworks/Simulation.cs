using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Interaction = Labs.Agents.MarkovAgentInteraction<Labs.Agents.NeuralNetworks.Agent, Labs.Agents.Action2, Labs.Agents.InteractionResult>;

namespace Labs.Agents.NeuralNetworks
{
    public class Simulation : Simulation<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState>
    {
        List<Interaction> EnvironmentIteractions = new List<Interaction>();
        public List<HistoryItem> History = new List<HistoryItem>();
        AgentNetwork Network = new AgentNetwork();

        public Simulation(MarkovEnvironment2<Agent, AgentState> environment, int numberOfAgents) : base(environment)
        {
            CreateAgents(numberOfAgents);
        }

        public override void Step()
        {
            double epsilon = 0.2;

            foreach (var interaction in EnvironmentIteractions)
            {
                if (Environment.Random.NextDouble() < epsilon)
                {
                    interaction.Action = Environment.Random.Next(Action2.All);
                    interaction.Agent.LastInput = new AgentNetworkInput(interaction.Agent.State);
                    interaction.Agent.LastInput.EncodeAction(interaction.Action);
                }
                else
                {
                    var input = new AgentNetworkInput(interaction.Agent.State);
                    var prediction = Network.Predict(input);
                    interaction.Agent.LastInput = prediction.Input;
                    interaction.Action = prediction.Action;
                }
            }

            Environment.Apply(EnvironmentIteractions);

            foreach (var interaction in EnvironmentIteractions)
            {
                if (interaction.Result != InteractionResult.Ignored)
                {
                    var encodedInput = interaction.Agent.LastInput;
                    var encodedState = new AgentNetworkInput(interaction.Agent.State);
                    var historyItem = new HistoryItem(encodedInput, encodedState, interaction.Reward);
                    History.Add(historyItem);
                }
            }

            int numberOfRemovedAgents = EnvironmentIteractions.RemoveAll(interaction => interaction.Result == InteractionResult.Collision);
            CreateAgents(numberOfRemovedAgents);

            if (History.Count > 256)
            {
                Fit(0.99, History.Subset(64, Environment.Random), Environment.Random);
            }
        }

        private void CreateAgents(int numberOfAgents)
        {
            for (int i = 0; i < numberOfAgents; i++)
            {
                CreateAgent();
            }
        }

        private void CreateAgent()
        {
            var interaction = Environment.AddAgent(new Agent(), Environment.GetRandomUnusedPosition());
            EnvironmentIteractions.Add(interaction);
        }

        private void Fit(double gamma, IEnumerable<HistoryItem> batch, Random random)
        {
            var optimizer = new SGDMomentum(Network, 0.001, 0.04);
            var trainer = new Trainer(optimizer, random);
            var nextQ = batch.Select(item => new Projection(item.Input.Encoded, new double[] { item.Reward + gamma * Network.Predict(item.State).Value })).ToArray();
            trainer.Train(nextQ, 2, 8);
        }
    }
}
