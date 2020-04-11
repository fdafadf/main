using System;
using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AgentState
    {
        public static readonly Sensor[] Sensors = new Sensor[]
        {
                new Sensor(new Vector2(15, 0), 0),
                new Sensor(new Vector2(15, 0), -60),
                new Sensor(new Vector2(15, 0), 60),
        };

        public static readonly int EncodedSize = Sensors.Length + 2;

        public double DirectionSin;
        public double DirectionCos;
        public double[] Signals;
        public double[] Encoded;

        public AgentState(double directionSin, double directionCos, params double[] signals)
        {
            DirectionSin = directionSin;
            DirectionCos = directionCos;
            Signals = signals;
            Encoded = new double[EncodedSize];
            Array.Copy(Signals, Encoded, Signals.Length);
            Encoded[Signals.Length + 0] = DirectionSin;
            Encoded[Signals.Length + 1] = DirectionCos;
        }
    }
}
