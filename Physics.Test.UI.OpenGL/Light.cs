using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Basics.Physics.Test.UI
{
    class Light : ILight
    {
        private Vector3 position;
        private ShaderProgram shaderProgram;
        private int uniformLocation;

        public Light(ShaderProgram shaderProgram, int uniformLocation)
        {
            this.shaderProgram = shaderProgram;
            this.uniformLocation = uniformLocation;
        }

        public Vector3 Position
        {
            set
            {
                GL.UseProgram(shaderProgram.Handle);
                GL.Uniform3(this.uniformLocation, this.position = value);
            }
            get
            {
                return this.position;
            }
        }
    }
}
