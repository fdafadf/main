using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CollisionSystem
    {
        private List<Sphere3d> Objects { get; }
        private Triangle[] Triangles { get; }
        private float?[,] collisionMatrix;
        private Point closest;
        private float closestTime;

        public CollisionSystem(List<Sphere3d> objects, Triangle[] triangles) //, List<Box3d> objects2)
        {
            Objects = objects;
            Triangles = triangles;
            collisionMatrix = new float?[Objects.Count, Objects.Count];
            RecalculateCollisionMatrix();
            RecalculateClosestCollision();
            Console.WriteLine($"{closestTime}");
            CollisionForm.Show();
        }

        public float? Update(float rotationY, float t)
        {
            float? result = null;
            HashSet<int> toRecalculate = new HashSet<int>();

            if (t >= closestTime)
            {
                Console.WriteLine("Collision");
                MoveObjects(closestTime);
                Sphere3d s1 = Objects[closest.X];
                Sphere3d s2 = Objects[closest.Y];
                result = CalculateReaction(rotationY, s1, s2);
                SubstractCollisionTimes(closestTime);
                toRecalculate.Add(closest.X);
                toRecalculate.Add(closest.Y);
            }
            else
            {
                MoveObjects(t);
                SubstractCollisionTimes(t);
                closestTime -= t;
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                Sphere3d sphere = Objects[i];

                if (sphere.Position.X < 0 && sphere.Direction.X < 0)
                {
                    sphere.Direction.X = -sphere.Direction.X;
                    toRecalculate.Add(i);
                }
                
                if (sphere.Position.X > 800 && sphere.Direction.X > 0)
                {
                    sphere.Direction.X = -sphere.Direction.X;
                    toRecalculate.Add(i);
                }
                
                if (sphere.Position.Y < 0 && sphere.Direction.Y < 0)
                {
                    sphere.Direction.Y = -sphere.Direction.Y;
                    toRecalculate.Add(i);
                }
                
                if (sphere.Position.Y > 600 && sphere.Direction.Y > 0)
                {
                    sphere.Direction.Y = -sphere.Direction.Y;
                    toRecalculate.Add(i);
                }
            }

            foreach (int i in toRecalculate)
            {
                RecalculateCollisions(i);
            }

            if (toRecalculate.Count > 0)
            {
                RecalculateClosestCollision();
            }

            return result;
        }

        private void MoveObjects(float t)
        {
            foreach (Sphere3d sphere in Objects)
            {
                sphere.Position.X += sphere.Direction.X * t;
                sphere.Position.Y += sphere.Direction.Y * t;
                sphere.Position.Z += sphere.Direction.Z * t;
            }
        }

        public void RecalculateCollisions(int sphereIndex)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (i != sphereIndex)
                {
                    int a = Math.Min(i, sphereIndex);
                    int b = Math.Max(i, sphereIndex);
                    collisionMatrix[a, b] = CollisionSystem.CalculateCollision(Objects[a], Objects[b]);
                }
            }
        }

        private void SubstractCollisionTimes(float t)
        {
            for (int i = 0; i < Objects.Count - 1; i++)
            {
                for (int j = i + 1; j < Objects.Count; j++)
                {
                    if (collisionMatrix[i, j].HasValue)
                    {
                        collisionMatrix[i, j] -= t;
                    }
                }
            }
        }

        private void RecalculateCollisionMatrix()
        {
            for (int i = 0; i < Objects.Count - 1; i++)
            {
                for (int j = i + 1; j < Objects.Count; j++)
                {
                    collisionMatrix[i, j] = CollisionSystem.CalculateCollision(Objects[i], Objects[j]);
                }
            }
        }

        public void RecalculateClosestCollision()
        {
            closest.X = 0;
            closest.Y = 0;
            closestTime = float.MaxValue;

            for (int i = 0; i < Objects.Count - 1; i++)
            {
                for (int j = i + 1; j < Objects.Count; j++)
                {
                    if (collisionMatrix[i, j].HasValue)
                    {
                        float t = collisionMatrix[i, j].Value;

                        if (t < closestTime && t > 0)
                        {
                            closestTime = t;
                            closest.X = i;
                            closest.Y = j;
                        }
                    }
                }
            }
        }

        private static void WriteDebugInfo(float rotationY, Sphere3d s1, Sphere3d s2)
        {
            string vector(Vector3d v) { return $"new Vector3d({v.X}f, {v.Y}f, {v.Z}f)"; }
            Console.WriteLine($"float rotationY = {rotationY}f;");
            Console.WriteLine($"Sphere3d s1 = new Sphere3d({vector(s1.Position)}, {s1.Radius}, {vector(s1.Direction)});");
            Console.WriteLine($"Sphere3d s2 = new Sphere3d({vector(s2.Position)}, {s1.Radius}, {vector(s2.Direction)});");
        }

        private static CollisionForm CollisionForm = new CollisionForm();

        public static float? CalculateReaction(float rotationY, Sphere3d s1, Sphere3d s2)
        {
            float? result = null;
            Vector3d n = s1.Position - s2.Position;
            n.Normalize();
            n.Mul(n.Dot(s1.Direction) * 2);
            Vector3d r = s1.Direction - n;

            if (s2 != null)
            {
                //Vector3d direction = new Vector3d();
                //direction.Set(s1.Direction);
                //direction.Normalize();
                result = 57.29577f * (float)Math.Atan2(r.Z, r.X);

                CollisionForm.RotationY = rotationY;
                CollisionForm.NewRotationY = result.Value;
                CollisionForm.Sphere1 = s1;
                CollisionForm.Sphere2 = s2;
                CollisionForm.Reaction = r;
                CollisionForm.Empty = false;
                CollisionForm.Refresh();
            }

            s1.Direction = r;
            return result;

            // https://math.stackexchange.com/questions/13261/how-to-get-a-reflection-vector
            //s1.Direction = r;
            //float rx = 
            //float x = s1.Direction.X;
            //float y = s1.Direction.Y;
            //s1.Direction.X = 0;
            //s1.Direction.Y = 0;
            //s2.Direction.X = x;
            //s2.Direction.Y = y;
            ////s2.RotationY = s1.RotationY;
            ////s2.RecalculateDirection(5);
            //s2.RotationY = s1.RotationY;
            ////s1.RotationY += 180;

            //Vector3d vecx = new Vector3d();
            //Vector3d.Sub(s1.Position, s2.Position, ref vecx);
            //Vector3d.Normalize(vecx, ref vecx);
            //Vector3d vecv1 = new Vector3d(s1.Direction);
            //float x1 = vecx.Dot(s1.Direction);
            //Vector3d vecv1x = new Vector3d(vecx);
            //vecv1x.Mul(x1);
            //Vector3d vecv1y = new Vector3d();
            //Vector3d.Sub(vecv1, vecv1x, ref vecv1y);
            //float m1 = 1;
            //
            //vecx.Mul(-1);
            //Vector3d vecv2 = new Vector3d(s2.Direction);
            //float x2 = vecx.Dot(s2.Direction);
            //Vector3d vecv2x = new Vector3d(vecx);
            //vecv2x.Mul(x2);
            //Vector3d vecv2y = new Vector3d();
            //Vector3d.Sub(vecv2, vecv2x, ref vecv2y);
            //float m2 = 1;
            //
            //Vector3d vecv2xa = new Vector3d(vecv2x);
            //vecv2xa.Mul((2 * m2) / (m1 + m2));
            //vecv1x.Mul((m1 - m2) / (m1 - m2));
            //Vector3d.Sum(ref s1.Direction, vecv1x, vecv2xa, vecv1);

        }

        private static Vector3d vecs = new Vector3d();
        private static Vector3d vecv = new Vector3d();
        private static Vector3d collp = new Vector3d();

        public static float? CalculateCollision(Sphere3d s1, Sphere3d s2)
        {
            Vector3d.Sub(s1.Position, s2.Position, ref vecs);
            Vector3d.Sub(s1.Direction, s2.Direction, ref vecv);
            float radiusSum = s1.Radius + s2.Radius;
            float c = vecs.Dot(vecs) - radiusSum * radiusSum;
            float t = 0;

            if (c < 0)
            {
                return 0;
            }
            else
            {
                float a = vecv.Dot(vecv);
                float b = vecv.Dot(vecs);

                if (b >= 0)
                {
                    return null;
                }
                else
                {
                    float d = b * b - a * c;

                    if (d < 0)
                    {
                        return null;
                    }
                    else
                    {
                        t = (float)(-b - Math.Sqrt(d)) / a;
                        return t;
                    }
                }
            }
        }
    }
}
