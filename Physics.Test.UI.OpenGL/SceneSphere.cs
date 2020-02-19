using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using WindowsFormsApp1;

namespace Basics.Physics.Test.UI
{
    class SceneSphere : IRenderable
    {
        IRenderableModel model;
        public Sphere3d Sphere;
        //int modelMatrixUniformIndex;

        public SceneSphere(IRenderableModel model, Sphere3d sphere)
        {
            this.model = model;
            this.Sphere = sphere;
            UpdatePosition();
        }

        Matrix4 rot = Matrix4.Identity;
        Matrix4 positionMatrix;
        Matrix4 scaleMatrix;
        Matrix4 modelMatrix;
        public Vector3 Position = new Vector3();
        Vector3 PrevPosition = new Vector3();

        void UpdatePosition()
        {
            PrevPosition = Position;
            Position.X = Sphere.Position.X;
            Position.Y = Sphere.Position.Y;
            Position.Z = Sphere.Position.Z;
        }

        public void UpdateRotation()
        {
            float radius = Sphere.Radius;
            float pir2 = 2 * (float)Math.PI * radius;
            Vector3 d = Position - PrevPosition;
            float ax = (360.0f / pir2) * d.Z;
            float az = (360.0f / pir2) * -d.X;
            Matrix4 rotXd;
            Matrix4 rotZd;
            Matrix4 rotd;
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(ax), out rotXd);
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(az), out rotZd);
            Matrix4.Mult(ref rotZd, ref rotXd, out rotd);
            Matrix4.Mult(ref rot, ref rotd, out rot);
            return;


            //float radius = Sphere.Radius * 0.01f;
            ////float rx = 2.0f * t * (float)Math.Cos(Sphere.RotationY);
            ////float rz = 2.0f * t * (float)Math.Sin(Sphere.RotationY);
            //WindowsFormsApp1.Vector3d direction = new WindowsFormsApp1.Vector3d(Sphere.Direction);
            //direction.Normalize();
            //float rx = t * -direction.X;
            //float rz = t * direction.Z;
            //Matrix4 rotXd;
            //Matrix4 rotZd;
            //Matrix4 rotd;
            //Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rx), out rotXd);
            //Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rz), out rotZd);
            ////Matrix4.CreateRotationX(rx, out rotXd);
            ////Matrix4.CreateRotationZ(rz, out rotZd);
            //Matrix4.Mult(ref rotZd, ref rotXd, out rotd);
            //Matrix4.Mult(ref rot, ref rotd, out rot);
        }

        public void Render(int modelUniformIndex)
        {
            UpdatePosition();
            float radius = Sphere.Radius;
            Matrix4.CreateTranslation(ref Position, out positionMatrix);
            Matrix4.CreateScale(radius, out scaleMatrix);
            Matrix4.Mult(ref rot, ref scaleMatrix, out modelMatrix);
            Matrix4.Mult(ref modelMatrix, ref positionMatrix, out modelMatrix);
            this.model.Bind();
            GL.UniformMatrix4(modelUniformIndex, true, ref modelMatrix);
            model.Render();
        }
    }
}
