﻿using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Labs.Agents.NeuralNetworks
{
    public class NeuralSimulationPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<NeuralAgent>, NeuralAgent>, NeuralAgent>
    {
        public Random Random;
        public AgentNetwork Network;
        public bool TrainingMode => TrainingConfiguration != null;
        AgentNetworkTrainingConfiguration TrainingConfiguration;
        public AgentNetworkFile NetworkFile;
        public List<MarkovHistoryItem> MarkovHistory = new List<MarkovHistoryItem>();
        public double TotalReward;
        public int[] Predictions = new int[CardinalMovement.All.Length];
        public int Collisions;
        List<double> TotalRewardSerie = new List<double>();

        public NeuralSimulationPlugin(AgentNetworkFile networkFile, AgentNetworkTrainingConfiguration trainingConfiguration, int seed) : base(new NeuralAgentFactory())
        {
            Random = new Random(seed);
            NetworkFile = networkFile;
            Network = NetworkFile.Load();
            TrainingConfiguration = trainingConfiguration;
        }

        public override void OnSimulationStarted(ISimulation simulation)
        {
            simulation.Results.Series.Add("Total Reward", TotalRewardSerie);
        }

        public override void OnInteractionCompleted(IEnumerable<NeuralAgent> agents)
        {
            foreach (var agent in agents)
            {
                if (agent.Interaction.ActionResult != InteractionResult.Suspended)
                {
                    var encodedState = Network.CreateInput(agent, CardinalMovement.Nothing);
                    double reward;

                    if (agent.IsGoalReached())
                    {
                        reward = 2;
                    }
                    else
                    {
                        if (agent.Interaction.ActionResult == InteractionResult.Collision)
                        {
                            Collisions++;
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

                    TotalReward += reward;
                    TotalRewardSerie.Add(TotalReward);
                    var historyItem = new MarkovHistoryItem(agent.NetworkLastInput, encodedState, reward);
                    MarkovHistory.Add(historyItem);
                }
            }
        }

        public override void OnIterationCompleted(IEnumerable<NeuralAgent> agents)
        {
            if (TrainingMode)
            {
                if (MarkovHistory.Count > TrainingConfiguration.FirstTrainingIteration)
                {
                    Network.Fit(MarkovHistory.Subset(TrainingConfiguration.HistorySubsetSize, Random), TrainingConfiguration, Random);
                }
            }
        }

        public override void OnIterationStarted(IEnumerable<NeuralAgent> agents)
        {
            var undestroyedAgents = agents.Where(agent => agent.Fitness.IsDestroyed == false);

            if (TrainingMode)
            {
                foreach (NeuralAgent agent in undestroyedAgents)
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
                foreach (NeuralAgent agent in undestroyedAgents)
                {
                    var prediction = Network.Predict(agent);
                    agent.Interaction.Action = prediction.Action;
                    agent.NetworkLastInput = prediction.Input;
                    Predictions[prediction.Action.Index]++;
                }
            }
        }

        public override void OnSimulationCompleted()
        {
            if (TrainingMode)
            {
                if (MessageBox.Show("Do you want to update neural network?", "Training", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NetworkFile.Save(Network);
                }
            }
        }

        public override void OnSimulationPaused()
        {
            int index = 0;

            foreach (var historyItem in MarkovHistory.Take(10))
            {
                Console.WriteLine($"{index++}: {Network.Predict(historyItem.Input)}");
            }
        }
    }
}
