using System;

namespace AI.NeuralNetworks
{
    public class GaussianRandom : Random
    {
        public double Mean { get; }
        public double Deviation { get; }

        public GaussianRandom()
        {
        }

        public GaussianRandom(int seed/*, double mean, double deviation*/) : base(seed)
        {
            //Mean = mean;
            //Deviation = deviation;
        }

        //public override double NextDouble()
        //{
        //    double u1 = 1.0 - base.NextDouble(); //uniform(0,1] random doubles
        //    double u2 = 1.0 - base.NextDouble();
        //    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        //    //double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
        //    return randStdNormal;
        //}

        public override double NextDouble()
        {
            double u1 = base.NextDouble(); //uniform(0,1] random doubles
            double u2 = base.NextDouble();
            double a = Math.Sqrt(-2.0 * Math.Log(u1));
            double b = Math.Cos(2.0 * Math.PI * u2);
            double randStdNormal = a * b; //random normal(0,1)
            //double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randStdNormal;
        }
    }
}
