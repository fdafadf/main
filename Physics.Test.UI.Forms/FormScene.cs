using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class ScenePainter
    {
        IScene2 Scene;
        public List<IPaintable> Objects = new List<IPaintable>();
        Func<MouseEventArgs, bool> OnMouseMove;
        Action<KeyEventArgs> OnKeyDown;

        public ScenePainter(SimulationScene scene)
        {
            Scene = scene;

            foreach (Sphere3d @object in scene.Objects)
            {
                Objects.Add(new PaintableSphere3d(@object, Pens.Black));
            }
        }

        public ScenePainter(DynamicCase scene)
        {
            Scene = scene;
            Pen spherePen = new Pen(Brushes.DarkGray, 2);
            Pen selectedSpherePen = new Pen(Brushes.Black, 2);

            foreach (Sphere3d @object in scene.Spheres)
            {
                PaintableSphere3d paintableSphere = new PaintableSphere3d(@object, spherePen);
                Objects.Add(paintableSphere);
            }

            Objects.Add(new CollisionPainter(scene.Spheres));
            PaintableSphere3d selectedSphere = null;

            OnKeyDown = (KeyEventArgs keys) =>
            {
            };

            Vector3d mousePosition = new Vector3d();

            OnMouseMove = (MouseEventArgs e) =>
            {

                return false;
            };
        }

        public void Update(float t)
        {
            Scene.UpdateInput();
        }

        public void KeyDown(KeyEventArgs e)
        {
            OnKeyDown?.Invoke(e);
        }

        public void Paint(PaintEventArgs e)
        {
        }

        public void PaintDetails(PaintEventArgs e)
        {
            foreach (IPaintable @object in Objects)
            {
                @object.Paint(e);
            }

            //if (closestTime != float.MaxValue)
            //{
            //    var p1 = Spheres[closest.X].Position;
            //    var p2 = Spheres[closest.Y].Position;
            //    e.Graphics.DrawLine(Pens.Red, p1.X, p1.Y, p2.X, p2.Y);
            //}

            // 2

            //foreach (Sphere3d sphere in Spheres)
            //{
            //    Pen pen = sphere == selectedSphere ? selectedSpherePen : spherePen;
            //    sphere.Paint(e, pen);
            //}
            //
        }

        public bool MouseMove(MouseEventArgs e)
        {
            return OnMouseMove?.Invoke(e) ?? false;
        }
    }
}
