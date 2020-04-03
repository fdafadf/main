using System.Linq;
using System.Numerics;

namespace Labs.Agents
{
    public class Circle : SceneObject
    {
        public int Radius;

        public Circle(Vector2 position, int radius)
        {
            Position = position;
            Radius = radius;
        }

        public bool Collide(Circle agent)
        {
            return agent != this && Vector2.Distance(Position, agent.Position) < Radius + agent.Radius;
        }

        public bool CollideWithCircle(Vector2 positon, int radius)
        {
            return Vector2.Distance(Position, positon) < Radius + radius;
        }

        public bool Collide(Rectangle obstacle)
        {
            return obstacle.Collide(this);
        }
    }
}
