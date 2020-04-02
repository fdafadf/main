using System.Linq;
using System.Numerics;

namespace Labs.Agents
{
    public class Agent : SceneObject
    {
        public int Size;
        public Vector2 Target;
        public Vector2 Velocity;

        public Agent(Vector2 position, int size)
        {
            Position = position;
            Size = size;
            Target = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        public bool Collide(Agent agent)
        {
            return agent != this && Vector2.Distance(Position, agent.Position) < Size + agent.Size;
        }

        public bool Collide(Obstacle obstacle)
        {
            return obstacle.Collide(this);
        }

        public bool Collide(Scene scene)
        {
            return scene.Agents.Any(Collide) || scene.Obstacles.Any(Collide);
        }
    }
}
