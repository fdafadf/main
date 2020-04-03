using System;
using System.Numerics;

namespace Labs.Agents
{
    public class Rectangle : SceneObject
    {
        public Vector2 Size;

        public Rectangle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public bool Collide(Circle agent)
        {
            return CollideWithCircle(agent.Position, agent.Radius);
        }

        public bool CollideWithCircle(Vector2 positon, int radius)
        {
            return CollideWithCircle(positon.X, positon.Y, radius);
        }

        public bool CollideWithCircle(float cx, float cy, float radius)
        {
            float rx = Position.X;
            float ry = Position.Y;
            float rw = Size.X;
            float rh = Size.Y;
            float testX = cx;
            float testY = cy;
            if (cx < rx) testX = rx;
            else if (cx > rx + rw) testX = rx + rw;
            if (cy < ry) testY = ry;
            else if (cy > ry + rh) testY = ry + rh;
            float distX = cx - testX;
            float distY = cy - testY;
            return Math.Sqrt((distX * distX) + (distY * distY)) <= radius;
        }
    }
}
