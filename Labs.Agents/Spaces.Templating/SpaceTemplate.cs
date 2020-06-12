using System.Drawing;

namespace Labs.Agents
{
    public class SpaceTemplate
    {
        public readonly Color ObstacleColor = Color.Black;
        public readonly Color AgentColor = Color.Red;
        public readonly Brush ObstacleBrush = Brushes.Black;
        public readonly Brush AgentBrush = Brushes.Red;
        public readonly Bitmap Bitmap;
        public readonly int Width;
        public readonly int Height;
        public readonly int Scale = 3;
        public int NumberOfAgents => AgentMap.Count(v => v == true);
        public readonly bool[,] Obstacles;
        public readonly bool[,] AgentMap;

        public SpaceTemplate(bool[,] obstacles, bool[,] agents)
        {
            Obstacles = obstacles;
            AgentMap = agents;
            Width = Obstacles.GetLength(0);
            Height = Obstacles.GetLength(1);
            Bitmap = new Bitmap(Width * Scale, Height * Scale);
            Update();
        }

        private SpaceTemplate(int width, int height)
        {
            Width = width;
            Height = height;
            Bitmap = new Bitmap(width * Scale, height * Scale);
            Obstacles = new bool[Width, Height];
            AgentMap = new bool[Width, Height];
        }

        public SpaceTemplate(Bitmap bitmap) : this(bitmap.Width, bitmap.Height)
        {
            int obstacle = ObstacleColor.ToArgb();
            int agent = AgentColor.ToArgb();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Obstacles[x, y] = bitmap.GetPixel(x, y).ToArgb() == obstacle;
                    AgentMap[x, y] = bitmap.GetPixel(x, y).ToArgb() == agent;
                }
            }

            Update();
        }

        //public void GenerateObstacles(EnvironmentGeneratorProperties properties)
        //{
        //    EnvironmentGenerator.GenerateObstacles(new Random(properties.Seed), obstacles, properties.NumberOfObstacles, properties.ObstacleMinSize, properties.ObstacleMaxSize);
        //}
        //
        //public void GenerateAgents(EnvironmentGeneratorProperties properties)
        //{
        //    EnvironmentGenerator.GenerateAgents(new Random(properties.Seed), obstacles, agents, properties.NumberOfAgents);
        //}

        public void Update()
        {
            using (Graphics graphics = Graphics.FromImage(Bitmap))
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        var brush = Obstacles[x, y] ? ObstacleBrush : (AgentMap[x, y] ? AgentBrush : Brushes.White);
                        //Bitmap.SetPixel(x, y, brush);
                        graphics.FillRectangle(brush, x * Scale, y * Scale, Scale, Scale);
                    }
                }
            }
        }

        public Bitmap CreatePreviewImage(int scale)
        {
            Bitmap obstacles = new Bitmap(Width * scale, Height * scale);

            using (Graphics bufferGraphics = Graphics.FromImage(obstacles))
            {
                bufferGraphics.PaintMap(Brushes.DarkGray, Width, Height, scale, (x, y) => Obstacles[x, y]);
                bufferGraphics.PaintMap(Brushes.Red, Width, Height, scale, (x, y) => AgentMap[x, y]);
            }

            return obstacles;
        }
    }
}
