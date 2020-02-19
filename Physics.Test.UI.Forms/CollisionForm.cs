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
    public partial class CollisionForm : Form
    {
        public float RotationY;
        public float NewRotationY;
        public Vector3d Reaction;
        public bool Empty = true;
        private Sphere3d sphere1 = new Sphere3d();
        private Sphere3d sphere2 = new Sphere3d();

        public CollisionForm()
        {
            InitializeComponent();
        }

        public Sphere3d Sphere1
        {
            set
            {
                sphere1.Set(value);
            }
        }

        public Sphere3d Sphere2
        {
            set
            {
                sphere2.Set(value);
            }
        }

        Sphere3dPainter spherePainter = new Sphere3dPainter();

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Empty == false)
            {
                float scaleX = Width / (Math.Abs(sphere1.Position.X - sphere2.Position.X) + sphere1.Radius + sphere2.Radius + 20);
                float scaleY = Height / (Math.Abs(sphere1.Position.Z - sphere2.Position.Z) + sphere1.Radius + sphere2.Radius + 20);

                spherePainter.TranslateX = -(Math.Min(sphere1.Position.X - sphere1.Radius, sphere2.Position.X - sphere2.Radius) - 10);
                spherePainter.TranslateZ = -(Math.Min(sphere1.Position.Z - sphere1.Radius, sphere2.Position.Z - sphere2.Radius) - 10);
                spherePainter.Scale = Math.Min(scaleX, scaleY);
                spherePainter.Reaction = Reaction;
                spherePainter.Paint(e, Pens.Black, sphere1);
                spherePainter.Reaction = null;
                spherePainter.Paint(e, Pens.Black, sphere2);

                Text = $"{sphere1.Position.ToString("")} : {RotationY} -> {NewRotationY}";
                Console.WriteLine($"Form.Direction {sphere1.Direction.ToString("")}");
                Console.WriteLine($"Form.Reaction {Reaction?.ToString("") ?? ""}");
                Console.WriteLine($"Form.Rotation {RotationY} -> {NewRotationY}");
            }
        }

        private void CollisionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.Empty = true;
            }
        }
    }
}
