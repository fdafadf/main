using System;

namespace WindowsFormsApp1
{
    public class Matrix3d
    {
        public static Matrix3d RotationY(float angle)
        {
            // | cos  0  sin |
            // |  0   1   0  |
            // | -sin 0  cos |

            Matrix3d result = new Matrix3d();
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            result.Values[0, 0] = cos;
            result.Values[2, 0] = sin;
            result.Values[1, 1] = 1;
            result.Values[0, 2] = -sin;
            result.Values[2, 2] = cos;
            return result;
        }

        public float[,] Values = new float[3, 3];
    }
}
