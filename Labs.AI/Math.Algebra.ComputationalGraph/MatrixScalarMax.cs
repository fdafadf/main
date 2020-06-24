namespace Math.Algebra.ComputationalGraph
{
    class MatrixScalarMax : MatrixScalarOperation
    {
        public MatrixScalarMax(Matrix matrix, Scalar scalar) : base(matrix, scalar)
        {
        }

        protected override void Evaluate()
        {
            var matrixValue = matrix.Value;
            var scalarValue = scalar.Value;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    value[i][j] = System.Math.Max(matrixValue[i][j], scalarValue);
                }
            }
        }
    }
}
