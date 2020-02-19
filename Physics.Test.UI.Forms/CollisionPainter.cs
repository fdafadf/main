using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CollisionPainter : IPaintable
    {
        public List<Sphere3d> Spheres { get; }

        public CollisionPainter(List<Sphere3d> spheres)
        {
            Spheres = spheres;
        }

        //private static Vector3d collp = new Vector3d();
        private static Sphere3d c1 = new Sphere3d(new Vector3d(), 0, new Vector3d());
        private static Sphere3d c2 = new Sphere3d(new Vector3d(), 0, new Vector3d());
        private static Pen CollisionCirecleCentersPen = new Pen(Color.FromArgb(255, 170, 30));

        public void Paint(PaintEventArgs e)
        {
            for (int i = 0; i < Spheres.Count - 1; i++)
            {
                for (int j = i + 1; j < Spheres.Count; j++)
                {
                    Sphere3d s1 = Spheres[i];
                    Sphere3d s2 = Spheres[j];
                    float? t = CollisionSystem.CalculateCollision(s1, s2);
            
                    if (t.HasValue && t.Value != 0)
                    {
                        s1.Move(t.Value, ref c1);
                        //collp.X = s1.Direction.X * t.Value + s1.Position.X;
                        //collp.Y = s1.Direction.Y * t.Value + s1.Position.Y;
                        e.Graphics.DrawCircle(Pens.Red, c1.Position.X, c1.Position.Y, s1.Radius);
                        s2.Move(t.Value, ref c2);
                        //collp.X = s2.Direction.X * t.Value + s2.Position.X;
                        //collp.Y = s2.Direction.Y * t.Value + s2.Position.Y;
                        e.Graphics.DrawCircle(Pens.Red, c2.Position.X, c2.Position.Y, s2.Radius);
                        e.Graphics.DrawLine(CollisionCirecleCentersPen, c1.Position.X, c1.Position.Y, c2.Position.X, c2.Position.Y);
                        CollisionSystem.CalculateReaction(0, c1, c2);
                        float tx = c1.Position.X + c1.Direction.X;
                        float ty = c1.Position.Y + c1.Direction.Y;
                        e.Graphics.DrawLine(Pens.Black, c1.Position.X, c1.Position.Y, tx, ty);
                    }
                }
            }
        }
    }
}
