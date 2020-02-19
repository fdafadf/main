using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class SimulationScene : IScene2
    {
        public List<Sphere3d> Objects { get; }
        private float?[,] collisions;

        public SimulationScene(int sphereCount, int width, int height)
        {
            Objects = new List<Sphere3d>();
            Random random = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < sphereCount; i++)
            {
                float x = 10 + random.Next(width - 20);
                float y = 10 + random.Next(height - 20);
                float r = 5 + random.Next(20);
                float dx = 1 + random.Next(5);
                float dy = 1 + random.Next(5);
                Objects.Add(new Sphere3d(new Vector3d(x, y, 0), r, new Vector3d(dx, dy, 0)));
            }

            collisions = new float?[sphereCount, sphereCount];
            CalculateCollision();
            //FindClosest();
        }

        private void CalculateCollision()
        {
            for (int i = 0; i < Objects.Count - 1; i++)
            {
                for (int j = i + 1; j < Objects.Count; j++)
                {
                    collisions[i, j] = DynamicCase.CalculateCollision(Objects[i], Objects[j]);
                }
            }
        }

        private void CalculateCollision(int sphereIndex)
        {
            for (int i = 0; i < Objects.Count - 1; i++)
            {
                if (i != sphereIndex)
                {
                    int a = Math.Min(i, sphereIndex);
                    int b = Math.Max(i, sphereIndex);
                    collisions[a, b] = DynamicCase.CalculateCollision(Objects[a], Objects[b]);
                }
            }
        }

        private void SubstractCollisionTime(float t)
        {
            for (int i = 0; i < Objects.Count - 1; i++)
            {
                for (int j = i + 1; j < Objects.Count; j++)
                {
                    if (collisions[i, j].HasValue)
                    {
                        collisions[i, j] -= t;
                    }
                }
            }
        }

               
        public void Reset()
        {
        }

        public void UpdateInput()
        {
        }
    }
}
