namespace Math.Algebra.ComputationalGraph
{
    class MatrixOperations
    {
        public static void Add(double[][] a, double[][] b, double[][] @out, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    @out[i][j] = a[i][j] + b[i][j];
                }
            }
        }

        public static void Subtract(double[][] a, double[][] b, double[][] @out, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    @out[i][j] = a[i][j] - b[i][j];
                }
            }
        }
    }
}
