namespace WindowsFormsApp1
{
    class Helper
    {
        public static void ClosestPointOnLine(Sphere dynamicSphere, Sphere staticSphere, ref Vector d)
        {
            ClosestPointOnLine
            (
                dynamicSphere.Position.X,
                dynamicSphere.Position.Y,
                dynamicSphere.Position.X + dynamicSphere.Move.X,
                dynamicSphere.Position.Y + dynamicSphere.Move.Y,
                staticSphere.Position.X,
                staticSphere.Position.Y,
                ref d
            );
        }

        public static void ClosestPointOnLine(float lx1, float ly1, float lx2, float ly2, float x0, float y0, ref Vector d)
        {
            float A1 = (ly2 - ly1);
            float B1 = (lx1 - lx2);
            float C1 = (ly2 - ly1) * lx1 + (lx1 - lx2) * ly1;
            float C2 = -B1 * x0 + A1 * y0;
            float det = (A1 * A1 - (-B1 * B1));
            float cx = 0;
            float cy = 0;

            if (det != 0)
            {
                cx = (float)((A1 * C1 - B1 * C2) / det);
                cy = (float)((A1 * C2 - -B1 * C1) / det);
            }
            else
            {
                cx = x0;
                cy = y0;
            }

            d.X = cx;
            d.Y = cy;
            //return new Vector(cx, cy);
        }

        //public static double ClosestPossibleDistanceTo2(Sphere dynamicSphere, Sphere staticSphere)
        //{
        //
        //}
    }
}
