namespace Math.Algebra.ComputationalGraph
{
    abstract class MatricesOperation : Matrix
    {
        protected Matrix matrix1;
        protected Matrix matrix2;

        public MatricesOperation(Matrix matrix1, Matrix matrix2, int m, int n) : base(m, n)
        {
            this.matrix1 = matrix1;
            this.matrix1.References.Add(this);
            this.matrix2 = matrix2;
            this.matrix2.References.Add(this);
        }
    }
}
