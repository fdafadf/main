using System;

namespace Math.Algebra.ComputationalGraph
{
    class Matrix : Expression
    {
        protected double[][] value;
        public readonly int Rows;
        public readonly int Cols;

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            value = new double[Rows][];

            for (int i = 0; i < Rows; i++)
            {
                value[i] = new double[Cols];
            }
        }

        public Matrix T
        {
            get
            {
                return new MatrixTransposition(this);
            }
        }

        public MatrixSum Sum
        {
            get
            {
                return new MatrixSum(this);
            }
        }

        public double[][] Value
        {
            get
            {
                if (IsEvaluated == false)
                {
                    Evaluate();
                    IsEvaluated = true;
                }

                return value;
            }
        }

        protected virtual void Evaluate()
        {
        }

        public void Subtract(double[][] m)
        {
            MatrixOperations.Subtract(value, m, @out: value, Rows, Cols);
            IsEvaluated = false;
        }

        public static MatricesDotProduct operator *(Matrix m1, Matrix m2) => new MatricesDotProduct(m1, m2);
        public static MatrixScalarMultiplication operator *(Matrix m, double c) => new MatrixScalarMultiplication(m, new Scalar(c));
        public static MatrixScalarMultiplication operator *(double c, Matrix m) => new MatrixScalarMultiplication(m, new Scalar(c));
        public static MatricesSubtraction operator -(Matrix m1, Matrix m2) => new MatricesSubtraction(m1, m2);
    }
}
