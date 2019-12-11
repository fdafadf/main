using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.Main.UI
{
    public partial class PerceptronForm : Form
    {
        public const int FunctionArgumentsCount = 2;
        public const int FunctionImageSize = 100;
        public const double TrainAlpha = 0.5;
        public const int TrainSamplesCount = 20;
        public const int TrainIterations = 1000;

        Bitmap Bitmap = new Bitmap(FunctionImageSize * 2, FunctionImageSize * 2);

        public PerceptronForm()
        {
            InitializeComponent();
            Generate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(Bitmap, 10, 10);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Space:
                    Generate();
                    Refresh();
                    break;
            }
        }

        private void Generate()
        {
            void PixelToFunctionDomain(double px, double py, out double x, out double y)
            {
                x = px - FunctionImageSize;
                y = FunctionImageSize - py;
            }

            void FunctionDomainToPixel(double x, double y, out double px, out double py)
            {
                px = FunctionImageSize + x;
                py = FunctionImageSize - y;
            }

            // Random line function
            double a = Extensions.Random.NextDouble() * 2 + 0.1;
            double b = Extensions.Random.NextDouble() * 10;
            Func<double, double> Line = x => a * x + b;

            // Classifiers based on line function
            Func<double, double> activationFunction = input => input < 0 ? -1 : 1;
            Func<double[], double> classifier = input => Line(input[0]) < input[1] ? -1 : 1;

            // Random preceptron
            Perceptron perceptron = new Perceptron(FunctionArgumentsCount);

            // Random data to train
            IEnumerable<TestData> testItems = new TestDataFactory(FunctionArgumentsCount, classifier, -FunctionImageSize, FunctionImageSize).Generate(TrainSamplesCount);

            // Train
            perceptron.Train(testItems, activationFunction, TrainAlpha, TrainIterations);

            // Draw perceprtron output
            Bitmap.DrawFunctionOutput((x, y) => perceptron.Output(new double[] { x, y }), PixelToFunctionDomain);
            Bitmap.DrawTestData(testItems, FunctionDomainToPixel);
        }

        private void drawAndTrain_Click(object sender, EventArgs e)
        {
            Generate();
            Refresh();
        }
    }
}
