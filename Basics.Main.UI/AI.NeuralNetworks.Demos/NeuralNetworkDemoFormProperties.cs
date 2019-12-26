using Basics.Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Basics.AI.NeuralNetworks.Demos
{
    public class NeuralNetworkDemoFormProperties
    {
        [Category("Architecture")]
        public int NetworkInputSize { get; set; }
        [Category("Architecture")]
        public int[] NetworkLayersSize { get; set; }
        [Category("Randomizer")]
        public int Seed { get; set; }
        [Category("Training")]
        [TypeConverter(typeof(TrainingSetConverter))]
        public string TrainingSet { get; set; }
        [Category("Randomizer")]
        public int TrainingSetSize { get; set; }
        [Browsable(false)]
        public List<NamedObject<Func<IEnumerable<NeuralIO>>>> TrainingSets = new List<NamedObject<Func<IEnumerable<NeuralIO>>>>();
        [Category("Training")]
        public int TrainingEpoches { get; set; }
        [Category("Training")]
        public double TrainingAlpha { get; set; }
        [Category("Training")]
        public double TrainingLastError { get; set; }

        public void AddTrainingSet(string name, Func<IEnumerable<NeuralIO>> loader)
        {
            TrainingSets.Add(new NamedObject<Func<IEnumerable<NeuralIO>>>(name, loader));

            if (TrainingSet == null)
            {
                TrainingSet = name;
            }
        }
    }
}
