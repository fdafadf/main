using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class PaintableSphere3d : Sphere3dPainter, IPaintable
    {
        public Sphere3d Sphere;
        public Pen Pen;
        public Brush Brush;

        public PaintableSphere3d(Sphere3d sphere, Pen pen)
        {
            Sphere = sphere;
            Pen = pen;
        }

        public PaintableSphere3d(Sphere3d sphere, Brush brush)
        {
            Sphere = sphere;
            Brush = brush;
        }

        public void Paint(PaintEventArgs e)
        {
            //this.Paint(e, Pen, Sphere);
            float doubleRadius = Sphere.Radius * 2;

            if (Brush != null)
            {
                e.Graphics.FillCircle(Brush, Sphere.Position.X, Sphere.Position.Y, Sphere.Radius);
                //e.Graphics.DrawArrow(Brush, Sphere.Position.X, Sphere.Position.Y, Sphere.Position.X + Sphere.Direction.X, Sphere.Position.Y + Sphere.Direction.Y);
            }

            if (Pen != null)
            {
                e.Graphics.DrawCircle(Pen, Sphere.Position.X, Sphere.Position.Y, Sphere.Radius);
                e.Graphics.DrawArrow(Pen, Sphere.Position.X, Sphere.Position.Y, Sphere.Position.X + Sphere.Direction.X, Sphere.Position.Y + Sphere.Direction.Y);
            }
        }
    }

    public class Sphere3dPainter
    {
        public float TranslateX;
        public float TranslateZ;
        public float Scale;
        public Vector3d Reaction;

        public void Paint(PaintEventArgs e, Pen pen, Sphere3d sphere)
        {
            float doubleRadius = sphere.Radius * 2;
            float sx = (sphere.Position.X + TranslateX) * Scale;
            float sz = (sphere.Position.Z + TranslateZ) * Scale;
            e.Graphics.DrawCircle(pen, sx, sz, sphere.Radius * Scale);
            e.Graphics.DrawArrow(pen, sx, sz, sx + sphere.Direction.X * Scale, sz + sphere.Direction.Z * Scale);

            if (Reaction != null)
            {
                e.Graphics.DrawArrow(Pens.Red, sx, sz, sx + Reaction.X * Scale, sz + Reaction.Z * Scale);
            }
        }
    }
}
