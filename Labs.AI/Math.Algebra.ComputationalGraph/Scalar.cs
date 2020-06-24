namespace Math.Algebra.ComputationalGraph
{
    class Scalar : Expression
    {
        protected double value;

        public Scalar(double value)
        {
            this.value = value;
            IsEvaluated = true;
        }
        
        public double Value 
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
            set
            {
                this.value = value;
                IsEvaluated = false;
            }
        }

        public static double operator -(Scalar s, double c) => s.Value - c;
        public static implicit operator double(Scalar s) => s.Value;
        public static explicit operator Scalar(double value) => new Scalar(value);

        public override string ToString()
        {
            return Value.ToString();
        }

        protected virtual void Evaluate()
        {
        }
    }
}
