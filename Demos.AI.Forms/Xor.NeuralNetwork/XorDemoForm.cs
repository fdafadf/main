using AI.NeuralNetworks;
using Demos.Forms.Base;
using Demos.Forms.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Demos.Forms.Xor.NeuralNetwork
{
    public partial class XorDemoForm : NeuralNetworkDemoForm
    {
        Bitmap Bitmap = new Bitmap(100 * 2, 100 * 2);

        public XorDemoForm()
        {
        }

        protected override Control InitializeMainControl()
        {
            Control outputControl = base.InitializeMainControl();
            outputControl.Paint += OnPaintOutputControl;
            return outputControl;
        }

        protected override NeuralNetworkDemoFormProperties InitializeProperties()
        {
            var properties = base.InitializeProperties();
            properties.TrainingSetSize = 20;
            properties.AddTrainingSet("Random", LoadTrainingSet);
            return properties;
        }

        protected override void NetworkChanged()
        {
            DrawOutputOnBitmap();
            OutputControl.Refresh();
        }

        protected void OnPaintOutputControl(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Bitmap, 10, 10);
        }
        
        private void XorDemoForm_Load(object sender, EventArgs e)
        {
            //NetworksPath = Program.XorNeuralNetworkNetworksDirectoryPath;
            DrawOutputOnBitmap();
        }

        private IEnumerable<Projection> LoadTrainingSet()
        {
            double[] classifier(double[] input)
            {
                if (input[0] < 0.5)
                {
                    return new double[] { input[1] < 0.5 ? 0 : 1 };
                }
                else
                {
                    return new double[] { input[1] < 0.5 ? 1 : 0 };
                }
            };
        
            return new ClassifiedNeuralIOGenerator(2, classifier, 0, 1).Generate(Properties.TrainingSetSize).ToArray();
        }
        
        private void DrawOutputOnBitmap()
        {
            void PixelToFunctionDomain(double px, double py, out double x, out double y)
            {
                x = px / 200.0;
                y = py / 200.0;
            }
        
            void FunctionDomainToPixel(double x, double y, out double px, out double py)
            {
                px = x * 200.0;
                py = y * 200.0;
            }
        
            Func<double, double, double> networkEvaluation = (x, y) =>
            {
                return NeuralNetwork.Evaluate(new[] { x, y })[0];
            };
            
            Bitmap.DrawFunctionOutput(networkEvaluation, PixelToFunctionDomain);
            Bitmap.DrawTestData(LoadedTrainingSet, o => o[0] < 0.5 ? Pens.Blue : Pens.Red, FunctionDomainToPixel);
        }

        private void InitializeComponent()
        {
            //this.SuspendLayout();
            //// 
            //// XorDemoForm
            //// 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.ClientSize = new System.Drawing.Size(614, 214);
            //this.Name = "XorDemoForm";
            //this.ResumeLayout(false);
            //this.PerformLayout();
        }
    }
}
