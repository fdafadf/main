namespace Math.Algebra.ComputationalGraph
{
    class VectorScalarMultiplication : VectorScalarOperation
    {
        public VectorScalarMultiplication(Vector vector, Scalar scalar) : base(vector, scalar)
        {
        }

        protected override void Evaluate()
        {
            var vectorValue = vector.Value;
            var scalarValue = scalar.Value;

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = vectorValue[i] * scalarValue;
            }
        }
    }
}
