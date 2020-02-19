using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics.Physics.Test.UI
{
    static class Extensions
    {
        public static Vector3 ToVector3(this WindowsFormsApp1.Vector3d v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }
    }
}
