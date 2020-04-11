using System;

namespace Labs.Agents
{
    public class Environment2Generator<TAgent, TState> 
        where TAgent : IAgent<Environment2<TAgent, TState>, TAgent, TState>
        where TState : AgentState2<TAgent, TState>
    {
        public static Environment2<TAgent, TState> Generate(Random random, int width, int height, int numberOfObstacles, int obstacleMinSize, int obstacleMaxSize)
        {
            var environment = new Environment2<TAgent, TState>(width, height);
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

            return environment;
        }
    }
}
