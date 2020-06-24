namespace Math.Algebra.ComputationalGraph
{
    abstract class MatrixScalarOperation : Matrix
    {
        protected Matrix matrix;
        protected Scalar scalar;

        public MatrixScalarOperation(Matrix matrix, Scalar scalar) : base(matrix.Rows, matrix.Cols)
        {
            this.matrix = matrix;
            this.matrix.References.Add(this);
            this.scalar = scalar;
            this.scalar.References.Add(this);
        }
    }
}
