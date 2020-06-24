using System.Collections.Generic;

namespace Math.Algebra.ComputationalGraph
{
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
}
