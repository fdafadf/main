using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class DynamicCase : IScene2
    {
        public List<Sphere3d> Spheres { get; }

        public DynamicCase()
        {
            Spheres = new List<Sphere3d>();
            Spheres.Add(new Sphere3d(new Vector3d(150, 150, 0), 70, new Vector3d(10, 12, 0)));
            Spheres.Add(new Sphere3d(new Vector3d(220, 310, 0), 30, new Vector3d(-2, -6, 0)));
            Spheres.Add(new Sphere3d(new Vector3d(290, 70, 0), 50, new Vector3d(2, 4, 0)));
            
            foreach (var sphere in Spheres)
            {
                sphere.Direction.Mul(10);
            }
        }



        public static float? CalculateCollision(Sphere3d s1, Sphere3d s2)
        {
            return null;
            //Vector3d.Sub(s1.Position, s2.Position, ref vecs);
            //Vector3d.Sub(s1.Direction, s2.Direction, ref vecv);
            //float radiusSum = s1.Radius + s2.Radius;
            //float c = vecs.Dot(vecs) - radiusSum * radiusSum;
            //float t = 0;
            //
            //if (c < 0)
            //{
            //    return 0;
            //}
            //else
            //{
            //    float a = vecv.Dot(vecv);
            //    float b = vecv.Dot(vecs);
            //
            //    if (b >= 0)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        float d = b * b - a * c;
            //
            //        if (d < 0)
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            t = (float)(-b - Math.Sqrt(d)) / a;
            //            return t;
            //        }
            //    }
            //}
        }

        public void UpdateInput()
        {
        }
    }
}
