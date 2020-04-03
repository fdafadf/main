using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace Labs.Agents
{
    public static class Extensions
    {
        public static void Add(this List<Rectangle> self, float x, float y, float width, float height)
        {
            self.Add(new Rectangle(new Vector2(x, y), new Vector2(width, height)));
        }

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

        public static Vector2 Rotate(this Vector2 self, float degrees)
        {
            float sin = (float)Math.Sin(degrees * 0.0174532925);
            float cos = (float)Math.Cos(degrees * 0.0174532925);
            float tx = self.X;
            float ty = self.Y;
            self.X = (cos * tx) - (sin * ty);
            self.Y = (sin * tx) + (cos * ty);
            return self;
        }

        public static void DrawLine(this Graphics self, Pen pen, Vector2 from, Vector2 to)
        {
            self.DrawLine(pen, from.X, from.Y, to.X, to.Y);
        }

        public static void DrawEllipse(this Graphics self, Pen pen, Vector2 position, Vector2 size)
        {
            self.DrawEllipse(pen, position.X, position.Y, size.X, size.Y);
        }

        public static void FillRectangle(this Graphics self, Brush brush, Vector2 position, Vector2 size)
        {
            self.FillRectangle(brush, position.X, position.Y, size.X, size.Y);
        }

        public static T MinBy<T>(this IEnumerable<T> self, Func<T, float> predict)
        {
            T result = default(T);
            float resultValue = float.MaxValue;

            foreach (T item in self)
            {
                float itemValue = predict(item);

                if (itemValue < resultValue)
                {
                    result = item;
                    resultValue = itemValue;
                }
            }

            return result;
        }
    }
}
