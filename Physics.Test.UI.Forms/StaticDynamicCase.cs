using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class StaticDynamicCase : IScene2
    {
        public Sphere s1 = new Sphere();
        public List<Sphere3d> Spheres { get; }
        public List<Sphere> Spheres2d { get; }
        CollisionDetector detector = new CollisionDetector();
        Vector cw = new Vector();

        public StaticDynamicCase()
        {
            Spheres2d = new List<Sphere>();
            s1.Radius = 50;
            Spheres2d.Add(new Sphere());
            Spheres2d.Add(new Sphere());
            Spheres2d.Add(new Sphere());
            Spheres2d[0].Radius = 70;
            Spheres2d[1].Radius = 70;
            Spheres2d[2].Radius = 40;
            this.Reset();
        }

        public void Reset()
        {
            s1.Position.Set(150, 250);
            Spheres2d[0].Position.Set(260, 330);
            Spheres2d[1].Position.Set(180, 30);
            Spheres2d[2].Position.Set(480, 160);
            s1.Move.Set(12, -3);
            s1.Position.Add(-36, 9);
        }

        public void Paint(PaintEventArgs e)
        {
            s1.Paint(e);

            foreach (Sphere staticSphere in Spheres2d)
            {
                staticSphere.Paint(e);
            }
        }

        public void UpdateInput()
        {
            foreach (Sphere staticSphere in Spheres2d)
            {
                detector.Calculate(s1, staticSphere);

                if (detector.CollisionOcurred)
                {
                    s1.Position.Set(detector.s1OnCollision);
                    s1.Move.Set(detector.w);
                    Vector.Normalize(s1.Move, ref s1.Move);
                    s1.Move.Mul(15);
                    return;
                }
            }

            //s1.Position.Add(s1.Move.X * t, s1.Move.Y * t);
        }

        public void PaintDetails(PaintEventArgs e)
        {
            foreach (Sphere staticSphere in Spheres2d)
            {
                e.Graphics.FillCircle(Brushes.White, staticSphere.Position, staticSphere.Radius);
            }

            foreach (Sphere staticSphere in Spheres2d)
            {
                detector.Calculate(s1, staticSphere);

                e.Graphics.DrawLine(Pens.Cyan, s1.Position, detector.d);
                e.Graphics.DrawLine(Pens.Cyan, staticSphere.Position, detector.d);

                if (detector.CollisionOcurred)
                {
                    e.Graphics.DrawCircle(Pens.Red, detector.s1OnCollision, s1.Radius);
                    Vector.Sum(detector.s1OnCollision, detector.w, ref cw);
                    e.Graphics.DrawArrow(Pens.DarkBlue, detector.s1OnCollision, cw);
                    e.Graphics.DrawLine(Pens.Magenta, s1.Position, s1.Position + detector.s1ToCollision);
                }
            }

            //form.Text = $"{detector.DirectionToCollision} {detector.DistanceToCollision} {detector.DistanceToReach}";

            s1.PaintDetails(e);

            foreach (Sphere staticSphere in Spheres2d)
            {
                staticSphere.PaintDetails(e);
            }
        }

        public void KeyDown(Keys keys)
        {

        }

        public bool MouseMove(MouseEventArgs e)
        {
            return false;
        }


        class CollisionDetector
        {
            public Vector d = new Vector();
            public Vector s1OnCollision = new Vector();
            public Vector w = new Vector();
            public bool CollisionOcurred;
            public Vector s1ToCollision = new Vector();

            Vector n1 = new Vector();
            Vector n2 = new Vector();

            public bool DirectionToCollision;
            public double DistanceToCollision;
            public double DistanceToReach;

            public void Calculate(Sphere s1, Sphere s2)
            {
                Helper.ClosestPointOnLine(s1, s2, ref d);
                double closestPossibleDistance = Math.Pow(s2.Position.X - d.X, 2) + Math.Pow(s2.Position.Y - d.Y, 2);

                if (closestPossibleDistance <= Math.Pow(s1.Radius + s2.Radius, 2))
                {
                    //Vector.Sub(d, s1.Position, ref s1ToD);
                    //// Zrobić lepsze sprawdzanie cy są w tym samym kierunku
                    //
                    ////double crossProduct = Vector.CrossProduct(s1.Move, s1ToD);
                    ////CollisionOcurred = true;
                    //Vector.Normalize(s1.Move, ref n1);
                    //Vector.Normalize(s1ToD, ref n2);
                    //DirectionToD = n1.Equals(n2, 0.001f);
                    //DistanceToReach = Math.Sqrt(s1.Move.X * s1.Move.X + s1.Move.Y * s1.Move.Y);
                    //DistanceToD = Math.Sqrt(s1ToD.X * s1ToD.X + s1ToD.Y * s1ToD.Y);
                    //
                     
                    //??
                    double backDistance = Math.Sqrt(Math.Pow(s1.Radius + s2.Radius, 2) - closestPossibleDistance);
                    double movementVector = Math.Sqrt(Math.Pow(s1.Move.X, 2) + Math.Pow(s1.Move.Y, 2));
                    double backX = d.X - backDistance * (s1.Move.X / movementVector);
                    double backY = d.Y - backDistance * (s1.Move.Y / movementVector);
                    s1OnCollision.Set(backX, backY);

                    Vector.Sub(s1OnCollision, s1.Position, ref s1ToCollision);
                    Vector.Normalize(s1.Move, ref n1);
                    Vector.Normalize(s1ToCollision, ref n2);
                    DirectionToCollision = n1.Equals(n2, 0.001f);
                    DistanceToReach = s1.Move.GetLength(); //Math.Sqrt(s1.Move.X * s1.Move.X + s1.Move.Y * s1.Move.Y);
                    DistanceToCollision = s1ToCollision.GetLength(); //Math.Sqrt(s1ToCollision.X * s1ToCollision.X + s1ToCollision.Y * s1ToCollision.Y);

                    if (DirectionToCollision && DistanceToReach >= DistanceToCollision)
                    {
                        CollisionOcurred = true;

                        // reakcja

                        //double collisionDistance = Math.Sqrt(Math.Pow(s2.Position.X - backX, 2) + Math.Pow(s2.Position.Y - backY, 2));
                        double collisionDistance = Math.Sqrt(Math.Pow(s2.Position.X - backX, 2) + Math.Pow(s2.Position.Y - backY, 2));
                        double n_x = (s2.Position.X - backX) / collisionDistance;
                        double n_y = (s2.Position.Y - backY) / collisionDistance;
                        double p = 2 * (s1.Move.X * n_x + s1.Move.Y * n_y) / (s1.Mass + s2.Mass);
                        double w_x = s1.Move.X - p * s1.Mass * n_x - p * s2.Mass * n_x;
                        double w_y = s2.Move.Y - p * s1.Mass * n_y - p * s2.Mass * n_y;

                        Console.WriteLine($"n_x: {n_x}");
                        Console.WriteLine($"n_y: {n_y}");
                        Console.WriteLine($"p: {p}");
                        Console.WriteLine();
                        //Vector w = new Vector((float)w_x, (float)w_y);
                        //Vector cw = s1CenterAtCollision + w;
                        w.Set(w_x, w_y);
                    }
                    else
                    {
                        CollisionOcurred = false;
                    }
                }
                else
                {
                    CollisionOcurred = false;
                }
            }
        }
    }
}
