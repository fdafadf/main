using Basics.AI.NeuralNetworks;
using Basics.Games.TicTacToe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Basics.Main.UI
{
    static class Extensions
    {
        public delegate void Translation2d(double ix, double iy, out double ox, out double oy);

        public static readonly Random Random = new Random();

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

        public static string Concatenate<T>(this IEnumerable<T> self, string separator)
        {
            return string.Join(separator, self);
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

        public static void SetItems(this ComboBox self, IEnumerable<FileInfo> files)
        {
            self.Items.Clear();

            foreach (FileInfo trainDataFile in files)
            {
                self.Items.Add(trainDataFile);
            }

            if (self.Items.Count > 0)
            {
                self.SelectedIndex = 0;
            }
        }

        public static void ForEach<T>(this T[] self, Action<T> action)
        {
            foreach (T item in self)
            {
                action(item);
            }
        }

        public static string AsString(this double[] self, string format)
        {
            return string.Join(",", self.Select(i => i.ToString(format)));
        }

        public static string AsString<T>(this T[] self)
        {
            return string.Join(",", self.Select(i => i.ToString()));
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

        public static T Add<T>(this ToolStripItemCollection self, ContextMenuStrip contextMenu) where T : ToolStripDropDownItem
        {
            T menu = Activator.CreateInstance<T>();
            menu.Text = contextMenu.Text;

            while (contextMenu.Items.Count > 0)
            {
                menu.DropDownItems.Add(contextMenu.Items[0]);
            }

            self.Add(menu);
            return menu as T;
        }

        public static double[] Fill(this Random self, double[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = self.NextDouble();
            }

            return values;
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

        public static double Product(this double[] v1, double[] v2)
        {
            double result = 0;

            for (int i = 0; i < v1.Length; i++)
            {
                result += v1[i] * v2[i];
            }

            return result;
        }

        public static void DrawLine(this Graphics self, Pen pen, double x1, double y1, double x2, double y2)
        {
            self.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public static void DrawEllipse(this Graphics self, Pen pen, double x1, double y1, double x2, double y2)
        {
            self.DrawEllipse(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public static void DrawTestData(this Bitmap self, IEnumerable<NeuralIO> testData, Func<double[], Pen> classifier, Translation2d translation)
        {
            if (testData != null)
            {
                using (Graphics graphics = Graphics.FromImage(self))
                {
                    foreach (NeuralIO testItem in testData)
                    {
                        translation(testItem.Input[0], testItem.Input[1], out double px, out double py);
                        Pen pen = classifier(testItem.Output);//[0] < 0 ? Pens.Blue : Pens.Red;
                        graphics.DrawEllipse(pen, px - 3, py - 3, 6, 6);
                    }
                }
            }
        }

        public static void DrawFunctionOutput(this Bitmap self, Func<double, double, double> func, Translation2d translation)
        {
            double min = double.MaxValue;
            double max = double.MinValue;

            double[,] outputs = new double[self.Width, self.Height];

            for (int py = 0; py < self.Height; py++)
            {
                for (int px = 0; px < self.Width; px++)
                {
                    translation(px, py, out double x, out double y);
                    double output = func(x, y);
                    outputs[px, py] = output;
                    if (output < min) min = output;
                    if (output > max) max = output;
                }
            }

            for (int py = 0; py < self.Height; py++)
            {
                for (int px = 0; px < self.Width; px++)
                {
                    //translation(px, py, out double x, out double y);
                    double output = outputs[px, py];

                    if (output < 0)
                    {
                        int c = (int)(255.0 / +min * output);
                        self.SetPixel(px, py, Color.FromArgb(c, 255, 255));
                    }
                    else
                    {
                        int c = (int)(255.0 / max * output);
                        self.SetPixel(px, py, Color.FromArgb(255, 255, c));
                    }
                }
            }
        }
    }
}
