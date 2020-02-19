using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Basics.Physics.Test.UI
{
    class SceneObject : IRenderable
    {
        public Vector3 Position;
        public Vector3 PositionMove = Vector3.Zero;
        public float RotationY;
        public float RotationYMove;
        IRenderableModel model;
        //int modelMatrixUniformIndex;

        public SceneObject(IRenderableModel model, Vector3 position)
        {
            this.model = model;
            //this.modelMatrixUniformIndex = modelMatrixUniformIndex;
            this.Position = position;
        }

        public void Update()
        {
            if (this.PositionMove != Vector3.Zero)
            {
                this.Position = this.Position + this.PositionMove;
            }

            this.RotationY = this.RotationY + this.RotationYMove;
        }

        Matrix4 t;
        Matrix4 rotX;
        Matrix4 rotZ;
        Matrix4 modelMatrix;

        public void Render(int modelUniformIndex)
        {
            Matrix4.CreateTranslation(ref this.Position, out t);
            Matrix4.CreateRotationX(this.Position.Z, out rotX);
            Matrix4.CreateRotationZ(this.Position.X, out rotZ);
            Matrix4.Mult(ref t, ref rotX, out modelMatrix);
            Matrix4.Mult(ref rotZ, ref modelMatrix, out modelMatrix);
            this.model.Bind();
            GL.UniformMatrix4(modelUniformIndex, true, ref modelMatrix);
            model.Render();
        }
    }
}
