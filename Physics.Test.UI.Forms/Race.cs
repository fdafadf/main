using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class RaceInputModel
    {
        public bool TurningRight;
        public bool TurningLeft;
        public bool Accelerate;
        public bool Braking;
    }

    public class Triangle
    {
        public Vector3d A;
        public Vector3d B;
        public Vector3d C;

        public Triangle(Vector3d a, Vector3d b, Vector3d c)
        {
            A = a;
            B = b;
            C = c;
        }
    }

    public class Race : IScene2
    {
        public RaceInputModel Input { get; }
        public Sphere3d PlayerSphere { get; }
        public List<Sphere3d> Spheres { get; }
        public Triangle Ground { get; }
        public float Speed;
        public float RotationY;
        CollisionSystem collisionSystem;

        public Race(int numberOfSpheres)
        {
            Spheres = new List<Sphere3d>();
            PlayerSphere = new Sphere3d(new Vector3d(), 0.2f, new Vector3d());
            Speed = 0.1f;
            RotationY = 45;
            Input = new RaceInputModel();
            Random random = new Random();
            Spheres.Add(PlayerSphere);

            for (int i = 0; i < numberOfSpheres; i++)
            {
                float x = random.Next(100);
                float z = random.Next(100) - 50;
                float r = (float)random.NextDouble() * 2.0f + 0.2f;
                Spheres.Add(new Sphere3d(new Vector3d(x, 0, z), r, new Vector3d()));
            }

            Ground = new Triangle(new Vector3d(-100, -5, -50), new Vector3d(0, -5, 50), new Vector3d(100, -5, -50));
            collisionSystem = new CollisionSystem(Spheres, new Triangle[] { Ground });
        }

        public void Reset()
        {
        }

        public void UpdatePositions(float t)
        {
            collisionSystem.RecalculateCollisions(0);
            collisionSystem.RecalculateClosestCollision();
            Vector3d direction = PlayerSphere.Direction;
            float? newRotationY = collisionSystem.Update(RotationY, t);

            if (direction != PlayerSphere.Direction)
            {
                //Vector3d direction = new Vector3d();
                //direction.Set(s1.Direction);
                //direction.Normalize();
                //
                //CollisionForm.RotationY = rotationY;
                //CollisionForm.NewRotationY = 57.29577f * (float)Math.Atan2(direction.X, direction.Z);

                //direction = new Vector3d(PlayerSphere.Direction);
                //direction.Set(PlayerSphere.Direction);
                //direction.Normalize();
                float rotationY = 57.29577f * (float)Math.Atan2(PlayerSphere.Direction.Z, PlayerSphere.Direction.X);
                Console.WriteLine($"Update.Direction {direction.ToString("")}");
                Console.WriteLine($"Update.Reaction {PlayerSphere.Direction.ToString("")}");
                Console.WriteLine($"Update.Rotation {RotationY} -> {rotationY}");

                if (newRotationY.HasValue && newRotationY.Value != rotationY)
                {
                    //System.Diagnostics.Debugger.Break();
                }

                RotationY = rotationY;
                UpdateDirections();
            }
        }

        public void UpdateDirections()
        {
            float dx = (float)Math.Cos(RotationY / 57.29577f);
            float dz = (float)Math.Sin(RotationY / 57.29577f);
            PlayerSphere.Direction.X = dx * Speed;
            PlayerSphere.Direction.Z = dz * Speed;
        }

        public void UpdateInput()
        {
            if (Input.Accelerate)
            {
                Speed += 0.01f;
            }

            if (Input.Braking)
            {
                Speed -= 0.01f;
            }

            if (Input.TurningRight)
            {
                RotationY += 1;
            }

            if (Input.TurningLeft)
            {
                RotationY -= 1;
            }
        }
    }
}
