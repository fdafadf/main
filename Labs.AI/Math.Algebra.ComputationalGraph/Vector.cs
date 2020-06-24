namespace Math.Algebra.ComputationalGraph
{
    class Vector : Expression
    {
        protected double[] value;

        public Vector(params double[] value)
        {
            this.value = value;
        }

        public double[] Value 
        { 
            get
            {
                if (IsEvaluated == false)
                {
                    Evaluate();
                    IsEvaluated = true;
                }

                return value;
            }
        }

        public VectorSum Sum => new VectorSum(this);
        public Vector T => this;
        public static VectorScalarMultiplication operator *(double c, Vector v) => new VectorScalarMultiplication(v, new Scalar(c));
        public static VectorScalarMultiplication operator *(Vector v, Scalar s) => new VectorScalarMultiplication(v, s);
        public static VectorsMultiplication operator *(Vector v1, Vector v2) => new VectorsMultiplication(v1, v2);
        public static VectorScalarAddition operator +(Vector v, Scalar s) => new VectorScalarAddition(v, s);
        public static VectorsSubstraction operator -(Vector v1, Vector v2) => new VectorsSubstraction(v1, v2);
        public static implicit operator Vector(double[] value) => new Vector(value);

        protected virtual void Evaluate()
        {
        }
    }
}
