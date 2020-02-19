using AI.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demos.AI.NeuralNetwork
{
    public partial class PlaneForm : Form
    {
        IFunction ActivationFunction;

        public PlaneForm()
        {
            InitializeComponent();
        }

        public PlaneForm(IFunction activationFunction)
        {
            InitializeComponent();
            ActivationFunction = activationFunction;
        }

        private void PlaneForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void PlaneForm_DoubleClick(object sender, EventArgs e)
        {
        }

        Bitmap bitmap;
        double[][] features;
        double[][] labels;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (bitmap != null)
            {
                lock (bitmap)
                {
                    e.Graphics.DrawImage(bitmap, 0, 0, ClientSize.Width, ClientSize.Height);
                }
            }

            if (features != null)
            {
                for (int i = 0; i < features.Length; i++)
                {
                    double x = features[i][0] * ClientSize.Width;
                    double y = features[i][1] * ClientSize.Height;
                    var brush = labels[i][0] > 0 ? Brushes.SeaGreen : (labels[i][1] > 0 ? Brushes.Red : Brushes.Yellow);
                    e.Graphics.FillEllipse(brush, (float)x - 2, (float)y - 2, 4, 4);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bitmap = new Bitmap(200, 200);
            Random random = new Random(0);
            Optimizer optimizer;
            Network network;

            if (ActivationFunction == Function.ReLU)
            {
                //var network = NetworkBuilder.Build(Function.ReLU, 2, 3, 64, 64, 32, 32);
                //evaluator = new NetworkEvaluator(network);
                //optimizer = new SGDMomentum(evaluator, 0.001, 0.04);
                network = new Network(Function.ReLU, 2, 3, 72, 72, 72, 36, 36, 36, 18, 18);
                optimizer = new SGDMomentum(network, 0.001, 0.008);
            }
            else
            {
                network = new Network(Function.Sigmoidal, 2, 3, 32, 8);
                optimizer = new SGDMomentum(network, 0.1, 0.8);
            }

            var trainer = new Trainer(optimizer);
            var mseMonitor = new MeanSquareErrorMonitor();
            trainer.Monitors.Add(mseMonitor);
            int width;
            int height;

            lock (bitmap)
            {
                width = bitmap.Width;
                height = bitmap.Height;
            }

            var imagePointList = new List<double[]>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    imagePointList.Add(new double[] { (double)x / width, (double)y / height });
                }
            }

            GenerateTrainingData(random);

            for (int epoch = 0; epoch < 15000; epoch++)
            {
                //if (epoch % 300 == 0)
                //{
                //    GenerateTrainingData(random);
                //}

                if (epoch % 100 == 0)
                {
                    RefreshBitmap(network, width, height, imagePointList);
                }

                trainer.Train(features, labels, 1);
                Console.WriteLine(mseMonitor.CollectedData.Last());
            }
        }

        private void GenerateTrainingData(Random random)
        {
            List<double[]> featureList = new List<double[]>();
            List<double[]> labelList = new List<double[]>();
            double[] a = new double[] { 1, 0, 0 };
            double[] b = new double[] { 0, 1, 0 };
            double[] c = new double[] { 0, 0, 1 };

            for (int i = 0; i < 150; i++)
            {
                double x = random.NextDouble();
                double y = random.NextDouble();
                double r = Math.Sqrt((x - 0.5) * (x - 0.5) + (y - 0.5) * (y - 0.5));
                featureList.Add(new double[] { x, y });
                labelList.Add(r > 0.2 ? (r > 0.3 ? a : c) : b);
            }

            features = featureList.ToArray();
            labels = labelList.ToArray();
        }

        byte[] rgbValues;

        private void RefreshBitmap(Network evaluator, int width, int height, List<double[]> imagePointList)
        {
            lock (bitmap)
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;

                if (rgbValues == null || rgbValues.Length != bytes)
                {
                    rgbValues = new byte[bytes];
                }

                System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, rgbValues, 0, bytes);
                int p = 0;

                foreach (var imagePoint in imagePointList)
                {
                    double[] prediction = evaluator.Evaluate(imagePoint);
                    rgbValues[p + 3] = 255;
                    rgbValues[p + 0] = (byte)(255 * prediction[0]);
                    rgbValues[p + 1] = (byte)(255 * prediction[1]);
                    rgbValues[p + 2] = (byte)(255 * prediction[2]);
                    p += 4;
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, bitmapData.Scan0, bytes);
                bitmap.UnlockBits(bitmapData);
            }

            Invoke((MethodInvoker)delegate ()
            {
                Refresh();
            });
        }
    }
}
