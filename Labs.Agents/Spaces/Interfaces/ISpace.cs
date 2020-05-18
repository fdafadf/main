using System;
using System.Collections.Generic;
using System.Drawing;

namespace Labs.Agents
{
    public interface ISpace
    {
        int Width { get; }
        int Height { get; }
        void AddObstacle(int x, int y, int width, int height);
        ISpaceField this[int x, int y] { get; }
    }
}
