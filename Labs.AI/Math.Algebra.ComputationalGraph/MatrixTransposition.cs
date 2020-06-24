namespace Math.Algebra.ComputationalGraph
{
    class MatrixTransposition : MatrixOperation
    {
        public MatrixTransposition(Matrix matrix) : base(matrix)
        {
        }

        protected override void Evaluate()
        {
            var matrixValue = matrix.Value;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    value[i][j] = matrixValue[j][i];
                }
            }
        }
    }
}
