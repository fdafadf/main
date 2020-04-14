using AI.NeuralNetworks;
using System;

namespace Labs.Agents
{
    public class EnvironmentGenerator
    {
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

        public static void GenerateAgents(Random random, bool[,] obstaclesMap, bool[,] agents, int numberOfAgents)
        {
            int width = obstaclesMap.GetLength(0);
            int height = obstaclesMap.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    agents[x, y] = false;
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
                while (obstaclesMap[x, y] || agents[x, y]);

                agents[x, y] = true;
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
