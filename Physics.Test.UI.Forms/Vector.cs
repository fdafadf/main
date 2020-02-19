using System;

namespace WindowsFormsApp1
{
    public class Vector
    {
        public float X;
        public float Y;

        public Vector()
        {
        }

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool IsZero
        {
            get
            {
                return this.X == 0 && this.Y == 0;
            }
        }

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public void Mul(float n)
        {
            this.X *= n;
            this.Y *= n;
        }

        public void Add(Vector v)
        {
            this.X += v.X;
            this.Y += v.Y;
        }

        public void Sub(Vector v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
        }

        public void Add(float x, float y)
        {
            this.X += x;
            this.Y += y;
        }

        public void Set(double x, double y)
        {
            this.X = (float)x;
            this.Y = (float)y;
        }

        public void Set(Vector v)
        {
            this.X = v.X;
            this.Y = v.Y;
        }

        public bool Equals(Vector v, float epsilon)
        {
            return Math.Abs(X - v.X) < epsilon && Math.Abs(Y - v.Y) < epsilon;
        }

        public static void Sum(Vector v1, Vector v2, ref Vector result)
        {
            result.X = v1.X + v2.X;
            result.Y = v1.Y + v2.Y;
        }

        public static void Sub(Vector v1, Vector v2, ref Vector result)
        {
            result.X = v1.X - v2.X;
            result.Y = v1.Y - v2.Y;
        }

        public static void Normalize(Vector v, ref Vector result)
        {
            double distance = Math.Sqrt(v.X * v.X + v.Y * v.Y);
            result.Set(v.X / distance, v.Y / distance);
        }

        public static double CrossProduct(Vector v1, Vector v2)
        {
            return v1.X * v2.Y - v2.X * v1.Y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }
    }
}
