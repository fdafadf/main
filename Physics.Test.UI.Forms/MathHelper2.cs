using System.Drawing;

namespace Basics.Physics.Test.UI
{
    public class MathHelper2
    {
        public static PointF Intersection(PointF A, PointF B, PointF C, PointF D)
        {
            // Line AB represented as a1x + b1y = c1  
            float a1 = B.Y - A.Y;
            float b1 = A.X - B.X;
            float c1 = a1 * (A.X) + b1 * (A.Y);

            // Line CD represented as a2x + b2y = c2  
            float a2 = D.Y - C.Y;
            float b2 = C.X - D.X;
            float c2 = a2 * (C.X) + b2 * (C.Y);

            float determinant = a1 * b2 - a2 * b1;

            if (determinant == 0)
            {
                // The lines are parallel. This is simplified  
                // by returning a pair of FLT_MAX  
                return new PointF(float.MaxValue, float.MaxValue);
            }
            else
            {
                float x = (b2 * c1 - b1 * c2) / determinant;
                float y = (a1 * c2 - a2 * c1) / determinant;
                return new PointF(x, y);
            }
        }
    }
}
