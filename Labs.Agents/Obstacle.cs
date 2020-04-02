using System;
using System.Numerics;

namespace Labs.Agents
{
    public class Obstacle : SceneObject
    {
        public Vector2 Size;

        public bool Collide(Agent agent)
        {
            float cx = agent.Position.X;
            float cy = agent.Position.Y;
            float radius = agent.Size;
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
