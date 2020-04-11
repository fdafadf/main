using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class Sensor
    {
        public Vector2 Distance;
        public double Angle;
        public int Size = 10;

        public Sensor(Vector2 distance, double angle)
        {
            Distance = distance;
            Angle = angle;
        }
    }
}
