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
        public Sensor[] Sensors;

        public Ant(float x, float y, float dx, float dy)
        {
            Position = new Vector2(x, y);
            Direction = new Vector2(dx, dy);
            Velocity = new Vector2(dx, dy);
            Goal = new Vector2(120, 120);
            Sensors = new Sensor[]
            {
                new Sensor(this, new Vector2(15, 0), 0),
                new Sensor(this, new Vector2(15, 0), -60),
                new Sensor(this, new Vector2(15, 0), 60),
            };
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
