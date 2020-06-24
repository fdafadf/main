namespace Math.Algebra.ComputationalGraph
{
    class VectorsSubstraction : VectorsOperation
    {
        public VectorsSubstraction(Vector vector1, Vector vector2) : base(vector1, vector2)
        {
        }

        protected override void Evaluate()
        {
            var vector1Value = vector1.Value;
            var vector2Value = vector2.Value;

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = vector1Value[i] - vector2Value[i];
            }
        }
    }
}
