using AI.NeuralNetworks;
using System;

namespace Pathfinder
{
    class TrainingMonitor : MeanSquareErrorMonitor
    {
        string Name;

        public TrainingMonitor(string name)
        {
            Name = name;
        }

        protected override void OnEpoch(double meanSquareError)
        {
            Console.WriteLine($"{Name}: {meanSquareError}");
        }
    }
}
