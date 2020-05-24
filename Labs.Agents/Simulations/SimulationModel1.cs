﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationModel1<TPlugin, TAgent> : ISimulation
        where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> 
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
    {
        public Random Random = new Random(0);
        public DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent> Space;
        public RandomRenewableGoals<TAgent> Goals;
        public SimulationResults Results;
        public List<TAgent> Agents = new List<TAgent>();
        int Iteration;
        int Iterations;
        internal TPlugin Plugin;
        AgentDestructionModel AgentDestructionModel;
        List<double> ConsumedTime = new List<double>();
        Stopwatch Stopwatch = new Stopwatch();

        public SimulationModel1(ISpaceTemplateFactory spaceDefinition, TPlugin plugin, string pluginName, int iterations, AgentsCollisionModel agentsCollisionModel, AgentDestructionModel agentDestructionModel)
        {
            var spaceTemplate = spaceDefinition.CreateSpaceTemplate();
            var cardinalSpace = new CardinalMovementSpace<TAgent>(spaceTemplate, agentsCollisionModel);
            Space = new DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>(cardinalSpace);
            Agents.AddRange(spaceTemplate.AgentMap.SelectTrue((x, y) => plugin.CreateAgent(Space, x, y)));
            Goals = new RandomRenewableGoals<TAgent>(Space.InteractiveSpace, new Random(0));
            Goals.Initialize(Agents);
            Results = new SimulationResults(pluginName, spaceDefinition.Name);
            Results.Series.Add("Reached Goals", Goals.ReachedGoals);
            Results.Series.Add("Collisions", Space.Collisions);
            Results.Series.Add("ConsumedTime", ConsumedTime);
            Plugin = plugin;
            Iterations = iterations;
            AgentDestructionModel = agentDestructionModel;
        }

        public bool Iterate()
        {
            Iteration++;
            Stopwatch.Start();

            Plugin.OnIterationStarted();
            Space.Interact(Agents);
            Plugin.OnInteractionCompleted();
            Goals.Update(Agents);
            int destroyed = Agents.Count(agent => agent.Interaction.ActionResult == InteractionResult.Collision);

            for (int i = 0; i < destroyed; i++)
            {
                if (AgentDestructionModel.CreateNew)
                {
                    var field = Random.GetUnusedField(Space.InteractiveSpace);
                    Agents.Add(Plugin.CreateAgent(Space, field.X, field.Y));
                }
            }

            if (AgentDestructionModel.RemoveDestoryed)
            {
                Agents.RemoveAll(agent => agent.Fitness.IsDestroyed);
            }

            Plugin.OnIterationCompleted();
            Stopwatch.Stop();
            ConsumedTime.Add(Stopwatch.Elapsed.TotalSeconds);
            return Iteration < Iterations;
        }

        public void Complete()
        {
            Plugin.OnSimulationCompleted();
        }

        public void Pause()
        {
            Plugin.OnSimulationPaused();
        }

        public override string ToString()
        {
            return $"Iteration: {Iteration}/{Iterations} Reached Goals: {Goals.TotalReachedGoals} Collisions: {Space.TotalCollisions}";
        }
    }

    public class RandomRenewableGoals<TAgent> where TAgent : IGoalAgent, IAnchoredAgent<TAgent>
    {
        public int TotalReachedGoals { get; private set; }
        public List<double> ReachedGoals = new List<double>();
        ISpace Space;
        Random Random;

        public RandomRenewableGoals(ISpace space, Random random)
        {
            Space = space;
            Random = random;
        }

        public void Initialize(IEnumerable<TAgent> agents)
        {
            foreach (var agent in agents)
            {
                if (agent.Goal.Position == Point.Empty)
                {
                    agent.Goal.Position = Random.GetUnusedField(Space);
                }
                else if (agent.IsGoalReached())
                {
                    TotalReachedGoals++;
                    agent.Goal.Position = Random.GetUnusedField(Space);
                }
            }
        }

        public void Update(IEnumerable<TAgent> agents)
        {
            Initialize(agents);
            ReachedGoals.Add(TotalReachedGoals);
        }
    }
}
