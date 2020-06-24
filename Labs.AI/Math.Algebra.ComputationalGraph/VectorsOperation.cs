using Labs.AI;

namespace Math.Algebra.ComputationalGraph
{
    abstract class VectorsOperation : Vector
    {
        protected Vector vector1;
        protected Vector vector2;

        public VectorsOperation(Vector vector1, Vector vector2) : base(new double[vector1.Value.Length])
        {
            Assert.Equals(vector1.Value.Length, vector2.Value.Length);
            this.vector1 = vector1;
            this.vector1.References.Add(this);
            this.vector2 = vector2;
            this.vector2.References.Add(this);
        }
    }
}
