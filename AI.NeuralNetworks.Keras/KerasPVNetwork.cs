using AI.MonteCarlo;
using Games;
using System;
using System.Collections.Generic;

namespace AI.Keras
{
    public abstract class KerasPVNetwork<TState, TAction, TOutput> : IPVNetwork<TState, TAction>
        where TState : IPeriodState
    {
        public KerasModel Model { get; }

        public KerasPVNetwork(KerasModel model)
        {
            Model = model;
        }

        public abstract PVNetworkOutput<TAction> Predict(TState gameState);
        public abstract IEnumerable<PVNetworkOutput<TAction>> Predict(IEnumerable<TState> gameState);
        protected abstract double[] TransformInput(TState state);
        protected abstract double[] GetTrainingOutput(PVNetworkBasedMCTreeSearchNode<TState, TAction> node, PVNetworkBasedMCTreeSearchNode<TState, TAction> finalNode);

        public void Train(IEnumerable<PVNetworkBasedMCTreeSearchNode<TState, TAction>> finalNodes, int epoches)
        {
            List<double[]> inputs = new List<double[]>();
            List<double[]> outputs = new List<double[]>();

            foreach (var finalNode in finalNodes)
            {
                PVNetworkBasedMCTreeSearchNode<TState, TAction> node = finalNode;
                
                while (node != null)
                {
                    double[] trainingOutput = GetTrainingOutput(node, finalNode);
                    ConsoleUtility.WriteLine(node, trainingOutput);
                    Console.ReadLine();
                    inputs.Add(TransformInput(node.State));
                    outputs.Add(trainingOutput);
                    node = node.Parent;
                }
            }

            Model.Train(inputs.ToArray(), outputs.ToArray(), epoches);
        }
    }
}
