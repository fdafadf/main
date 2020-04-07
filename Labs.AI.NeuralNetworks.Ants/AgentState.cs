using System;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AgentState
    {
        public double DirectionSin;
        public double DirectionCos;
        public double[] Signals;
        public double[] Encoded;

        public AgentState(double directionSin, double directionCos, params double[] signals)
        {
            DirectionSin = directionSin;
            DirectionCos = directionCos;
            Signals = signals;
            Encoded = new double[signals.Length + 2];
            Array.Copy(Signals, Encoded, Signals.Length);
            Encoded[Signals.Length + 0] = DirectionSin;
            Encoded[Signals.Length + 1] = DirectionCos;
        }
    }
}
