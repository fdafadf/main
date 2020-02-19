using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class PathfinderSceneControl : UserControl
    {
        PathfinderScene scene;
        List<IPaintable> paintables = new List<IPaintable>();

        public PathfinderSceneControl()
        {
            InitializeComponent();
            scene = new PathfinderScene(50, 600, 400, new Random(0));
            paintables.Add(new PaintableSphere3d(scene.Vehicle.Sphere, Pens.BlueViolet));
            paintables.Add(new PaintableSphere3d(scene.Checkpoint, Brushes.GreenYellow));
            paintables.AddRange(scene.Obstacles.Select(o => new PaintableSphere3d(o, Brushes.Chocolate)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (IPaintable paintable in paintables)
            {
                paintable.Paint(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    scene.Vehicle.Control.Accelerate = true;
                    break;
                case Keys.S:
                    scene.Vehicle.Control.Braking = true;
                    break;
                case Keys.Space:
                    timer1.Enabled = timer1.Enabled == false;
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    scene.Vehicle.Control.Accelerate = false;
                    break;
                case Keys.S:
                    scene.Vehicle.Control.Braking = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scene.Update();
            Refresh();
        }
    }

    public class VehicleControl
    {
        public bool TurningRight;
        public bool TurningLeft;
        public bool Accelerate;
        public bool Braking;
    }

    public class Sphere3dVehicle
    {
        public Sphere3d Sphere;
        public VehicleControl Control = new VehicleControl();
        float Speed;
        float RotationY;

        public Sphere3dVehicle(Sphere3d sphere)
        {
            Sphere = sphere;
        }

        public void Update()
        {
            if (Control.Accelerate)
            {
                Speed += 0.01f;
            }

            if (Control.Braking)
            {
                Speed -= 0.01f;
            }

            if (Control.TurningRight)
            {
                RotationY += 1;
            }

            if (Control.TurningLeft)
            {
                RotationY -= 1;
            }

            float dx = (float)Math.Cos(RotationY / 57.29577f);
            float dz = (float)Math.Sin(RotationY / 57.29577f);
            Sphere.Direction.X = dx * Speed;
            Sphere.Direction.Z = dz * Speed;
        }
    }

    public class PathfinderScene : IScene2
    {
        public Sphere3dVehicle Vehicle;
        public List<Sphere3d> Obstacles;
        public Sphere3d Checkpoint;
        CollisionSystem CollisionSystem;

        public PathfinderScene(int sphereCount, int width, int height, Random random)
        {
            Obstacles = new List<Sphere3d>();

            for (int i = 0; i < sphereCount; i++)
            {
                Obstacles.Add(RandomSphere(random, width, height, dynamic: false));
            }

            Checkpoint = RandomNonCollidingSphere(random, width, height, Obstacles, dynamic: false);
            Vehicle = new Sphere3dVehicle(RandomNonCollidingSphere(random, width, height, Obstacles, dynamic: true));
            List<Sphere3d> allSpheres = new List<Sphere3d>();
            allSpheres.AddRange(Obstacles);
            allSpheres.Add(Checkpoint);
            allSpheres.Add(Vehicle.Sphere);
            CollisionSystem = new CollisionSystem(allSpheres, new Triangle[] { });
        }

        public void Update()
        {
            Vehicle.Update();
            CollisionSystem.Update(0, 1);
        }

        public void UpdateInput()
        {
        }

        private static Sphere3d RandomSphere(Random random, int width, int height, bool dynamic)
        {
            int r = 5 + random.Next(20);
            float x = r + random.Next(width - r * 2);
            float y = r + random.Next(height - r * 2);

            if (dynamic)
            {
                float dx = 1 + random.Next(5);
                float dy = 1 + random.Next(5);
                return new Sphere3d(new Vector3d(x, y, 0), r, new Vector3d(dx, dy, 0));
            }
            else
            {
                return new Sphere3d(new Vector3d(x, y, 0), r, new Vector3d(0, 0, 0));
            }
        }

        private static Sphere3d RandomNonCollidingSphere(Random random, int width, int height, IEnumerable<Sphere3d> spheres, bool dynamic)
        {
            Sphere3d result;

            do
            {
                result = RandomSphere(random, width, height, dynamic);
            }
            while (spheres.Any(sphere => result.IsColliding(sphere)));

            return result;
        }
    }
}
