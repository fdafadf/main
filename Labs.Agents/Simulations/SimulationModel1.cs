using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationModel1<TDriver, TAgent> : ISimulation
        where TDriver : SimulationAgentDriver<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> // IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructible, IGoalAgent
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
    {
        public Random Random = new Random(0);
        public DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent> Space;
        public RandomRenewableGoals<TAgent> Goals;
        public SimulationResults Results;
        public List<TAgent> Agents = new List<TAgent>();
        TDriver AgentDriver;
        DateTime StartTime;
        List<double> ConsumedTime = new List<double>();

        public SimulationModel1(ISpaceDefinition spaceDefinition, TDriver agentDriver, string agentName, AgentsCollisionModel agentsCollisionModel)
        {
            var spaceTemplate = spaceDefinition.CreateSpaceTemplate();
            var cardinalSpace = new CardinalMovementSpace<TAgent>(spaceTemplate, agentsCollisionModel);
            Space = new DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>(cardinalSpace);
            Agents.AddRange(spaceTemplate.AgentMap.SelectTrue((x, y) => agentDriver.CreateAgent(Space, x, y)));
            Goals = new RandomRenewableGoals<TAgent>(Space.InteractiveSpace, new Random(0));
            Goals.Update(Agents);
            Results = new SimulationResults(agentName, spaceDefinition.Name);
            Results.Series.Add("Reached Goals", Goals.ReachedGoals);
            Results.Series.Add("Collisions", Space.Collisions);
            Results.Series.Add("ConsumedTime", ConsumedTime);
            AgentDriver = agentDriver;
        }

        public bool Iterate()
        {
            if (StartTime == default)
            {
                StartTime = DateTime.Now;
            }

            AgentDriver.OnIterationStarted();
            Space.Interact(Agents);
            AgentDriver.OnInteractionCompleted();
            Goals.Update(Agents);
            int destroyed = Agents.Count(agent => agent.Interaction.ActionResult == InteractionResult.Collision);

            for (int i = 0; i < destroyed; i++)
            {
                var field = Random.GetUnusedField(Space.InteractiveSpace);
                Agents.Add(AgentDriver.CreateAgent(Space, field.X, field.Y));
            }

            AgentDriver.OnIterationCompleted();
            ConsumedTime.Add((DateTime.Now - StartTime).TotalSeconds);
            return true;
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

        public void Update(IEnumerable<TAgent> agents)
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

            ReachedGoals.Add(TotalReachedGoals);
        }
    }
}
