using System.Linq;
using System.Numerics;

namespace Labs.AI.NeuralNetworks.Ants
{
    public class Ant
    {
        public Vector2 Position;
        public Vector2 Direction;
        public int Size = 5;
        public Vector2 Goal;
        public Vector2 Velocity;
        public double DirectionAngle;
        public AntSensor[] Sensors;

        public Ant(float x, float y, float dx, float dy, Sensor[] sensors)
        {
            Position = new Vector2(x, y);
            Direction = new Vector2(dx, dy);
            Velocity = new Vector2(dx, dy);
            Goal = new Vector2(120, 120);
            Sensors = sensors.Select(sensor => new AntSensor(this, sensor)).ToArray();
        }

        public double GetDistanceToGoal()
        {
            return Position.Distance(Goal);
        }

        public void Update(AgentAction action)
        {
            double rotation = AgentAction.Rotations[action.Rotation];
            Direction = Direction.Rotate(rotation);
            DirectionAngle += rotation;

            if (action.Forward)
            {
                Velocity = Direction;
            }
            else
            {
                Velocity = Vector2.Zero;
            }
        }
    }
}
