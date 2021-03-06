﻿using Math.Algebra.ComputationalGraph;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static Math.Algebra.ComputationalGraph.ExpressionHelper;

namespace Labs.AI
{
    class Program
    {
        static void Main()
        {
            NeuralNetworkWithManyLayers();
        }

        static void GradientDescentLinearRegression()
        {
            var x = new Vector(0.5, 2.3, 2.9);
            var y = new Vector(1.4, 1.9, 3.2);
            var a = new Scalar(1);
            var b = new Scalar(0);
            var diff = y - (x * a + b);
            var loss = Sum(diff * diff);
            var grad_a = Sum(-2.0 * x.T * diff);
            var grad_b = Sum(-2.0 * diff);

            do
            {
                Print($"Loss: {loss.Value}");
                a.Value -= grad_a * 0.01;
                b.Value -= grad_b * 0.01;
            }
            while (Max(grad_a.Abs, grad_b.Abs) > 0.01);
        }

        static void GradientDescentLinearRegressionWithMathSymbols()
        {
            var x = new Vector(0.5, 2.3, 2.9);
            var y = new Vector(1.4, 1.9, 3.2);
            var α = 0.01;
            var γ = 0.2;

            var a = new Scalar(1);
            var b = new Scalar(0);
            var ŷ = x * a + b;
            var δy = y - ŷ;
            var Ｌ = Sum(δy * δy);
            var Δa = Sum(-2 * x.T * δy);
            var Δb = Sum(-2 * δy);

            do
            {
                Print($"Loss: {Ｌ}");
                a.Value -= Δa * α;
                b.Value -= Δb * α;
            }
            while (Max(Abs(Δa), Abs(Δb)) > γ);
        }

        static void NeuralNetworkWithTwoLayers()
        {
            var random = new Random();
            var batchSize = 64;
            var inputSize = 1000;
            var hiddenLayerSize = 200;
            var outputSize = 5;
            var learningRate = 1e-6;
            var x = random.StandardDistribution(rows: batchSize, cols: inputSize);
            var y = random.StandardDistribution(rows: batchSize, cols: outputSize);
            var w1 = random.StandardDistribution(rows: inputSize, cols: hiddenLayerSize);
            var w2 = random.StandardDistribution(rows: hiddenLayerSize, cols: outputSize);
            var w1_sum = x * w1;
            var w1_out = Max(w1_sum, 0);
            var w2_sum = w1_out * w2;
            var diff = y - w2_sum;
            var loss = Sum(Square(diff));
            var grad_w2_out = -2.0 * (y - w2_sum);
            var grad_w2 = w1_out.T * grad_w2_out;
            var grad_w1_out = grad_w2_out * w2.T;
            var grad_w1 = x.T * Max(grad_w1_out, 0);
            var step_w1 = learningRate * grad_w1;
            var step_w2 = learningRate * grad_w2;

            for (int i = 0; i < 1000; i++)
            {
                Print($"Loss: {loss.Value}");
                var step_w1_value = step_w1.Value;
                var step_w2_value = step_w2.Value;
                w1.Subtract(step_w1_value);
                w2.Subtract(step_w2_value);
            }
        }

        static void NeuralNetworkWithManyLayers()
        {
            using (ChartForm form = new ChartForm())
            {
                form.Load += (s, a) =>
                {
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            int t = 100;
                            var seed = Guid.NewGuid().GetHashCode();
                            Console.WriteLine($"Seed: {seed}");
                            Series series = null;

                            form.Invoke((MethodInvoker)delegate
                            {
                                series = form.NewSeries($"{seed}");
                            });

                            NeuralNetworkWithManyLayers(seed, value =>
                            {
                                if (t == 0)
                                {
                                    form.Invoke((MethodInvoker)delegate
                                    {
                                        series.Points.Add(value);
                                    });
                                }
                                else
                                {
                                    t--;
                                }
                            });
                        }
                    });
                };

                Application.Run(form);
            }
        }

        static void NeuralNetworkWithManyLayers(int seed, Action<double> print)
        {
            var random = new Random(seed); // 2093337509 -246985280
            var batchSize = 64;
            var inputSize = 200;
            //var hiddenLayersSize = new[] { 1000, 1000, 100, 100, 100, 10, 10, 10, 10 };
            var hiddenLayersSize = new[] { 100, 50 };
            var H = hiddenLayersSize.Length;
            var outputSize = 5;
            var learningRate = 1e-6;
            var x = random.StandardDistribution(rows: batchSize, cols: inputSize);
            var y = random.StandardDistribution(rows: batchSize, cols: outputSize);
            var In = new Matrix[H + 1];
            var Sum = new Matrix[H + 1];
            var Weights = new Matrix[H + 1];
            var GradOut = new Matrix[H + 1];
            var Grad = new Matrix[H + 1];
            var Step = new Matrix[H + 1];
            var StepValues = new double[H + 1][][];
            In[0] = x;

            for (int i = 0; i < H; i++)
            {
                Weights[i] = random.StandardDistribution(rows: In[i].Cols, cols: hiddenLayersSize[i]);
                Sum[i] = In[i] * Weights[i];
                In[i + 1] = Max(Sum[i], 0);
            }

            Weights[H] = random.StandardDistribution(rows: In[H].Cols, cols: outputSize);
            Sum[H] = In[H] * Weights[H];
            var diff = y - Sum[H];
            var loss = Square(diff).Sum;
            GradOut[H] = -2.0 * (y - Sum[H]);
            Grad[H] = In[H].T * GradOut[H];
            Step[H] = learningRate * Grad[H];

            for (int i = H - 1; i >= 0; i--)
            {
                GradOut[i] = GradOut[i + 1] * Weights[i + 1].T;
                Grad[i] = In[i].T * Max(GradOut[i], 0);
                Step[i] = learningRate * Grad[i];
            }

            for (int k = 0; k < 1000; k++)
            {
                print(loss.Value);
                //print(NaN(Sum));
                Step.GetValues(StepValues);
                Weights.Subtract(StepValues);
            }
        }
    }
}
