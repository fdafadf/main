using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinder
{
    public static class Utility
    {
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> self)
        {
            while (self.MoveNext())
            {
                yield return self.Current;
            }
        }

        public static void FillRandom(this PointF[] self, Random random)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i].X = random.NextFloat();
                self[i].Y = random.NextFloat();
            }
        }

        public static int IndexOfMin(this float[] self, int startIndex)
        {
            float min = self[startIndex];
            int minIndex = startIndex;

            for (int i = startIndex + 1; i < self.Length; i++)
            {
                if (self[i] < min)
                {
                    min = self[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        public static int IndexOfMax(this double[] self, int startIndex)
        {
            double max = self[startIndex];
            int maxIndex = startIndex;

            for (int i = startIndex + 1; i < self.Length; i++)
            {
                if (self[i] > max)
                {
                    max = self[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public static float NextFloat(this Random self)
        {
            return (float)self.NextDouble();
        }

        public static void Shuffle<T>(this T[] self, Random random)
        {
            for (int t = 0; t < self.Length; t++)
            {
                int r = random.Next(self.Length - 1);
                T tmp1 = self[t];
                self[t] = self[r];
                self[r] = tmp1;
            }
        }
    }
}
