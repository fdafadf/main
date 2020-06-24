using Math.Algebra.ComputationalGraph;
using System;
using static Math.Algebra.ComputationalGraph.ExpressionHelper;

namespace Labs.AI
{
    class Program
    {
        static void Main()
        {
            NeuralNetworkWithTwoLayers();
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
            var inputSize = 100;
            var hiddenLayerSize = 10;
            var outputSize = 1;
            var learningRate = 1e-6;
            var x = Matrix.Normal(random, rows: batchSize, cols: inputSize);
            var y = Matrix.Normal(random, rows: batchSize, cols: outputSize);
            var w1 = Matrix.Normal(random, rows: inputSize, cols: hiddenLayerSize);
            var w2 = Matrix.Normal(random, rows: hiddenLayerSize, cols: outputSize);
            var w1_sum = x * w1;
            var w1_out = Max(w1_sum, 0);
            var w2_sum = w1_out * w2;
            var diff = y - w2_sum;
            var loss = Sum(Square(diff));
            var grad_w2_out = -2.0 * (y - w2_sum);
            var grad_w2 = w1_out.T * grad_w2_out;
            var grad_w1_out = grad_w2_out * w2.T;
            var grad_w1 = x.T * Max(grad_w1_out, 0);

            for (int i = 0; i < 500; i++)
            {
                Print($"Loss: {loss.Value}");
                var step_w1 = (learningRate * grad_w1).Value;
                var step_w2 = (learningRate * grad_w2).Value;
                w1.Subtract(step_w1);
                w2.Subtract(step_w2);
            }
        }
    }
}
