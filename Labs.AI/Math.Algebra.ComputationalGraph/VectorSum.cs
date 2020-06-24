using System;
using System.Linq;

namespace Math.Algebra.ComputationalGraph
{
    class VectorSum : Scalar 
    {
        Vector vector;

        public VectorSum(Vector vector) : base(0)
        {
            this.vector = vector;
            this.vector.References.Add(this);
            this.IsEvaluated = false;
        }

        public double Abs => System.Math.Abs(Value);
        protected override void Evaluate() => value = vector.Value.Sum();
    }
}
