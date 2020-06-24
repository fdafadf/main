namespace Math.Algebra.ComputationalGraph
{
    class MatricesAddition : MatricesOperation
    {
        public MatricesAddition(Matrix matrix1, Matrix matrix2) : base(matrix1, matrix2, matrix1.Rows, matrix2.Cols)
        {
        }

        protected override void Evaluate()
        {
            MatrixOperations.Add(value, matrix1.Value, matrix2.Value, Rows, Cols);
        }
    }
}
