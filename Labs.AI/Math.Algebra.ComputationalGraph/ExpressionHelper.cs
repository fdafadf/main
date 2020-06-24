using System;

namespace Math.Algebra.ComputationalGraph
{
    class ExpressionHelper
    {
        public static VectorSum Sum(Vector v) => v.Sum;
        public static double Max(double v1, double v2) => System.Math.Max(v1, v2);
        public static double Abs(double v) => System.Math.Abs(v);
        public static void Print(string text) => Console.WriteLine(text);
        public static MatrixSum Sum(Matrix v) => v.Sum;
        public static MatrixScalarMax Max(Matrix m, double c) => new MatrixScalarMax(m, new Scalar(c));
        public static MatricesMultiplication Square(Matrix m) => new MatricesMultiplication(m, m);
    }
}
