using System.Collections.Generic;
using System.Linq;

namespace AI.NeuralNetworks
{
    public class Network
    {
        public Layer[] Layers { get; }
        public double[][] LastEvaluation { get; private set; }

        public Network(IEnumerable<Layer> layers)
        {
            Layers = layers.ToArray();
            InitializeLastEvaluation();
        }

        private void InitializeLastEvaluation()
        {
            LastEvaluation = Layers.Select(layer => new double[layer.Neurons.Length]).ToArray();
        }

        public double[] Evaluate(double[] input)
        {
            double[] layerInput = input;

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Evaluate(layerInput, LastEvaluation[i]);
                layerInput = LastEvaluation[i];
            }

            return layerInput;
        }

        public override string ToString()
        {
            return string.Join(",", Layers.Select(layer => layer.Neurons.Length));
        }
    }
}
