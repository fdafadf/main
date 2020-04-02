using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Short.Cube
{
    public partial class CubeForm : Form
    {
        Matrix4x4 p = new Matrix4x4();
        Vector3[] v;
        int[] m;

        public CubeForm()
        {
            InitializeComponent();

            float zNear = 1;
            float zFar = 10;
            float aspectRatio = 1;
            float theta = ((float)Math.PI / 180.0f) * 90.0f;
            float fieldOfView = 1.0f / (float)Math.Tan(theta / 2.0);
            float zNormalisation = zFar / (zFar - zNear);
            p.M11 = aspectRatio * fieldOfView;
            p.M22 = fieldOfView;
            p.M33 = zNormalisation;
            p.M43 = -zNormalisation * zNear;
            p.M34 = 1;
            //p.M41 = -1;
            p = Matrix4x4.Multiply(p, Matrix4x4.CreateScale(300, 300, 300));
            v = new [] { new Vector3(0, 1, 2), new Vector3(1, 1, 2), new Vector3(0, 0, 2), new Vector3(1, 0, 2), new Vector3(0, 1, 3), new Vector3(1, 1, 3), new Vector3(0, 0, 3), new Vector3(1, 0, 3) };
            m = new [] { 0, 1, 2, 1, 3, 2, 4, 5, 6, 5, 7, 6 };

            timer1.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            for (int i = 0; i < m.Length; i += 3)
            {
                e.Graphics.DrawTriangle(Vector3.Transform(v[m[i]], p), Vector3.Transform(v[m[i + 1]], p), Vector3.Transform(v[m[i + 2]], p));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //p = Matrix4x4.Multiply(p, Matrix4x4.CreateRotationY(0.1f));
            Refresh();
        }
    }

    public static class Extensions
    {
        public static void DrawTriangle(this Graphics self, Vector3 a, Vector3 b, Vector3 c)
        {
            self.DrawLine(Pens.Black, a.X, a.Y, b.X, b.Y);
            self.DrawLine(Pens.Black, b.X, b.Y, c.X, c.Y);
            self.DrawLine(Pens.Black, c.X, c.Y, a.X, a.Y);
        }
    }
}
