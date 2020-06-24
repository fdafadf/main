using Labs.AI;

namespace Math.Algebra.ComputationalGraph
{
    class MatricesMultiplication : MatricesOperation
    {
        public MatricesMultiplication(Matrix matrix1, Matrix matrix2) : base(matrix1, matrix2, matrix1.Rows, matrix1.Cols)
        {
            Assert.Equals(matrix1.Rows, matrix2.Rows);
            Assert.Equals(matrix1.Cols, matrix2.Cols);
        }

        protected override void Evaluate()
        {
            var matrix1Value = matrix1.Value;
            var matrix2Value = matrix2.Value;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    value[i][j] = matrix1Value[i][j] * matrix2Value[i][j];
                }
            }
        }
    }
}
