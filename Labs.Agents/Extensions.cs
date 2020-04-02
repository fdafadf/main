using System;
using System.Numerics;

namespace Labs.Agents
{
    public static class Extensions
    {
        public static Vector2 NextVector2(this Random random, Vector2 max)
        {
            return new Vector2()
            {
                X = (float)random.NextDouble() * max.X,
                Y = (float)random.NextDouble() * max.Y
            };
        }

        public static Vector2 NextVector2(this Random random, float minX, float maxX, float minY, float maxY)
        {
            return new Vector2()
            {
                X = minX + (float)random.NextDouble() * (maxX - minX),
                Y = minY + (float)random.NextDouble() * (maxY - minY)
            };
        }
    }
}
