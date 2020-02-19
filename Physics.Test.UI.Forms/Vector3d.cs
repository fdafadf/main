using System;
using System.Runtime.CompilerServices;

namespace WindowsFormsApp1
{
    public class Vector3d
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3d()
        {

        }

        public Vector3d(Vector3d v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public Vector3d(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(Vector3d v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public void Mul(Matrix3d m)
        {
            X = m.Values[0, 0] * X + m.Values[1, 0] * Y + m.Values[2, 0] * Z;
            Y = m.Values[0, 1] * X + m.Values[1, 1] * Y + m.Values[2, 1] * Z;
            Z = m.Values[0, 2] * X + m.Values[1, 2] * Y + m.Values[2, 2] * Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(Vector3d v)
        {
            X += v.X;
            Y += v.Y;
            Z += v.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Mul(float v)
        {
            X *= v;
            Y *= v;
            Z *= v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Div(float v)
        {
            X /= v;
            Y /= v;
            Z /= v;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Dot(Vector3d v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public double Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Distance(Vector3d v)
        {
            float dx = X - v.X;
            float dy = Y - v.Y;
            float dz = Z - v.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            Div((float)Length);
        }

        public string ToString(string format)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "({0:F1},{1:F1})", X, Z);
        }

        public static void Sub(Vector3d v1, Vector3d v2, ref Vector3d result)
        {
            result.X = v1.X - v2.X;
            result.Y = v1.Y - v2.Y;
            result.Z = v1.Z - v2.Z;
        }

        public static void Sum(ref Vector3d result, Vector3d v1, Vector3d v2, Vector3d v3)
        {
            result.X = v1.X + v2.X + v3.X;
            result.Y = v1.Y + v2.Y + v3.Y;
            result.Z = v1.Z + v2.Z + v3.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
    }
}
