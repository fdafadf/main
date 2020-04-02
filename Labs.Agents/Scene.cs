using System.Collections.Generic;
using System.Numerics;

namespace Labs.Agents
{
    public class Scene
    {
        public Vector2 Size;
        public List<Obstacle> Obstacles = new List<Obstacle>();
        public List<Agent> Agents = new List<Agent>();

        public Scene(int width, int height)
        {
            Size = new Vector2(width, height);
        }
    }
}
