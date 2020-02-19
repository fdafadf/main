using OpenTK;

namespace Basics.Physics.Test.UI
{
    public struct TexturedVertex
    {
        public const int Size = (3 + 2) * 4; // size of struct in bytes

        public readonly Vector3 Position;
        private readonly Vector2 _textureCoordinate;

        public TexturedVertex(Vector3 position, Vector2 textureCoordinate)
        {
            Position = position;
            _textureCoordinate = textureCoordinate;
        }
    }
}
