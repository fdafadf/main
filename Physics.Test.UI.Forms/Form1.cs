using Basics.Physics.Test.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        LinkedList<ScenePainter> scenes = new LinkedList<ScenePainter>();
        //IScene[] scenes;

        public Form1()
        {
            InitializeComponent();
            //DoubleBuffered = true;
            //scenes.AddLast();
            //scenes.AddLast(new FormScene(new DynamicCase()));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            (timer1.Tag as Control).Refresh();
            //scene.Update(1);
            //Refresh();
        }

        float zeroX;
        float zeroY;
        float vectorX;
        float vectorY;

        public static TestModel CreateTestModel2()
        {
            TestModel result = new TestModel();
            float size = 5;

            PointF a = new PointF(-size, 0);
            PointF b = new PointF(size, 0);
            Vector3d delta = new Vector3d(0, 0, size);
            Matrix3d matrix = Matrix3d.RotationY(6 / 57.29577f);
            result.Vertices.Add(new Vector3d(a.X, 0, a.Y));
            result.Vertices.Add(new Vector3d(b.X, 0, b.Y));

            for (uint i = 0; i < 160; i++)
            {
                delta.Mul(matrix);

                PointF d = new PointF(b.X + delta.X, b.Y + delta.Z);
                PointF dv = new PointF(delta.Z, -delta.X);
                dv = dv.Normalized();
                dv.X *= size * 2;
                dv.Y *= size * 2;
                PointF c = new PointF(d.X - dv.X, d.Y - dv.Y);

                result.Vertices.Add(new Vector3d(c.X, -0.2f, c.Y));
                result.Vertices.Add(new Vector3d(d.X, -0.2f, d.Y));

                result.Indices.Add(0 + i * 2);
                result.Indices.Add(1 + i * 2);
                result.Indices.Add(3 + i * 2);
                result.Indices.Add(0 + i * 2);
                result.Indices.Add(2 + i * 2);
                result.Indices.Add(3 + i * 2);

                a = c;
                b = d;
            }

            return result;
        }

        public static TestModel CreateTestModel()
        {
            TestModel result = new TestModel();

            PointF a = new PointF(-10, 0);
            PointF b = new PointF(10, 0);
            Vector3d delta = new Vector3d(0, 0, 10);
            Matrix3d matrix = Matrix3d.RotationY(5 / 57.29577f);
            result.Vertices.Add(new Vector3d(a.X, 0, a.Y));
            result.Vertices.Add(new Vector3d(b.X, 0, b.Y));

            for (uint i = 0; i < 80; i++)
            {
                delta.Mul(matrix);

                PointF d = new PointF(b.X + delta.X, b.Y + delta.Z);
                PointF dp = new PointF(d.X, d.Y);
                PointF dv = new PointF(d.X + delta.Z, d.Y - delta.X);
                PointF ap = new PointF(a.X, a.Y);
                PointF av = new PointF(a.X + delta.X, a.Y + delta.Z);
                PointF p = MathHelper2.Intersection(dp, dv, ap, av);
                PointF c = new PointF(p.X, p.Y);

                result.Vertices.Add(new Vector3d(c.X, 0, c.Y));
                result.Vertices.Add(new Vector3d(d.X, 0, d.Y));

                result.Indices.Add(2 + i * 2);
                result.Indices.Add(0 + i * 2);
                result.Indices.Add(1 + i * 2);
                result.Indices.Add(2 + i * 2);
                result.Indices.Add(1 + i * 2);
                result.Indices.Add(3 + i * 2);

                a = c;
                b = d;
            }

            //float rotationY = (float)Math.Atan2(delta.Z, delta.X); // 57.29577f * 
            //PointF c = new PointF(-100 + delta.X, 100 + delta.Z);

            //PointF[] points = new PointF[] { a, b, c, d };

            //for (int i = 0; i < points.Length; i++)
            //{
            //    points[i].X += 400;
            //    points[i].Y += 310;
            //}
            //
            //for (int i = 0; i < points.Length; i++)
            //{
            //    result.Vertices.Add(new Vector3d(points[i].X, 0, points[i].Y));
            //}
            //
            //result.Indices.Add(2 + 0 * 2);
            //result.Indices.Add(0 + 0 * 2);
            //result.Indices.Add(1 + 0 * 2);
            //result.Indices.Add(2 + 0 * 2);
            //result.Indices.Add(1 + 0 * 2);
            //result.Indices.Add(3 + 0 * 2);
            return result;
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //
        //    #region Track
        //
        //    TestModel model = CreateTestModel2();
        //
        //    for (int i = 0; i < model.Vertices.Count; i++)
        //    {
        //        model.Vertices[i].X += 400;
        //        model.Vertices[i].Z += 310;
        //    }
        //
        //    PointF[] triangle = new PointF[3];
        //
        //    for (int i = 0; i < model.Indices.Count; i += 3)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            triangle[j].X = model.Vertices[(int)model.Indices[i + j]].X;
        //            triangle[j].Y = model.Vertices[(int)model.Indices[i + j]].Z;
        //        }
        //
        //        e.Graphics.FillPolygon(Brushes.DarkSalmon, triangle);
        //    }
        //
        //    #endregion
        //    #region Track 2
        //
        //    /*
        //    PointF a = new PointF(-100, 0);
        //    PointF b = new PointF(100, 0);
        //    Vector3d delta = new Vector3d(0, 0, 100);
        //    //float rotationY = (float)Math.Atan2(delta.Z, delta.X); // 57.29577f * 
        //    Matrix3d matrix = Matrix3d.RotationY(5 / 57.29577f);
        //    delta.Mul(matrix);
        //    //PointF c = new PointF(-100 + delta.X, 100 + delta.Z);
        //    PointF d = new PointF(b.X + delta.X, b.Y + delta.Z);
        //    PointF dp = new PointF(d.X, d.Y);
        //    PointF dv = new PointF(d.X + delta.Z, d.Y - delta.X);
        //    PointF ap = new PointF(a.X, a.Y);
        //    PointF av = new PointF(a.X + delta.X, a.Y + delta.Z);
        //    PointF p = MathHelper2.Intersection(dp, dv, ap, av);
        //    PointF c = new PointF(p.X, p.Y);
        //
        //    PointF[] points = new PointF[] { a, b, d, c };
        //
        //    for (int i = 0; i < points.Length; i++)
        //    {
        //        points[i].X += 400;
        //        points[i].Y += 310;
        //    }
        //
        //    e.Graphics.FillPolygon(Brushes.DarkSalmon, points);
        //    e.Graphics.DrawLine(Pens.Red, ap.X + 400, ap.Y + 310, av.X + 400, av.Y + 310);
        //    e.Graphics.DrawLine(Pens.Red, dp.X + 400, dp.Y + 310, dv.X + 400, dv.Y + 310);
        //    */
        //
        //    #endregion
        //
        //    //e.Graphics.DrawEllipse(Pens.Black, 20, 20, 10, 10);
        //    //e.Graphics.DrawRectangle(Pens.Black, 10, 10, 10, 10);
        //    //spheres.ForEach(s => s.Paint(e));
        //
        //    scenes.First().PaintDetails(e);
        //
        //    zeroX = Width / 2;
        //    zeroY = Height / 2;
        //    e.Graphics.DrawLine(Pens.Gray, zeroX, 0, zeroX, Height);
        //    e.Graphics.DrawLine(Pens.Gray, 0, zeroY, Width, zeroY);
        //    e.Graphics.DrawLine(Pens.DarkOliveGreen, zeroX, zeroY, zeroX + vectorX, zeroY + vectorY);
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    var last = scenes.Last;
                    scenes.RemoveLast();
                    scenes.AddFirst(last);
                    //scene = scenes[0];
                    Refresh();
                    break;
                case Keys.F2:
                    //scene = scenes[1];
                    Refresh();
                    break;
                case Keys.P:
                    if (timer1.Enabled)
                        timer1.Stop();
                    else
                        timer1.Start();
                    break;
                case Keys.Space:
                    scenes.First().Update(1);
                    Refresh();
                    break;
                case Keys.Enter:
                    //scenes.First().Reset();
                    Refresh();
                    break;
                default:
                    //scene.KeyDown(e.KeyCode);
                    scenes.First().KeyDown(e);
                    Refresh();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Tag = simulationSceneControl1;
            timer1.Enabled = timer1.Enabled == false;
            //this.timer1.Start();
        }

        //private void Form1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (scenes.First().MouseMove(e))
        //    {
        //        Refresh();
        //    }
        //
        //    float cx = e.X - zeroX;
        //    float cy = e.Y - zeroY;
        //    float angle = 57.29577f * (float)Math.Atan2(cy, cx);
        //
        //    Text = $"{angle}";
        //
        //    vectorX = (float)Math.Cos(angle / 57.29577f) * 50;
        //    vectorY = (float)Math.Sin(angle / 57.29577f) * 50;
        //    //PlayerSphere.Direction.X = dx * Speed;
        //    //PlayerSphere.Direction.Z = dz * Speed;
        //    Refresh();
        //}
    }
}
