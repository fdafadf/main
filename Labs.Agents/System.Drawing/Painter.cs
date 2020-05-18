﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents
{
    public class Painter
    {
        protected ISpace Environment { get; }
        protected readonly int Scale = 3;

        Bitmap obstaclesBuffer;

        public Painter(ISpace space)
        {
            Environment = space;
            obstaclesBuffer = CreateObstaclesBitmap(space.Width, space.Height, Scale, (x, y) => space[x, y].IsObstacle);
        }

        public static Bitmap CreateObstaclesBitmap(int width, int height, int scale, Func<int, int, bool> map)
        {
            Bitmap obstacles = new Bitmap(width * scale, height * scale);

            using (Graphics bufferGraphics = Graphics.FromImage(obstacles))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (map(x, y))
                        {
                            bufferGraphics.FillRectangle(Brushes.DarkGray, x * scale, y * scale, scale, scale);
                        }
                    }
                }
            }

            return obstacles;
        }

        public void PaintObstacles(Graphics graphics)
        {
            graphics.DrawImage(obstaclesBuffer, 0, 0);
        }

        public void PaintAgents<TAgent>(Graphics graphics, IEnumerable<TAgent> agents) where TAgent : IAnchoredAgent<TAgent>, IDestructibleAgent
        {
            foreach (TAgent agent in agents)
            {
                var x = agent.Anchor.Field.X;
                var y = agent.Anchor.Field.Y;
                Brush brush = agent.Fitness.IsDestroyed ? Brushes.Red : Brushes.Black;
                graphics.FillRectangle(brush, x * Scale, y * Scale, Scale, Scale);
            }
        }

        public void PaintGoals<TAgent>(Graphics graphics, IEnumerable<TAgent> agents) where TAgent : IAnchoredAgent<TAgent>, IGoalAgent
        {
            foreach (TAgent agent in agents)
            {
                if (agent.Goal.Position != Point.Empty)
                {
                    var ax = agent.Anchor.Field.X;
                    var ay = agent.Anchor.Field.Y;
                    var bx = agent.Goal.Position.X;
                    var by = agent.Goal.Position.Y;
                    graphics.DrawLine(Pens.LightBlue, ax * Scale, ay * Scale, bx * Scale, by * Scale);
                }
            }
        }
    }
}