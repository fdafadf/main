using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Basics.Main.UI
{
    static class Extensions
    {
        public delegate void Translation2d(double ix, double iy, out double ox, out double oy);

        public static readonly Random Random = new Random();

        public static void Fill(this Random self, double[] values, double min, double max)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = self.NextDouble() * (max - min) + min;
            }
        }

        public static double[] Fill(this double[] values, double min, double max)
        {
            Random.Fill(values, min, max);
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

        public static void DrawTestData(this Bitmap self, IEnumerable<TestData> testData, Translation2d translation)
        {
            using (Graphics graphics = Graphics.FromImage(self))
            {
                foreach (TestData testItem in testData)
                {
                    translation(testItem.Input[0], testItem.Input[1], out double px, out double py);
                    Pen pen = testItem.Output < 0 ? Pens.Blue : Pens.Red;
                    graphics.DrawEllipse(pen, px - 3, py - 3, 6, 6);
                }
            }
        }

        public static void DrawFunctionOutput(this Bitmap self, Func<double, double, double> func, Translation2d translation)
        {
            double min = double.MaxValue;
            double max = double.MinValue;

            for (int py = 0; py < self.Height; py++)
            {
                for (int px = 0; px < self.Width; px++)
                {
                    translation(px, py, out double x, out double y);
                    double output = func(x, y);
                    if (output < min) min = output;
                    if (output > max) max = output;
                }
            }

            for (int py = 0; py < self.Height; py++)
            {
                for (int px = 0; px < self.Width; px++)
                {
                    translation(px, py, out double x, out double y);
                    double output = func(x, y);

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
