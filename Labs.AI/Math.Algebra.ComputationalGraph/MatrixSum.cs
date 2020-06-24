namespace Math.Algebra.ComputationalGraph
{
    class MatrixSum : Scalar
    {
        Matrix matrix;

        public MatrixSum(Matrix matrix) : base(0)
        {
            this.matrix = matrix;
            this.matrix.References.Add(this);
            this.IsEvaluated = false;
        }

        protected override void Evaluate()
        {
            var matrixValue = matrix.Value;
            value = 0;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Cols; j++)
                {
                    value += matrixValue[i][j];
                }
            }
        }
    }
}
