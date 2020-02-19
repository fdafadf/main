using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Sphere
    {
        public float Radius = 10;
        public float Mass = 10;
        public Vector Position = new Vector();
        public Vector Move = new Vector();

        public Sphere()
        {

        }

        public void Update()
        {
            Position.Add(Move);
        }

        public void Paint(PaintEventArgs e)
        {
            float doubleRadius = Radius * 2;
            e.Graphics.DrawEllipse(Pens.Black, Position.X - Radius, Position.Y - Radius, doubleRadius, doubleRadius);
        }

        private Vector newPosition = new Vector();

        public void PaintDetails(PaintEventArgs e)
        {
            e.Graphics.DrawCircle(Pens.Black, Position, Radius);

            if (Move.IsZero == false)
            {
                Vector.Sum(Position, Move, ref newPosition);
                e.Graphics.DrawArrow(Pens.Blue, Position, newPosition);
                e.Graphics.DrawCircle(Pens.Gray, newPosition, Radius);
            }
        }
    }
}
