using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class AntSensor
    {
        public Ant Ant;
        public Vector2 Position;
        public Sensor Sensor;

        public AntSensor(Ant ant, Sensor sensor)
        {
            Ant = ant;
            Sensor = sensor;
        }

        public void UpdatePosition()
        {
            Position = Sensor.Distance.Rotate((Ant.DirectionAngle + Sensor.Angle) % 360) + Ant.Position;
        }
    }
}
