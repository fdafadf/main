using Labs.AI;

namespace Math.Algebra.ComputationalGraph
{
    class MatricesDotProduct : MatricesOperation
    {
        public readonly int I;

        public MatricesDotProduct(Matrix matrix1, Matrix matrix2) : base(matrix1, matrix2, matrix1.Rows, matrix2.Cols)
        {
            Assert.Equals(I = matrix1.Cols, matrix2.Rows);
        }

        protected override void Evaluate()
        {
            var matrix1Value = matrix1.Value;
            var matrix2Value = matrix2.Value;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    double v = 0;

                    for (int k = 0; k < I; k++)
                    {
                        v += matrix1Value[i][k] * matrix2Value[k][j];
                    }

                    value[i][j] = v;
                }
            }
        }
    }
}
