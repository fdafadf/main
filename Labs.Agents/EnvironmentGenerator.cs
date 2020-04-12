using System;

namespace Labs.Agents
{
    public class EnvironmentGenerator
    {
        public static void GenerateObstacles(IEnvironment environment, int numberOfObstacles, int obstacleMinSize, int obstacleMaxSize)
        {
            Random random = environment.Random;
            int width = environment.Width;
            int height = environment.Height;
            environment.AddObstacle(0, 0, width, 1);
            environment.AddObstacle(0, 0, 1, height);
            environment.AddObstacle(0, height - 1, width, 1);
            environment.AddObstacle(width - 1, 0, 1, height);

            for (int i = 0; i < numberOfObstacles; i++)
            {
                int oWidth = obstacleMinSize + random.Next(obstacleMaxSize - obstacleMinSize);
                int oHeight = obstacleMinSize + random.Next(obstacleMaxSize - obstacleMinSize);
                int oX = random.Next(width - oWidth);
                int oY = random.Next(height - oHeight);
                environment.AddObstacle(oX, oY, oWidth, oHeight);
            }
        }
    }
}
