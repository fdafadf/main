using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Labs.AI.NeuralNetworks.Ants
{
    public static class Extensions
    {
        public static T Next<T>(this Random self, T[] array)
        {
            return array[self.Next(array.Length)];
        }

        public static void InvokeAction(this Form self, Action action)
        {
            self.Invoke((MethodInvoker)delegate
            {
                action();
            });
        }

        public static double CrossProduct(this Vector2 v1, Vector2 v2)
        {
            return (v1.X* v2.Y) - (v1.Y* v2.X);
        }

        public static IEnumerable<T> Subset<T>(this IEnumerable<T> self, int k, Random random)
        {
            var enumerator = self.GetEnumerator();
            double available = self.Count();
            int selected = 0;
            int needed = k;

            while (selected < k)
            {
                enumerator.MoveNext();

                if (random.NextDouble() < needed / available)
                {
                    yield return enumerator.Current;
                    needed--;
                    selected++;
                }

                available--;
            }
        }

        public static double SmoothL1Loss(this double[] self, double[] y)
        {
            double sum = 0;

            for (int i = 0; i < self.Length; i++)
            {
                double diff = Math.Abs(self[i] - y[i]);

                if (diff < 1)
                {
                    sum += 0.5 * (diff * diff);
                }
                else
                {
                    sum += diff - 0.5;
                }
            }

            return sum / self.Length;
        }

        public static int RandomFromDistribution(this double[] self, Random random)
        {
            double t = random.NextDouble();
            double k = 0;

            for (int i = 0; i < self.Length; i++)
            {
                if (k >= t)
                {
                    return i;
                }
                else
                {
                    k += self[i];
                }
            }

            return self.Length - 1;
        }

        public static double[] Softmax(this double[] self)
        {
            double[] result = self.Select(Math.Exp).ToArray();
            double sum = result.Sum();

            for (int i = 0; i < result.Length; i++)
            {
                result[i] /= sum; 
            }

            return result;
        }

        public static Vector2 Rotate(this Vector2 self, double degrees)
        {
            double radians = ConvertDegreesToRadians(degrees);
            float sin = (float)Math.Sin(radians);
            float cos = (float)Math.Cos(radians);

            float tx = self.X;
            float ty = self.Y;
            self.X = (cos * tx) - (sin * ty);
            self.Y = (sin * tx) + (cos * ty);
            return self;
        }

        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180.0f) * degrees;
        }

        public static float Distance(this Vector2 self, Vector2 v)
        {
            return (v - self).Length();
        }
    }
}
