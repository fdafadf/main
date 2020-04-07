using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class Sensor
    {
        public Ant Agent;
        public Vector2 Position;
        public Vector2 Distance;
        public double Angle;
        public int Size = 10;

        public Sensor(Ant agent, Vector2 distance, double angle)
        {
            Agent = agent;
            Distance = distance;
            Angle = angle;
        }

        public void UpdatePosition()
        {
            Position = Distance.Rotate((Agent.DirectionAngle + Angle) % 360) + Agent.Position;
        }
    }
}
