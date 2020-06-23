using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNTK;

namespace Labs.AI
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new Vector(0.5, 2.3, 2.9);
            var y = new Vector(1.4, 1.9, 3.2);
            var a = new Scalar(1.0);
            var b = new Scalar(0.0);
            var diff = y - (x * a + b);
            var loss = (diff * diff).Sum;
            var grad_a = (-2.0 * x.Transposed * diff).Sum;
            var grad_b = (-2.0 * diff).Sum;

            do
            {
                Console.WriteLine($"Loss: {loss.Value}");
                a.Value -= grad_a * 0.01;
                b.Value -= grad_b * 0.01;
            }
            while (Math.Max(grad_a.Abs, grad_b.Abs) > 0.1);
        }
    }

    class Expression
    {
        public List<Expression> References = new List<Expression>();
        bool isEvaluated;

        protected bool IsEvaluated
        {
            get
            {
                return isEvaluated;
            }
            set
            {
                if (value == false)
                {
                    foreach (var reference in References)
                    {
                        reference.IsEvaluated = false;
                    }
                }

                isEvaluated = value;
            }
        }
    }

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

        protected virtual void Evaluate()
        {
        }
    }

    class VectorSum : Scalar 
    {
        Vector vector;

        public VectorSum(Vector vector) : base(0)
        {
            this.vector = vector;
            this.vector.References.Add(this);
            this.IsEvaluated = false;
        }

        public double Abs => Math.Abs(Value);

        protected override void Evaluate() => value = vector.Value.Sum();
    }

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

        public Vector Transposed => this;

        public static VectorMultiplication operator *(double c, Vector v) => new VectorMultiplication(v, new Scalar(c));

        public static VectorMultiplication operator *(Vector v, Scalar s) => new VectorMultiplication(v, s);

        public static VectorsMultiplication operator *(Vector v1, Vector v2) => new VectorsMultiplication(v1, v2);

        public static VectorAddition operator +(Vector v, Scalar s) => new VectorAddition(v, s);

        public static VectorsSubstraction operator -(Vector v1, Vector v2) => new VectorsSubstraction(v1, v2);

        protected virtual void Evaluate()
        {
        }
    }

    class VectorVectorOperation : Vector
    {
        protected Vector vector1;
        protected Vector vector2;

        public VectorVectorOperation(Vector vector1, Vector vector2) : base(new double[vector1.Value.Length])
        {
            Assert.Equals(vector1.Value.Length, vector2.Value.Length);
            this.vector1 = vector1;
            this.vector1.References.Add(this);
            this.vector2 = vector2;
            this.vector2.References.Add(this);
        }
    }

    class VectorsMultiplication : VectorVectorOperation
    {
        public VectorsMultiplication(Vector vector1, Vector vector2) : base(vector1, vector2)
        {
        }

        protected override void Evaluate()
        {
            var vector1Value = vector1.Value;
            var vector2Value = vector2.Value;

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = vector1Value[i] * vector2Value[i];
            }
        }
    }

    class VectorsSubstraction : VectorVectorOperation
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

    class VectorScalarOperation : Vector
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

    class VectorAddition : VectorScalarOperation
    {
        public VectorAddition(Vector vector, Scalar scalar) : base(vector, scalar)
        {
        }

        protected override void Evaluate()
        {
            var vectorValue = vector.Value;
            var scalarValue = scalar.Value;

            for (int i = 0; i < value.Length; i++)
            {
                value[i] = vectorValue[i] + scalarValue;
            }
        }
    }

    class VectorMultiplication : VectorScalarOperation
    {
        public VectorMultiplication(Vector vector, Scalar scalar) : base(vector, scalar)
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

    class Assert
    {
        public static void Equals(int a, int b)
        {
            if (a != b)
            {
                throw new ArgumentException();
            }
        }
    }
}
