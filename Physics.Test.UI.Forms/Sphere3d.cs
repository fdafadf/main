using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    //public class RotatedSphere3d : Sphere3d
    //{
    //    public Vector3d Rotation;
    //    public Vector3d RotationDirection;
    //
    //    public RotatedSphere3d(Vector3d position, float radius, Vector3d direction) : base(position, radius, direction)
    //    {
    //    }
    //
    //    public override void Move(float t)
    //    {
    //        base.Move(t);
    //
    //        Rotation.X += 0.1f;
    //    }
    //}

    public class Box3d
    {
        public Vector3d Position;
        public float Width;
        public float Height;
    }

    public class Sphere3d
    {
        public Vector3d Position;
        public float Radius;
        public Vector3d Direction;

        public Sphere3d()
        {
            Position = new Vector3d();
            Direction = new Vector3d();
        }

        public Sphere3d(Vector3d position, float radius, Vector3d direction)
        {
            Position = position;
            Radius = radius;
            Direction = direction;
        }

        public void Set(Sphere3d s)
        {
            Position.Set(s.Position);
            Direction.Set(s.Direction);
            Radius = s.Radius;
        }

        public virtual void Move(float t)
        {
            Position.X += Direction.X * t;
            Position.Y += Direction.Y * t;
            Position.Z += Direction.Z * t;
        }

        public virtual void Move(float t, ref Sphere3d s)
        {
            s.Position.X = Position.X + Direction.X * t;
            s.Position.Y = Position.Y + Direction.Y * t;
            s.Position.Z = Position.Z + Direction.Z * t;
            s.Direction.X = Direction.X;
            s.Direction.Y = Direction.Y;
            s.Direction.Z = Direction.Z;
        }

        public bool IsColliding(Sphere3d s)
        {
            double d = Position.Distance(s.Position);
            return d < Radius + s.Radius; 
        }

        //public void RotateY(float angle)
        //{
        //    //RotationY += angle;
        //    Direction.Mul(Matrix3d.RotationY(angle));
        //    //float dx = -(float)Math.Sin(RotationY);
        //    //float dz = (float)Math.Cos(RotationY);
        //    //Direction.X = dx * length;
        //    //Direction.Z = dz * length;
        //}

        //public void RecalculateDirection(float length)
        //{
        //    float dx = -(float)Math.Sin(RotationY);
        //    float dz = (float)Math.Cos(RotationY);
        //    Direction.X = dx * length;
        //    Direction.Z = dz * length;
        //}
    }
}
