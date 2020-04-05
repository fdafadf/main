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
            //UpdateV1();
            //UpdateV2(simulation, t);
            UpdateV3(simulation, t);
        }

        private void UpdateV1()
        {
            Velocity = Vector2.Normalize(Target - SceneObject.Position);
        }

        private const int Directions = 4;
        private readonly Vector2[] RotationsV2 = Enumerable.Range(0, Directions).Select(i => Vector2.UnitX.Rotate(360.0f / Directions * i)).ToArray();

        private void UpdateV2(Simulation simulation, float t)
        {
            UpdateVelocity(simulation, t, RotationsV2);
        }

        private Vector2[] RotationsV3 = new Vector2[3];

        private void UpdateV3(Simulation simulation, float t)
        {
            if (Velocity == Vector2.Zero)
            {
                Velocity = Vector2.UnitX;
            }

            RotationsV3[0] = Velocity;
            RotationsV3[1] = Velocity.Rotate(90);
            RotationsV3[2] = Velocity.Rotate(-90);
            UpdateVelocity(simulation, t, RotationsV3);
        }

        private void UpdateVelocity(Simulation simulation, float t, Vector2[] rotations)
        {
            var rotations2 = rotations.Select(rotation => rotation * t).Where(target => {
                var originalPosition = SceneObject.Position;
                SceneObject.Position = SceneObject.Position + target;
                var collide = simulation.Scene.Collide(SceneObject);
                SceneObject.Position = originalPosition;
                return collide == false;
            });

            if (rotations2.Any())
            {
                Velocity = rotations2.MinBy(rotation => Vector2.Distance(SceneObject.Position + rotation, Target));
            }
            else
            {
                Velocity = Vector2.Zero;
            }
        }
    }
}
