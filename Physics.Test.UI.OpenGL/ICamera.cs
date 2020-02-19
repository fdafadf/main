using OpenTK;

namespace Basics.Physics.Test.UI
{
    interface ICamera
    {
        CameraData Data { get; }
        void Update();
        //void Bind();
    }

    class CameraData
    {
        public Matrix4 ProjectionMatrix;
        public Matrix4 ViewMatrix;
    }
}
