using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Interaction = Labs.Agents.MarkovAgentInteraction<Labs.Agents.NeuralNetworks.Agent, Labs.Agents.Action2, Labs.Agents.InteractionResult>;

namespace Labs.Agents.NeuralNetworks
{
    public class Simulation //: Simulation<MarkovEnvironment2<Agent, AgentState>, Agent, AgentState>
    {
        public MarkovEnvironment2<Agent, AgentState> MarkovEnvironment { get; }
        List<Interaction> EnvironmentIteractions = new List<Interaction>();
        public List<HistoryItem> History = new List<HistoryItem>();
        AgentNetwork Network = new AgentNetwork();
        bool Training;
        public int StepNumber = 0;

        public Simulation(MarkovEnvironment2<Agent, AgentState> environment, int numberOfAgents) //: base(environment)
        {
            MarkovEnvironment = environment;
            AddAgents(numberOfAgents);
        }

        public Simulation(MarkovEnvironment2<Agent, AgentState> environment, bool[,] agentsMap) //: base(environment)
        {
            MarkovEnvironment = environment;
            AddAgents(agentsMap);
        }

        public void Step()
        {
            StepNumber++;

            if (Training)
            {

            }
            else
            {
                foreach (var interaction in EnvironmentIteractions)
                {
                    interaction.Action = Network.Predict(interaction.Agent.State).Action;
                }

                MarkovEnvironment.Apply(EnvironmentIteractions);
                int numberOfRemovedAgents = EnvironmentIteractions.RemoveAll(interaction => interaction.Result == InteractionResult.Collision);
                AddAgents(numberOfRemovedAgents);
            }
            /*
            double epsilon = 0.2;

            foreach (var interaction in EnvironmentIteractions)
            {
                if (MarkovEnvironment.Random.NextDouble() < epsilon)
                {
                    interaction.Action = MarkovEnvironment.Random.Next(Action2.All);
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

            MarkovEnvironment.Apply(EnvironmentIteractions);

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
                Fit(0.99, History.Subset(64, MarkovEnvironment.Random), MarkovEnvironment.Random);
            }
            */
        }

        private void AddAgents(int numberOfAgents)
        {
            for (int i = 0; i < numberOfAgents; i++)
            {
                AddAgent();
            }
        }

        private void AddAgents(bool[,] agentsMap)
        {
            for (int x = 0; x < agentsMap.GetLength(0); x++)
            {
                for (int y = 0; y < agentsMap.GetLength(1); y++)
                {
                    if (agentsMap[x, y])
                    {
                        AddAgent(x, y);
                    }
                }
            }
        }

        private void AddAgent() => AddAgent(MarkovEnvironment.GetRandomUnusedPosition());
        private void AddAgent(Point p) => EnvironmentIteractions.Add(MarkovEnvironment.AddAgent(new Agent(), p));
        private void AddAgent(int x, int y) => AddAgent(new Point(x, y));

        private void Fit(double gamma, IEnumerable<HistoryItem> batch, Random random)
        {
            var optimizer = new SGDMomentum(Network, 0.001, 0.04);
            var trainer = new Trainer(optimizer, random);
            var nextQ = batch.Select(item => new Projection(item.Input.Encoded, new double[] { item.Reward + gamma * Network.Predict(item.State).Value })).ToArray();
            trainer.Train(nextQ, 2, 8);
        }
    }
}
