﻿using Basics.AI.NeuralNetworks.Demos.Perceptron;
using Basics.Main.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.Xor
{
    public partial class XorDemoForm : NeuralNetworkDemoForm
    {
        Bitmap Bitmap = new Bitmap(100 * 2, 100 * 2);

        public XorDemoForm()
        {
        }

        protected override Control CreateOutputControl()
        {
            Control outputControl = base.CreateOutputControl();
            outputControl.Paint += OnPaintOutputControl;
            return outputControl;
        }

        protected override void InitializeProperties()
        {
            base.InitializeProperties();

            Properties.TrainingSetSize = 20;
            Properties.AddTrainingSet("Random", LoadTrainingSet);
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
            NetworksPath = Program.XorNeuralNetworkNetworksDirectoryPath;
            DrawOutputOnBitmap();
        }

        private IEnumerable<NeuralIO> LoadTrainingSet()
        {
            double classifier(double[] input)
            {
                if (input[0] < 0.5)
                {
                    return input[1] < 0.5 ? 0 : 1;
                }
                else
                {
                    return input[1] < 0.5 ? 1 : 0;
                }
            };
        
            return new PointsNeuralIOGenerator(2, classifier, 0, 1).Generate(Properties.TrainingSetSize).ToArray();
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
                NeuralNetwork.Evaluate(new[] { x, y });
                return NeuralNetwork.Output[0];
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
