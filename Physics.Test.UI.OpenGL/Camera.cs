using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using WindowsFormsApp1;

namespace Basics.Physics.Test.UI
{
    class CameraFollow : ICamera
    {
        public Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
        //int projectionMatrixUniformIndex;
        //int viewMatrixUniformIndex;
        public CameraData Data { get; }
        //public Matrix4 ProjectionMatrix { get; }
        //public Matrix4 ViewMatrix { get; set; }
        //private int lastMouseX;
        SceneSphere sceneObject;
        Vector3 cameraFront = new Vector3();
        public Vector3 cameraUp = Vector3.UnitY;
        public Vector3 cameraRight = Vector3.UnitX;

        public CameraFollow(int width, int height, SceneSphere sceneObject)
        {
            Data = new CameraData();
            //this.projectionMatrixUniformIndex = projectionMatrixUniformIndex;
            //this.viewMatrixUniformIndex = viewMatrixUniformIndex;
            Data.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, width / (float)height, 0.01f, 100f);
            //lastMouseX = Mouse.GetState().X;
            this.sceneObject = sceneObject;
        }

        //public void Bind()
        //{
        //    Matrix4 viewMatrix = Matrix4.LookAt(this.position, this.position + this.cameraFront, this.cameraUp);
        //    GL.UniformMatrix4(this.projectionMatrixUniformIndex, true, ref projectionMatrix);
        //    GL.UniformMatrix4(this.viewMatrixUniformIndex, true, ref viewMatrix);
        //}

        Vector3 to = new Vector3();

        public void Update()
        {
            to.X = sceneObject.Position.X;// - sceneObject.Sphere.Direction.X / 100.0f;
            to.Z = sceneObject.Position.Z;// - sceneObject.Sphere.Direction.Z / 100.0f;
            position.X = (to.X + position.X * 100) / 101.0f;
            position.Z = (to.Z + position.Z * 100) / 101.0f;

            cameraFront.X = sceneObject.Position.X - position.X;
            cameraFront.Y = sceneObject.Position.Y - position.Y;
            cameraFront.Z = sceneObject.Position.Z - position.Z;
            cameraFront.Normalize();
            cameraRight = Vector3.Normalize(Vector3.Cross(cameraFront, Vector3.UnitY));
            cameraUp = Vector3.Normalize(Vector3.Cross(cameraRight, this.cameraFront));

            

            Data.ViewMatrix = Matrix4.LookAt(this.position, this.position + this.cameraFront, this.cameraUp);
        }
    }

    class Camera : ICamera
    {
        public Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
        private float positionRotationX;
        private float positionRotationY = -MathHelper.PiOver2;
        public Vector3 cameraFront = -Vector3.UnitZ;
        public Vector3 cameraUp = Vector3.UnitY;
        public Vector3 cameraRight = Vector3.UnitX;

        //int projectionMatrixUniformIndex;
        //int viewMatrixUniformIndex;
        //public Matrix4 ProjectionMatrix { get; }
        //public Matrix4 ViewMatrix { get; set; }
        public CameraData Data { get; }
        float speed = 0.1f;

        private int lastMouseX;

        public Camera(int width, int height)
        {
            Data = new CameraData();
            //this.projectionMatrixUniformIndex = projectionMatrixUniformIndex;
            //this.viewMatrixUniformIndex = viewMatrixUniformIndex;
            Data.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, width / (float)height, 0.01f, 100f);
            lastMouseX = Mouse.GetState().X;
        }

        public void Update()
        {
            var input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Up) || input.IsKeyDown(Key.W))
            {
                this.position = this.position + this.cameraFront * speed;
            }

            if (input.IsKeyDown(Key.Down) || input.IsKeyDown(Key.S))
            {
                this.position = this.position - this.cameraFront * speed;
            }

            int mouseX = Mouse.GetState().X;

            if (mouseX != this.lastMouseX)
            {
                this.positionRotationY += 0.01f * (mouseX - lastMouseX);
                this.lastMouseX = mouseX;
            }

            if (input.IsKeyDown(Key.Left) || input.IsKeyDown(Key.A))
            {
                this.position = this.position - this.cameraRight * speed;
                //this.positionRotationY = MathHelper.DegreesToRadians(MathHelper.RadiansToDegrees(this.positionRotationY) - 1.0f);
            }

            if (input.IsKeyDown(Key.Right) || input.IsKeyDown(Key.D))
            {
                this.position = this.position + this.cameraRight * speed;
                //this.positionRotationY = MathHelper.DegreesToRadians(MathHelper.RadiansToDegrees(this.positionRotationY) + 1.0f);
            }

            this.cameraFront.X = (float)Math.Cos(this.positionRotationX) * (float)Math.Cos(this.positionRotationY);
            this.cameraFront.Y = (float)Math.Sin(this.positionRotationX);
            this.cameraFront.Z = (float)Math.Cos(this.positionRotationX) * (float)Math.Sin(this.positionRotationY);
            this.cameraFront = Vector3.Normalize(this.cameraFront);
            this.cameraRight = Vector3.Normalize(Vector3.Cross(this.cameraFront, Vector3.UnitY));
            this.cameraUp = Vector3.Normalize(Vector3.Cross(this.cameraRight, this.cameraFront));

            Data.ViewMatrix = Matrix4.LookAt(this.position, this.position + this.cameraFront, this.cameraUp);
        }

        //public void Bind()
        //{
        //    //this.shader.SetMatrix4("view", Matrix4.LookAt(this.position, this.position + this.cameraFront, this.cameraUp));
        //    Matrix4 viewMatrix = Matrix4.LookAt(this.position, this.position + this.cameraFront, this.cameraUp);
        //
        //    GL.UniformMatrix4(this.projectionMatrixUniformIndex, true, ref projectionMatrix);
        //    GL.UniformMatrix4(this.viewMatrixUniformIndex, true, ref viewMatrix);
        //
        //
        //}
    }
}
