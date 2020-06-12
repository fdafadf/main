using Labs.Agents.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationModel1<TPlugin, TAgent> : ISimulation, ISimulation<TAgent>, IShufflable
        where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> 
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
    {
        public Random Random = new Random(0);
        public DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent> Space;
        public RandomRenewableGoals<TAgent> Goals;
        public SimulationResults Results { get; private set; }
        public List<TAgent> Agents = new List<TAgent>();
        public int Iteration { get; private set; }
        public int TotalReachedGoals => Goals.TotalReachedGoals;
        public event Action<TAgent> AgentCreated;
        internal TPlugin Plugin;
        SimulationModelConfiguration ModelConfiguration;
        List<double> ConsumedTime = new List<double>();
        Stopwatch Stopwatch = new Stopwatch();
        bool shuffleRequested = false;
        IEnumerable<TAgent> ISimulation<TAgent>.Agents => Agents;

        public SimulationModel1(ISpaceTemplateFactory spaceDefinition, TPlugin plugin, string pluginName, SimulationModelConfiguration modelConfiguration)
        {
            Plugin = plugin;
            var spaceTemplate = spaceDefinition.CreateSpaceTemplate();
            var cardinalSpace = new CardinalMovementSpace<TAgent>(spaceTemplate, modelConfiguration.AgentsCollisionModel);
            Space = new DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>(cardinalSpace);
            Goals = new RandomRenewableGoals<TAgent>(Space.InteractiveSpace, modelConfiguration.CreateRandom());
            spaceTemplate.AgentMap.ForEachTrue(CreateAgent);
            Results = new SimulationResults(pluginName, spaceDefinition.Name);
            Results.Series.Add("Reached Goals", Goals.ReachedGoals);
            Results.Series.Add("Collisions", Space.Collisions);
            Results.Series.Add("ConsumedTime", ConsumedTime);
            ModelConfiguration = modelConfiguration;
        }

        public void Initialise()
        {
            Plugin.OnSimulationStarted(this);
        }

        public bool Iterate()
        {
            if (shuffleRequested)
            {
                DoShuffle();
            }

            Iteration++;
            Stopwatch.Start();
            Goals.OnIterationStarted(Agents);
            Plugin.OnIterationStarted(Agents);
            Space.Interact(Agents);
            Plugin.OnInteractionCompleted(Agents);
            Goals.OnInteractionCompleted(Agents);
            int destroyed = Agents.Count(agent => agent.Interaction.ActionResult == InteractionResult.Collision);

            for (int i = 0; i < destroyed; i++)
            {
                if (ModelConfiguration.AgentDestructionModel.CreateNew)
                {
                    CreateAgent();
                }
            }

            if (ModelConfiguration.AgentDestructionModel.RemoveDestoryed)
            {
                Agents.RemoveAll(agent => agent.Fitness.IsDestroyed);
            }

            Plugin.OnIterationCompleted(Agents);
            Stopwatch.Stop();
            ConsumedTime.Add(Stopwatch.Elapsed.TotalSeconds);
            return Iteration < ModelConfiguration.IterationLimit;
        }

        public void Complete()
        {
            Plugin.OnSimulationCompleted();
        }

        public void Pause()
        {
            Plugin.OnSimulationPaused();
        }

        public void Shuffle()
        {
            shuffleRequested = true;
        }

        private void DoShuffle()
        {
            shuffleRequested = false;
            var undestroyedAgents = Agents.Where(agent => agent.Fitness.IsDestroyed == false);

            foreach (var agent in undestroyedAgents)
            {
                Space.InteractiveSpace.MoveAgentAnchor(agent.Anchor, Random.GetEmptyField(Space.InteractiveSpace));
            }
        }

        private void CreateAgent()
        {
            var field = Random.GetEmptyField(Space.InteractiveSpace);
            CreateAgent(field.X, field.Y);
        }

        private void CreateAgent(int x, int y)
        {
            var agent = Plugin.CreateAgent(Space, x, y);
            Goals.Initialize(agent);
            Agents.Add(agent);
            AgentCreated?.Invoke(agent);
        }

        public override string ToString()
        {
            return $"Iteration: {Iteration}/{ModelConfiguration.IterationLimit} Reached Goals: {Goals.TotalReachedGoals} Collisions: {Space.TotalCollisions}";
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
                Initialize(agent);
            }
        }

        public void Initialize(TAgent agent)
        {
            if (agent.Goal.Position == null || agent.IsGoalReached())
            {
                agent.Goal.Position = Random.GetEmptyField(Space);
            }
        }

        public void OnIterationStarted(IEnumerable<TAgent> agents)
        {
            Initialize(agents);
            ReachedGoals.Add(TotalReachedGoals);
        }

        public void OnInteractionCompleted(IEnumerable<TAgent> agents)
        {
            foreach (var agent in agents)
            {
                if (agent.IsGoalReached())
                {
                    TotalReachedGoals++;
                }
            }

            ReachedGoals.Add(TotalReachedGoals);
        }
    }
}
