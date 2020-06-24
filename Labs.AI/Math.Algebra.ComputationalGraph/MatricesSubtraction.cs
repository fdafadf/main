namespace Math.Algebra.ComputationalGraph
{
    class MatricesSubtraction : MatricesOperation
    {
        public MatricesSubtraction(Matrix matrix1, Matrix matrix2) : base(matrix1, matrix2, matrix1.Rows, matrix2.Cols)
        {
        }

        protected override void Evaluate()
        {
            MatrixOperations.Subtract(matrix1.Value, matrix2.Value, @out: value, Rows, Cols);
        }
    }
}
