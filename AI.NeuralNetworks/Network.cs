using Games.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI.NeuralNetworks
{
    public static class NetworkSerializer
    {
        public static void SaveLayers(BinaryWriter writer, IEnumerable<Layer> layers)
        {
            writer.Write(layers.Count());
            writer.Write(layers.First().Neurons.First().Weights.Length - 1);

            foreach (var layer in layers)
            {
                SaveLayer(writer, layer);
            }
        }

        private static void SaveLayer(BinaryWriter writer, Layer layer)
        {
            writer.Write((int)layer.ActivationFunctionName);
            writer.Write(layer.Neurons.Length);

            for (int i = 0; i < layer.Neurons.Length; i++)
            {
                writer.WriteDoubleArray(layer.Neurons[i].Weights);
            }
        }

        public static IEnumerable<Layer> LoadLayers(BinaryReader reader)
        {
            int numberOfLayers = reader.ReadInt32();
            int inputSize = reader.ReadInt32();
            List<Layer> layers = new List<Layer>();

            for (int i = 0; i < numberOfLayers; i++)
            {
                var layer = LoadLayer(reader, inputSize);
                layers.Add(layer);
                inputSize = layer.Neurons.Length;
            }

            return layers;
        }

        private static Layer LoadLayer(BinaryReader reader, int inputSize)
        {
            FunctionName activationFunctionName = reader.ReadEnum<FunctionName>();
            int layerSize = reader.ReadInt32();
            List<Neuron> neurons = new List<Neuron>();

            for (int i = 0; i < layerSize; i++)
            {
                double[] weights = reader.ReadDoubleArray(inputSize + 1);
                neurons.Add(new Neuron(weights));
            }

            return new Layer(neurons, activationFunctionName);
        }
    }

    public class NetworkDefinition
    {
        public int InputSize { get; set; }
        public List<NetworkLayerDefinition> Layers { get; set; } = new List<NetworkLayerDefinition>();

        public NetworkDefinition(int inputSize, IEnumerable<NetworkLayerDefinition> layers)
        {
            InputSize = inputSize;
            Layers.AddRange(layers);
        }
        
        public NetworkDefinition(FunctionName activationFunction, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        {
            InputSize = inputSize;

            foreach (var layerSize in hiddenLayerSizes.Concat(new [] { outputSize }))
            {
                Layers.Add(new NetworkLayerDefinition(activationFunction, layerSize));
            }
        }
    }

    public class NetworkLayerDefinition
    {
        public FunctionName ActivationFunction { get; set; }
        public int Size { get; set; }

        public NetworkLayerDefinition(FunctionName activationFunction, int size)
        {
            ActivationFunction = activationFunction;
            Size = size;
        }

        public override string ToString()
        {
            return $"{ActivationFunction},{Size}";
        }
    }

    public class NetworkBuilder
    {
        public static Network Build(NetworkDefinition definition, ILayerInitializer initializer)
        {
            return new Network(BuildLayers(definition.Layers, initializer, definition.InputSize));
        }

        private static IEnumerable<Layer> BuildLayers(IEnumerable<NetworkLayerDefinition> definitions, ILayerInitializer initializer, int inputSize)
        {
            foreach (var definition in definitions)
            {
                yield return BuildLayer(definition, initializer, inputSize);
                inputSize = definition.Size;
            }
        }

        private static Layer BuildLayer(NetworkLayerDefinition definition, ILayerInitializer initializer, int inputSize)
        {
            var layerNeurons = new Neuron[definition.Size];
            layerNeurons.Initialize(() => new Neuron(new double[inputSize + 1]));
            var layer = new Layer(layerNeurons, definition.ActivationFunction);
            initializer.Initialize(layer);
            return layer;
        }

        //private static Network Build(FunctionName activationFunction, ILayerInitializer initializer, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        //{
        //    return new Network(BuildLayers(activationFunction, initializer, inputSize, outputSize, hiddenLayerSizes));
        //}
        //
        //private static IEnumerable<Layer> BuildLayers(FunctionName activationFunction, ILayerInitializer initializer, int inputSize, int outputSize, params int[] hiddenLayerSizes)
        //{
        //    List<Layer> layers = new List<Layer>();
        //    
        //    for (int i = 0; i < hiddenLayerSizes.Length; i++)
        //    {
        //        int layerInputSize = i == 0 ? inputSize : hiddenLayerSizes[i - 1];
        //        var layerNeurons = new Neuron[hiddenLayerSizes[i]];
        //        layerNeurons.Initialize(() => new Neuron(new double[layerInputSize + 1]));
        //        var layer = new Layer(layerNeurons, activationFunction);
        //        initializer.Initialize(layer);
        //        layers.Add(layer);
        //    }
        //
        //    var lastLayerNeurons = new Neuron[outputSize];
        //    lastLayerNeurons.Initialize(() => new Neuron(new double[hiddenLayerSizes.Last() + 1]));
        //    var lastLayer = new Layer(lastLayerNeurons, activationFunction);
        //    initializer.Initialize(lastLayer);
        //    layers.Add(lastLayer);
        //    return layers;
        //}
    }

    public class Network
    {
        public Layer[] Layers { get; }
        public double[][] LastEvaluation { get; private set; }
        //public FunctionName ActivationFunctionName { get; }

        public Network(IEnumerable<Layer> layers)
        {
            //ActivationFunctionName = activationFunctionName;
            //IFunction activationFunction = Function.Get(ActivationFunctionName);
            //ILayerInitializer initializer = new He(0);
            //List<Layer> layers = new List<Layer>();
            //
            //for (int i = 0; i < weights.Length; i++)
            //{
            //    var layerNeurons = new Neuron[weights[i].Length];
            //
            //    for (int j = 0; j < weights[i].Length; j++)
            //    {
            //        layerNeurons[j] = new Neuron(weights[i][j]);
            //    }
            //
            //    layers.Add(new Layer(layerNeurons, activationFunction, initializer));
            //}

            //Layers = layers.ToArray();
            Layers = layers.ToArray();
            InitializeLastEvaluation();
        }

        //public Network(NetworkConfiguration configuration)
        //{
        //    ActivationFunctionName = configuration.ActivationFunction;
        //    IFunction activationFunction = Function.Get(ActivationFunctionName);
        //    int inputSize = configuration.InputSize;
        //    int outputSize = configuration.OutputSize;
        //    int[] hiddenLayerSizes = configuration.HiddenLayerSizes;
        //    List<Layer> layers = new List<Layer>();
        //    ILayerInitializer initializer = new He(0);
        //
        //    for (int i = 0; i < hiddenLayerSizes.Length; i++)
        //    {
        //        int layerInputSize = i == 0 ? inputSize : hiddenLayerSizes[i - 1];
        //        var layerNeurons = new Neuron[hiddenLayerSizes[i]];
        //        var layerNeurons = Enumerable.Range(0, hiddenLayerSizes[i]).Select(k => new Neuron(layerInputSize)).ToArray();
        //        layers.Add(new Layer(layerNeurons, activationFunction, initializer));
        //    }
        //
        //    var lastLayerNeurons = Enumerable.Range(0, outputSize).Select(k => new Neuron(hiddenLayerSizes.Last())).ToArray();
        //    layers.Add(new Layer(lastLayerNeurons, activationFunction, initializer));
        //    Layers = layers.ToArray();
        //    InitializeLastEvaluation();
        //}

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
