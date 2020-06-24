namespace Math.Algebra.ComputationalGraph
{
    abstract class VectorScalarOperation : Vector
    {
        protected Vector vector;
        protected Scalar scalar;

        public VectorScalarOperation(Vector vector, Scalar scalar) : base(new double[vector.Value.Length])
        {
            this.vector = vector;
            this.vector.References.Add(this);
            this.scalar = scalar;
            this.scalar.References.Add(this);
        }
    }
}
