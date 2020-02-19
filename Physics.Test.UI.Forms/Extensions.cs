using System;
using System.Drawing;

namespace WindowsFormsApp1
{
    static class Extensions
    {
        public static PointF Normalized(this PointF self)
        {
            float distance = (float)Math.Sqrt(self.X * self.X + self.Y * self.Y);
            return new PointF(self.X / distance, self.Y / distance);
        }

        private static Vector from = new Vector();
        private static Vector to = new Vector();
        private static Vector arrow = new Vector();
        private static Vector arrowOrtho = new Vector();

        public static void DrawArrow(this Graphics self, Pen pen, Vector from, Vector to)
        {
            self.DrawLine(Pens.Blue, from, to);
            Vector.Sub(to, from, ref arrow);
            Vector.Normalize(arrow, ref arrow);
            arrow.Mul(5);
            arrowOrtho.Set(-arrow.Y, arrow.X);
            Vector.Sum(to, arrowOrtho, ref arrowOrtho);
            arrowOrtho.Sub(arrow);
            self.DrawLine(pen, to, arrowOrtho);
            arrowOrtho.Set(arrow.Y, -arrow.X);
            Vector.Sum(to, arrowOrtho, ref arrowOrtho);
            arrowOrtho.Sub(arrow);
            self.DrawLine(pen, to, arrowOrtho);
        }

        public static void DrawArrow(this Graphics self, Pen pen, float fromX, float fromY, float toX, float toY)
        {
            from.Set(fromX, fromY);
            to.Set(toX, toY);
            self.DrawArrow(pen, from, to);
        }

        public static void FillCircle(this Graphics self, Brush brush, Vector position, float radius)
        {
            float doubleRadius = radius * 2;
            self.FillEllipse(brush, position.X - radius, position.Y - radius, doubleRadius, doubleRadius);
        }

        public static void DrawCircle(this Graphics self, Pen pen, Vector position, float radius)
        {
            float doubleRadius = radius * 2;
            self.DrawEllipse(pen, position.X - radius, position.Y - radius, doubleRadius, doubleRadius);
        }

        public static void DrawCircle(this Graphics self, Pen pen, float x, float y, float radius)
        {
            float doubleRadius = radius * 2;
            self.DrawEllipse(pen, x - radius, y - radius, doubleRadius, doubleRadius);
        }

        public static void DrawLine(this Graphics self, Pen pen, Vector a, Vector b)
        {
            self.DrawLine(pen, a.X, a.Y, b.X, b.Y);
        }

        public static void FillCircle(this Graphics self, Brush brush, float x, float y, float radius)
        {
            float doubleRadius = radius * 2;
            self.FillEllipse(brush, x - radius, y - radius, doubleRadius, doubleRadius);
        }
    }
}
