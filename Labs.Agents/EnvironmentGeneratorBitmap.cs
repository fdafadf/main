using System;
using System.Drawing;

namespace Labs.Agents
{
    public class EnvironmentGeneratorBitmap
    {
        public readonly Color ObstacleColor = Color.Black;
        public readonly Color AgentColor = Color.Red;
        public readonly Brush ObstacleBrush = Brushes.Black;
        public readonly Brush AgentBrush = Brushes.Red;
        public readonly Bitmap Bitmap;
        public readonly bool[,] Obstacles;
        public readonly bool[,] Agents;
        public readonly int Width;
        public readonly int Height;
        public readonly int Scale = 3;
        public int NumberOfAgents => Agents.Count(v => v == true);

        public EnvironmentGeneratorBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bitmap = new Bitmap(width * Scale, height * Scale);
            Obstacles = new bool[Width, Height];
            Agents = new bool[Width, Height];
        }

        public EnvironmentGeneratorBitmap(Bitmap bitmap) : this(bitmap.Width, bitmap.Height)
        {
            int obstacle = ObstacleColor.ToArgb();
            int agent = AgentColor.ToArgb();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Obstacles[x, y] = bitmap.GetPixel(x, y).ToArgb() == obstacle;
                    Agents[x, y] = bitmap.GetPixel(x, y).ToArgb() == agent;
                }
            }

            UpdateBitmap();
        }

        public void GenerateObstacles(EnvironmentGeneratorProperties properties)
        {
            EnvironmentGenerator.GenerateObstacles(new Random(properties.Seed), Obstacles, properties.NumberOfObstacles, properties.ObstacleMinSize, properties.ObstacleMaxSize);
        }

        public void GenerateAgents(EnvironmentGeneratorProperties properties)
        {
            EnvironmentGenerator.GenerateAgents(new Random(properties.Seed), Obstacles, Agents, properties.NumberOfAgents);
        }

        public void UpdateBitmap()
        {
            using (Graphics graphics = Graphics.FromImage(Bitmap))
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var brush = Obstacles[x, y] ? ObstacleBrush : (Agents[x, y] ? AgentBrush : Brushes.White);
                        //Bitmap.SetPixel(x, y, brush);
                        graphics.FillRectangle(brush, x * Scale, y * Scale, Scale, Scale);
                    }
                }
            }
        }
    }
}
