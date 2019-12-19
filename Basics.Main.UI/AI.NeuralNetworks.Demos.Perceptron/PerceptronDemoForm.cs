using Basics.Main.UI;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.Perceptron
{
    public partial class PerceptronDemoForm : Form
    {
        public const int FunctionArgumentsCount = 2;
        public const int FunctionImageSize = 100;
        public const double TrainAlpha = 0.5;
        public const int TrainSamplesCount = 20;
        public const int MaxTrainIterations = 10000;

        Bitmap Bitmap = new Bitmap(FunctionImageSize * 2, FunctionImageSize * 2);

        public PerceptronDemoForm()
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
            double b = Extensions.Random.NextDouble() * 100;
            Func<double, double> Line = x => a * x + b;

            // Classifiers based on line function
            activationFunction = input => input < 0 ? -1 : 1;
            classifier = input => Line(input[0]) < input[1] ? -1 : 1;

            // Random preceptron
            perceptron = new AI.NeuralNetworks.Perceptron(FunctionArgumentsCount, activationFunction, - 50, 50);

            // Random data to train
            testItems = new PointsNeuralIOGenerator(FunctionArgumentsCount, classifier, -FunctionImageSize, FunctionImageSize).Generate(TrainSamplesCount).ToArray();

            // Train
            perceptron.Train(testItems, TrainAlpha, MaxTrainIterations);

            // Draw perceprtron output
            Bitmap.DrawFunctionOutput((x, y) => perceptron.Output(new double[] { x, y }), PixelToFunctionDomain);
            Bitmap.DrawTestData(testItems, FunctionDomainToPixel);
        }

        AI.NeuralNetworks.Perceptron perceptron;
        AI.NeuralNetworks.NeuralIO[] testItems;
        Func<double, double> activationFunction;
        Func<double[], double> classifier;

        private void drawAndTrain_Click(object sender, EventArgs e)
        {
            Generate();
            Refresh();
        }

        private void PerceptronForm_DoubleClick(object sender, EventArgs e)
        {
            int testDataSize = testItems.Count();

            for (int i = 0; i < testDataSize; i++)
            {
                double output = activationFunction(perceptron.Output(testItems[i].Input));
                double outputError = testItems[i].Output - output;
                Console.WriteLine($"({testItems[i].Input[0]}, {testItems[i].Input[1]}) {outputError}");
            }
        }
    }
}
