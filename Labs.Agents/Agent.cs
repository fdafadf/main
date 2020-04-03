using System.Linq;
using System.Numerics;

namespace Labs.Agents
{
    public class Agent
    {
        public Circle SceneObject;
        public Vector2 Velocity;
        public Vector2 Target;

        public Agent(Vector2 position, int size)
        {
            SceneObject = new Circle(position, size);
            Target = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        public void Update(Simulation simulation, float t)
        {
            //Velocity = Vector2.Normalize(Target - SceneObject.Position);
            //return;

            var rotations = simulation.Rotations.Select(rotation => rotation * t).Where(target => {
                var originalPosition = SceneObject.Position;
                SceneObject.Position = SceneObject.Position + target;
                var collide = simulation.Scene.Collide(SceneObject);
                SceneObject.Position = originalPosition;
                return collide == false;
            });

            if (rotations.Any())
            {
                Velocity = rotations.MinBy(rotation => Vector2.Distance(SceneObject.Position + rotation, Target));
            }
            else
            {
                Velocity = Vector2.Zero;
            }
        }
    }
}
