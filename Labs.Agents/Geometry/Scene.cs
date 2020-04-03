using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Labs.Agents
{
    public class Scene
    {
        public Vector2 Size;
        public List<Rectangle> Rectangles = new List<Rectangle>();
        public List<Circle> Circles = new List<Circle>();

        public Scene(int width, int height)
        {
            Size = new Vector2(width, height);
        }

        public bool Collide(Circle circle)
        {
            return Circles.Any(circle.Collide) || Rectangles.Any(circle.Collide);
        }

        public bool CollideWithCircle(Vector2 positon, int radius)
        {
            return Circles.Any(circle => circle.CollideWithCircle(positon, radius)) 
                || Rectangles.Any(rectangle => rectangle.CollideWithCircle(positon, radius));
        }
    }
}
