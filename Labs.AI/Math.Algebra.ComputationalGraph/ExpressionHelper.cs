using System;
using System.Linq;

namespace Math.Algebra.ComputationalGraph
{
    static class ExpressionHelper
    {
        public static VectorSum Sum(Vector v) => v.Sum;
        public static double Max(double v1, double v2) => System.Math.Max(v1, v2);
        public static double Abs(double v) => System.Math.Abs(v);
        public static void Print(string text) => Console.WriteLine(text);
        public static Matrix Matrix(int rows, int cols) => new Matrix(rows, cols);
        public static MatrixSum Sum(Matrix v) => v.Sum;
        public static MatrixScalarMax Max(Matrix m, double c) => new MatrixScalarMax(m, new Scalar(c));
        public static MatricesMultiplication Square(Matrix m) => new MatricesMultiplication(m, m);

        public static Matrix StandardDistribution(this Random self, int rows, int cols)
        {
            return self.Normal(Matrix(rows, cols));
        }

        public static Matrix Normal(this Random self, Matrix matrix)
        {
            var matrixValue = matrix.Value;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    matrixValue[i][j] = self.NextStandard();
                }
            }

            return matrix;
        }

        public static double NextStandard(this Random self)
        {
            return self.NextNormal(0, 1);
        }

        public static double NextNormal(this Random self, double mean, double stddev)
        {
            // The method requires sampling from a uniform random of (0,1]
            // but Random.NextDouble() returns a sample of [0,1).
            double x1 = 1 - self.NextDouble();
            double x2 = 1 - self.NextDouble();
            double y1 = System.Math.Sqrt(-2.0 * System.Math.Log(x1)) * System.Math.Cos(2.0 * System.Math.PI * x2);
            return y1 * stddev + mean;
        }

        public static void GetValues(this Matrix[] self, double[][][] buffer)
        {
            for (int i = 0; i < self.Length; i++)
            {
                buffer[i] = self[i].Value;
            }
        }

        public static void Subtract(this Matrix[] self, double[][][] values)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i].Subtract(values[i]);
            }
        }

        public static int NaN(Matrix[] self)
        {
            return self.Select(m => NaN(m.Value)).Sum();
        }

        public static int NaN(double[][] self)
        {
            return self.Select(a => a.Count(v => double.IsNaN(v))).Count();
        }
    }
}
