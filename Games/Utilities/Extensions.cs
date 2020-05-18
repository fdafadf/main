using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Games.Utilities
{
    public static class Extensions
    {
        public static void Initialize<T>(this T[] self, Func<T> initializer)
        {
            for (int i = 0; i < self.Length; i++)
            {
                self[i] = initializer();
            }
        }

        public static void Apply<T>(this Action<T> self, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                self(item);
            }
        }

        public static void WriteDoubleArray(this BinaryWriter self, double[] array)
        {
            Action<double> write = self.Write;
            write.Apply(array);
        }
        
        public static T[] EvaluateMany<T>(this Func<T> self, int size)
        {
            T[] result = new T[size];

            for (int i = 0; i < size; i++)
            {
                result[i] = self();
            }

            return result;
        }

        public static double[] ReadDoubleArray(this BinaryReader self, int size)
        {
            Func<double> read = self.ReadDouble;
            return read.EvaluateMany(size);
        }

        public static int[] ReadInt32Array(this BinaryReader self, int size)
        {
            Func<int> read = self.ReadInt32;
            return read.EvaluateMany(size);
        }

        public static T ReadEnum<T>(this BinaryReader self) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), self.ReadInt32());
        }

        public static void Shuffle<T1, T2>(this Random random, T1[] array1, T2[] array2)
        {
            for (int t = 0; t < array1.Length; t++)
            {
                int r = random.Next(array1.Length - 1);
                T1 tmp1 = array1[t];
                array1[t] = array1[r];
                array1[r] = tmp1;
                T2 tmp2 = array2[t];
                array2[t] = array2[r];
                array2[r] = tmp2;
            }
        }

        public static void Shuffle<T1>(this Random random, T1[] array1)
        {
            for (int t = 0; t < array1.Length; t++)
            {
                int r = random.Next(array1.Length - 1);
                T1 tmp1 = array1[t];
                array1[t] = array1[r];
                array1[r] = tmp1;
            }
        }
        //public static IEnumerable<TAttribute> Flatten<TTree, TAttribute>(this TTree self, Func<TTree, IEnumerable<TTree>> children, Func<TTree, TAttribute> attribute)
        //{
        //    List<TAttribute> result = new List<TAttribute>();
        //    self.Flatten(children, attribute, result);
        //    return result;
        //}
        //
        //private static IEnumerable<TAttribute> Flatten<TTree, TAttribute>(this TTree self, Func<TTree, IEnumerable<TTree>> children, Func<TTree, TAttribute> attribute, List<TAttribute> result)
        //{
        //    result.Add(attribute(self));
        //    return children(self).SelectMany(child => child.Flatten(children, attribute, result));
        //}

        public delegate void Translation2d(double ix, double iy, out double ox, out double oy);

        public static readonly Random Random = new Random();

        public static string ReadUntil(this StreamReader reader, Func<char, bool> predicate)
        {
            StringBuilder result = new StringBuilder();
            char c = (char)reader.Peek();

            while (predicate(c))
            {
                reader.Read();
                result.Append(c);
                c = (char)reader.Peek();
            }

            return result.ToString();
        }

        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (T item in self)
            {
                action(item);
            }
        }

        public static double[] Fill(this Random self, double[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = self.NextDouble();
            }

            return values;
        }

        public static double Product(this double[] v1, double[] v2)
        {
            double result = 0;

            for (int i = 0; i < v1.Length; i++)
            {
                result += v1[i] * v2[i];
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOf<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            int index = 0;

            foreach (var item in self)
            {
                if (predicate(item))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> MaxItems<T>(this IEnumerable<T> collection, Func<T, double> valueSelector)
        {
            double maxValue;
            List<T> maxElements = new List<T>();
            IEnumerator<T> e = collection.GetEnumerator();

            if (e.MoveNext())
            {
                maxValue = valueSelector(e.Current);
                maxElements.Add(e.Current);

                while (e.MoveNext())
                {
                    double weight = valueSelector(e.Current);

                    if (weight > maxValue)
                    {
                        maxValue = weight;
                        maxElements.Clear();
                        maxElements.Add(e.Current);
                    }
                    else if (weight == maxValue)
                    {
                        maxElements.Add(e.Current);
                    }
                }
            }

            return maxElements;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxItem<T>(this IEnumerable<T> collection, Func<T, double> valueSelector)
        {
            IEnumerator<T> e = collection.GetEnumerator();
            double maxValue;
            T maxElement = default(T);

            if (e.MoveNext())
            {
                maxValue = valueSelector(e.Current);
                maxElement = e.Current;

                while (e.MoveNext())
                {
                    double weight = valueSelector(e.Current);

                    if (weight > maxValue)
                    {
                        maxValue = weight;
                        maxElement = e.Current;
                    }
                }
            }

            return maxElement;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfMin<T>(this IEnumerable<T> self, Func<T, IComparable> valueSelector)
        {
            int result = -1;
            var enumerator = self.GetEnumerator();

            if (enumerator.MoveNext())
            {
                result = 0;
                IComparable min = valueSelector(enumerator.Current);
                int index = 1;

                while (enumerator.MoveNext())
                {
                    IComparable value = valueSelector(enumerator.Current);

                    if (min.CompareTo(value) > 0)
                    {
                        min = value;
                        result = index;
                    }

                    index++;
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfMin<T>(this IEnumerable<T> self) where T : IComparable
        {
            return self.IndexOfMin(item => item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfMax<T>(this IEnumerable<T> self, Func<T, IComparable> valueSelector)
        {
            int result = -1;
            var enumerator = self.GetEnumerator();

            if (enumerator.MoveNext())
            {
                result = 0;
                IComparable max = valueSelector(enumerator.Current);
                int index = 1;

                while (enumerator.MoveNext())
                {
                    IComparable value = valueSelector(enumerator.Current);

                    if (max.CompareTo(value) < 0)
                    {
                        max = value;
                        result = index;
                    }

                    index++;
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfMax<T>(this T[] self) where T : IComparable
        {
            return self.IndexOfMax(item => item);
        }

        public static Dictionary<TKey, TValue> Unique<TKey, TValue>(this IEnumerable<TValue> self, Func<TValue, TKey> keySelector)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            foreach (TValue value in self)
            {
                TKey key = keySelector(value);

                if (result.ContainsKey(key) == false)
                {
                    result.Add(key, value);
                }
            }

            return result;
        }

        public static T Next<T>(this Random self, IEnumerable<T> collection)
        {
            return collection.ElementAt(self.Next(collection.Count()));
        }

        public static void FillWithRandomValues(this Random self, double[] values, double min, double max)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = self.NextDouble() * (max - min) + min;
            }
        }

        public static double[] FillWithRandomValues(this double[] values, double min, double max)
        {
            Random.FillWithRandomValues(values, min, max);
            return values;
        }

        public static double[] ToDouble(this int[] self)
        {
            double[] result = new double[self.Length];

            for (int i = 0; i < self.Length; i++)
            {
                result[i] = self[i];
            }

            return result;
        }
    }
}
