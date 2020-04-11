using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class Environment
    {
        public static readonly AgentAction[] Actions = new AgentAction[]
        {
            new AgentAction(0, false),
            new AgentAction(1, false),
            new AgentAction(2, false),
            new AgentAction(0, true),
            new AgentAction(1, true),
            new AgentAction(2, true),
        };

        public Color SandColor = Color.Yellow;
        public Bitmap Bitmap;
        public Random Random;
        public Agent[] Agents;

        public Environment(int width, int height, Random random)
        {
            Random = random;
            Bitmap = new Bitmap(width, height);

            using (ObstaclesBitmap obstacles = new ObstaclesBitmap(Bitmap))
            {
                Agent agent = new Agent();
                agent.Ant = new Ant(Bitmap.Width / 2, Bitmap.Height / 2, 1, 0, AgentState.Sensors);
                agent.State = GetState(agent.Ant, obstacles);

                Agents = new Agent[]
                {
                    agent
                };
            }
        }

        public void Randomize(Color backColor)
        {
            lock (Bitmap)
            {
                using (Graphics g = Graphics.FromImage(Bitmap))
                {
                    g.Clear(backColor);
                    g.FillRectangle(Brushes.Yellow, 0, 0, Bitmap.Width, 3);
                    g.FillRectangle(Brushes.Yellow, 0, 0, 3, Bitmap.Height);
                    g.FillRectangle(Brushes.Yellow, Bitmap.Width - 3, 0, Bitmap.Width, Bitmap.Height);
                    g.FillRectangle(Brushes.Yellow, 0, Bitmap.Height - 3, Bitmap.Width, Bitmap.Height);

                    for (int i = 0; i < 20; i++)
                    {
                        int x = Random.Next(Bitmap.Width - 5);
                        int y = Random.Next(Bitmap.Height - 5);
                        int width = Random.Next(90) + 50;
                        int height = Random.Next(90) + 50;
                        g.FillRectangle(Brushes.Yellow, x, y, width, height);
                    }
                }

                using (ObstaclesBitmap obstacles = new ObstaclesBitmap(Bitmap))
                {
                    foreach (Agent agent in Agents)
                    {
                        NewGoal(agent, obstacles);
                        NewPosition(agent, obstacles);
                    }
                }
            }
        }

        public double DoAction(Agent agent, AgentAction action)
        {
            double reward = 0;

            lock (Bitmap)
            {
                using (ObstaclesBitmap obstacles = new ObstaclesBitmap(Bitmap))
                {
                    double lastDistance = agent.Ant.GetDistanceToGoal();
                    agent.Ant.Update(action);
                    Vector2 newPosition = agent.Ant.Position + agent.Ant.Velocity;

                    if (obstacles.CollideRectangle(newPosition, agent.Ant.Size))
                    {
                        reward = -1;
                    }
                    else
                    {
                        agent.Ant.Position = newPosition;
                        double distance = agent.Ant.GetDistanceToGoal();
                        reward = lastDistance - distance;

                        if (distance < 5)
                        {
                            NewGoal(agent, obstacles);
                        }
                    }

                    agent.State = GetState(agent.Ant, obstacles);
                }
            }

            return reward;
        }

        private void NewPosition(Agent agent, ObstaclesBitmap obstacles)
        {
            do
            {
                agent.Ant.Goal.X = Random.Next(Bitmap.Width - 40) + 20;
                agent.Ant.Goal.Y = Random.Next(Bitmap.Height - 40) + 20;
            }
            while (obstacles.CollideRectangle(agent.Ant.Goal, agent.Ant.Size));
        }

        private void NewGoal(Agent agent, ObstaclesBitmap obstacles)
        {
            do
            {
                agent.Ant.Goal.X = Random.Next(Bitmap.Width - 40) + 20;
                agent.Ant.Goal.Y = Random.Next(Bitmap.Height - 40) + 20;
            }
            while (obstacles.CollideRectangle(agent.Ant.Goal, 1));
        }

        private AgentState GetState(Ant agent, ObstaclesBitmap obstacles)
        {
            var v1 = Vector2.Normalize(agent.Direction);
            var v2 = Vector2.Normalize(agent.Goal - agent.Position);
            var signal1 = GetSignal(agent.Sensors[0], obstacles);
            var signal2 = GetSignal(agent.Sensors[1], obstacles);
            var signal3 = GetSignal(agent.Sensors[2], obstacles);
            var sin = Vector2.Dot(v2, v1);
            var cos = v1.CrossProduct(v2);
            return new AgentState(sin, cos, signal1, signal2, signal3);
        }

        public double GetSignal(AntSensor sensor, ObstaclesBitmap obstacles)
        {
            sensor.UpdatePosition();
            return obstacles.GetSignal(sensor);
        }
    }
}
