using OpenTK;

namespace Basics.Physics.Test.UI
{
    struct Vertex
    {
        public const int Size = (3 + 3 + 3) * 4; // size of struct in bytes

        public readonly Vector3 Position;
        public readonly Vector3 Color;
        public readonly Vector3 Normal;

        public Vertex(Vector3 position, Vector3 color, Vector3 normal)
        {
            this.Position = position;
            this.Color = color;
            this.Normal = normal;
        }
    }
}
