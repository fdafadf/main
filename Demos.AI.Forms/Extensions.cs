using AI.NeuralNetworks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Games.Utilities.Extensions;

namespace Demos.Forms
{
    public static class Extensions
    {
        public static Random Random = new Random();

        //public static void LoadWeights<TOutput>(this NeuralNetwork<TOutput> self, string filePath)
        //{
        //    string fileContent = File.ReadAllText(filePath);
        //    double[][][] weights = JsonConvert.DeserializeObject<double[][][]>(fileContent);
        //    self.SetWeights(weights);
        //}
        //
        //public static void SaveWeights<TOutput>(this NeuralNetwork<TOutput> self, string filePath)
        //{
        //    string fileContent = JsonConvert.SerializeObject(self.GetWeights());
        //    File.WriteAllText(filePath, fileContent);
        //}

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

        public static void DrawLine(this Graphics self, Pen pen, double x1, double y1, double x2, double y2)
        {
            self.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public static void DrawEllipse(this Graphics self, Pen pen, double x1, double y1, double x2, double y2)
        {
            self.DrawEllipse(pen, (float)x1, (float)y1, (float)x2, (float)y2);
        }

        public static void DrawTestData(this Bitmap self, IEnumerable<ConvertedInput> testData, Func<double[], Pen> classifier, Translation2d translation)
        {
            if (testData != null)
            {
                using (Graphics graphics = Graphics.FromImage(self))
                {
                    foreach (ConvertedInput testItem in testData)
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
