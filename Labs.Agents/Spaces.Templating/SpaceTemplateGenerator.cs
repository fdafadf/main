using System;

namespace Labs.Agents
{
    public class SpaceTemplateGenerator
    {
        public static bool[,] GenerateObstacles(SpaceTemplateGeneratorProperties properties)
        {
            return GenerateObstacles(
                new Random(properties.Seed),
                properties.Width,
                properties.Height,
                properties.NumberOfObstacles,
                properties.ObstacleMinSize,
                properties.ObstacleMaxSize
            );
        }

        public static bool[,] GenerateObstacles(Random random, int width, int height, int numberOfObstacles, int obstacleMinSize, int obstacleMaxSize)
        {
            bool[,] obstaclesMap = new bool[width, height];
            GenerateObstacles(random, obstaclesMap, numberOfObstacles, obstacleMinSize, obstacleMaxSize);
            return obstaclesMap;
        }
         
        public static void GenerateObstacles(Random random, bool[,] obstaclesMap, int numberOfObstacles, int obstacleMinSize, int obstacleMaxSize)
        {
            int width = obstaclesMap.GetLength(0);
            int height = obstaclesMap.GetLength(1);

            for (int i = 0; i < numberOfObstacles; i++)
            {
                int oWidth = obstacleMinSize + random.Next(obstacleMaxSize - obstacleMinSize);
                int oHeight = obstacleMinSize + random.Next(obstacleMaxSize - obstacleMinSize);
                int oX = random.Next(width - oWidth);
                int oY = random.Next(height - oHeight);

                for (int dX = 0; dX < oWidth; dX++)
                {
                    for (int dY = 0; dY < oHeight; dY++)
                    {
                        obstaclesMap[oX + dX, oY + dY] = true;
                    }
                }
            }

            for (int x = 0; x < width; x++)
            {
                obstaclesMap[x, 0] = true;
                obstaclesMap[x, height - 1] = true;
            }

            for (int y = 0; y < height; y++)
            {
                obstaclesMap[0, y] = true;
                obstaclesMap[width - 1, y] = true;
            }
        }

        public static void GenerateAgents(SpaceTemplateGeneratorProperties properties, SpaceTemplate environmentBitmap)
        {
            GenerateAgents(new Random(properties.Seed), environmentBitmap.Obstacles, environmentBitmap.AgentMap, properties.NumberOfAgents);
            environmentBitmap.Update();
        }

        public static bool[,] GenerateAgents(SpaceTemplateGeneratorProperties properties, bool[,] obstaclesMap)
        {
            return GenerateAgents(new Random(properties.Seed), obstaclesMap, properties.NumberOfAgents);
        }

        public static SpaceTemplate Generate(SpaceTemplateGeneratorProperties properties)
        {
            Random random = new Random(properties.Seed);
            bool[,] obstacles = GenerateObstacles(random, properties.Width, properties.Height, properties.NumberOfObstacles, properties.ObstacleMinSize, properties.ObstacleMaxSize);
            bool[,] agents = GenerateAgents(random, obstacles, properties.NumberOfAgents);
            return new SpaceTemplate(obstacles, agents);
        }

        public static bool[,] GenerateAgents(Random random, bool[,] obstaclesMap, int numberOfAgents)
        {
            int width = obstaclesMap.GetLength(0);
            int height = obstaclesMap.GetLength(1);
            bool[,] agentsMap = new bool[width, height];
            GenerateAgents(random, obstaclesMap, agentsMap, numberOfAgents);
            return agentsMap;
        }

        public static void GenerateAgents(Random random, bool[,] obstaclesMap, bool[,] agentsMap, int numberOfAgents)
        {
            int width = obstaclesMap.GetLength(0);
            int height = obstaclesMap.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    agentsMap[x, y] = false;
                }
            }

            for (int i = 0; i < numberOfAgents; i++)
            {
                int x;
                int y;

                do
                {
                    x = random.Next(width);
                    y = random.Next(height);
                }
                while (obstaclesMap[x, y] || agentsMap[x, y]);

                agentsMap[x, y] = true;
            }
        }

        //public static void GenerateObstacles(IEnvironment environment, int numberOfObstacles, int obstacleMinSize, int obstacleMaxSize)
        //{
        //    Random random = environment.Random;
        //    int width = environment.Width;
        //    int height = environment.Height;
        //    environment.AddObstacle(0, 0, width, 1);
        //    environment.AddObstacle(0, 0, 1, height);
        //    environment.AddObstacle(0, height - 1, width, 1);
        //    environment.AddObstacle(width - 1, 0, 1, height);
        //
        //    for (int i = 0; i < numberOfObstacles; i++)
        //    {
        //        int oWidth = obstacleMinSize + random.Next(obstacleMaxSize - obstacleMinSize);
        //        int oHeight = obstacleMinSize + random.Next(obstacleMaxSize - obstacleMinSize);
        //        int oX = random.Next(width - oWidth);
        //        int oY = random.Next(height - oHeight);
        //        environment.AddObstacle(oX, oY, oWidth, oHeight);
        //    }
        //}
    }
}
