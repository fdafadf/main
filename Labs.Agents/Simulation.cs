using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Labs.Agents
{
    public class Simulation
    {
        public const int Directions = 4;
        public readonly Vector2[] Rotations = Enumerable.Range(0, Directions).Select(i => Vector2.UnitX.Rotate(360.0f / Directions * i)).ToArray();
        public Scene Scene { get; }
        public List<Agent> Agents = new List<Agent>();
        public Random Random { get; }
        public int TargetsAchieved;
        public float ElapsedTime;

        public Simulation(Scene scene, Random random)
        {
            Scene = scene;
            Random = random;
        }

        public Simulation(int width, int height, int numberOfObstacles, int numberOfAgents, Random random)
        {
            Random = random;
            Scene = new Scene(width, height);
            Scene.Rectangles.Add(0, 0, 10, height);
            Scene.Rectangles.Add(0, 0, width, 10);
            Scene.Rectangles.Add(width - 10, 0, 10, height);
            Scene.Rectangles.Add(0, height - 10, width, 10);

            for (int i = 0; i < numberOfObstacles; i++)
            {
                Scene.Rectangles.Add(new Rectangle(Random.NextVector2(Scene.Size), Random.NextVector2(5, 50, 5, 50)));
            }

            for (int i = 0; i < numberOfAgents; i++)
            {
                Vector2 position;
                int radius = 6;

                do
                {
                    position = Random.NextVector2(Scene.Size);
                }
                while (Scene.CollideWithCircle(position, radius));

                Agent agent = new Agent(position, radius);
                Agents.Add(agent);
                Scene.Circles.Add(agent.SceneObject);
            }
        }

        public void Update(float t)
        {
            ElapsedTime += t;

            foreach (Agent agent in Agents)
            {
                if (Vector2.Zero.Equals(agent.Target))
                {
                    agent.Target = Random.NextVector2(Scene.Size - new Vector2(22, 22)) + new Vector2(11, 11);
                }
                else
                {
                    if (Vector2.Distance(agent.SceneObject.Position, agent.Target) < agent.SceneObject.Radius)
                    {
                        agent.Target = Vector2.Zero;
                        TargetsAchieved++;
                    }
                    else
                    {
                        agent.Update(this, t);
                        agent.SceneObject.Position += agent.Velocity * t;
                    }
                }
            }
        }
    }
}
