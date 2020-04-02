using System;
using System.Numerics;

namespace Labs.Agents
{
    public class Simulation
    {
        public Scene Scene { get; }
        public Random Random { get; }

        public Simulation(Scene scene, Random random)
        {
            Scene = scene;
            Random = random;
        }

        public Simulation(int width, int height, int numberOfObstacles, int numberOfAgents, Random random)
        {
            Random = random;
            Scene = new Scene(width, height);

            for (int i = 0; i < numberOfObstacles; i++)
            {
                Scene.Obstacles.Add(new Obstacle()
                {
                    Position = Random.NextVector2(Scene.Size),
                    Size = Random.NextVector2(5, 15, 5, 15),
                });
            }

            for (int i = 0; i < numberOfAgents; i++)
            {
                Agent agent;

                do
                {
                    agent = new Agent(position: Random.NextVector2(Scene.Size), size: 3);
                }
                while (agent.Collide(Scene));

                Scene.Agents.Add(agent);
            }
        }

        public void Update(float t)
        {
            foreach (Agent agent in Scene.Agents)
            {
                if (Vector2.Zero.Equals(agent.Target))
                {
                    agent.Target = Random.NextVector2(Scene.Size);
                }
                else
                {
                    if (Vector2.Distance(agent.Position, agent.Target) < agent.Size)
                    {
                        agent.Target = Vector2.Zero;
                    }
                    else
                    {
                        agent.Velocity = Vector2.Normalize(agent.Target - agent.Position);
                        agent.Position += agent.Velocity * t;
                    }
                }
            }
        }
    }
}
