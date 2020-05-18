using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralAgentDriver : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<NeuralAgent>, NeuralAgent>, NeuralAgent>
    {
        public Random Random;
        public AgentNetwork Network;
        public bool TrainingMode => TrainingConfiguration != null;
        AgentNetworkTrainingConfiguration TrainingConfiguration;
        public List<MarkovHistoryItem> MarkovHistory = new List<MarkovHistoryItem>();

        public NeuralAgentDriver(AgentNetwork network, AgentNetworkTrainingConfiguration trainingConfiguration, int seed) : base(new NeuralAgentFactory())
        {
            Random = new Random(seed);
            Network = network;
            TrainingConfiguration = trainingConfiguration;
        }

        public override void OnInteractionCompleted()
        {
            foreach (var agent in Agents)
            {
                if (agent.Interaction.ActionResult != InteractionResult.Suspended)
                {
                    var encodedState = Network.CreateInput(agent, CardinalMovement.Nothing);
                    double reward;

                    if (agent.IsGoalReached())
                    {
                        reward = 1;
                    }
                    else
                    {
                        if (agent.Interaction.ActionResult == InteractionResult.Collision)
                        {
                            reward = -2;
                        }
                        else if (agent.Interaction.Action == CardinalMovement.Nothing)
                        {
                            reward = -0.1;
                        }
                        else
                        {
                            double distanceBeforeInteraction = agent.Interaction.From.Distance(agent.Goal.Position);
                            double distanceAfterInteraction = agent.Interaction.To.Distance(agent.Goal.Position);
                            reward = distanceBeforeInteraction - distanceAfterInteraction;
                        }
                    }

                    var historyItem = new MarkovHistoryItem(agent.NetworkLastInput, encodedState, reward);
                    MarkovHistory.Add(historyItem);
                }
            }
        }

        public override void OnIterationCompleted()
        {
            if (TrainingMode)
            {
                if (MarkovHistory.Count > TrainingConfiguration.FirstTrainingIteration)
                {
                    Fit(MarkovHistory.Subset(TrainingConfiguration.HistorySubsetSize, Random), Random);
                }
            }
        }

        public override void OnIterationStarted()
        {
            if (TrainingMode)
            {
                foreach (var agent in Agents)
                {
                    if (Random.NextDouble() < TrainingConfiguration.Epsilon)
                    {
                        agent.Interaction.Action = Random.Next(CardinalMovement.All);
                        agent.NetworkLastInput = Network.CreateInput(agent, agent.Interaction.Action);
                    }
                    else
                    {
                        var prediction = Network.Predict(agent);
                        agent.NetworkLastInput = prediction.Input;
                        agent.Interaction.Action = prediction.Action;
                    }
                }
            }
            else
            {
                foreach (var agent in Agents)
                {
                    agent.Interaction.Action = Network.Predict(agent).Action;
                }
            }
        }

        private void Fit(IEnumerable<MarkovHistoryItem> batch, Random random)
        {
            var optimizer = new SGDMomentum(Network.Network, TrainingConfiguration.LearningRate, TrainingConfiguration.Momentum);
            var trainer = new Trainer(optimizer, random);
            var nextQ = batch.Select(item => new Projection(item.Input, new double[] { item.Reward + TrainingConfiguration.Gamma * Network.Predict(item.State).Value })).ToArray();
            trainer.Train(nextQ, TrainingConfiguration.EpochesPerIteration, TrainingConfiguration.BatchSize);
        }
    }
}
