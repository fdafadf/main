using System;
using System.Drawing;

namespace Labs.Agents
{
    public interface IEnvironment
    {
        int Width { get; }
        int Height { get; }
        void AddObstacle(int x, int y, int width, int height);
        Point GetRandomUnusedPosition(Random random);
    }
}
