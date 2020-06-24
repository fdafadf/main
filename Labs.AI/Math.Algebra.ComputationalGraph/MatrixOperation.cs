namespace Math.Algebra.ComputationalGraph
{
    class MatrixOperation : Matrix
    {
        protected Matrix matrix;

        public MatrixOperation(Matrix matrix) : base(matrix.Cols, matrix.Rows)
        {
            this.matrix = matrix;
            this.matrix.References.Add(this);
        }
    }
}
