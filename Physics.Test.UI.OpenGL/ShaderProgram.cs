using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace Basics.Physics.Test.UI
{
    class ShaderProgram : IDisposable
    {
        public static ShaderProgram Load(string vertexSource, string fragmentSource, string projectionUniformName, string viewUniformName)
        {
            using (Shader vertexShader = new Shader(ShaderType.VertexShader, vertexSource))
            {
                using (Shader fragmentShader = new Shader(ShaderType.FragmentShader, fragmentSource))
                {
                    ShaderProgram shaderProgram = new ShaderProgram(projectionUniformName, viewUniformName, vertexShader, fragmentShader);
                    GL.UseProgram(shaderProgram.Handle);
                    //Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, Width / (float)Height, 0.01f, 100f);
                    //this.disposables.Add(this.shaderProgram);
                    return shaderProgram;
                }
            }
        }

        public readonly int Handle;
        public readonly Dictionary<string, int> UniformLocations = new Dictionary<string, int>();
        //public readonly ShaderUniform Uniform;
        //public readonly ShaderLayout Layout;
        int projectionMatrixUniformIndex;
        int viewMatrixUniformIndex;
        //public int ModelUniformIndex { get; }

        public ShaderProgram(string projectionUniformName, string viewUniformName, params Shader[] shaders)
        {
            this.Handle = GL.CreateProgram();

            foreach (Shader shader in shaders)
            {
                GL.AttachShader(this.Handle, shader.Handle);
            }

            GL.LinkProgram(this.Handle);
            GL.GetProgram(this.Handle, GetProgramParameterName.LinkStatus, out var code);

            if (code != (int)All.True)
            {
                // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                throw new Exception($"Error occurred whilst linking Program({this.Handle})");
            }

            foreach (Shader shader in shaders)
            {
                GL.DetachShader(this.Handle, shader.Handle);
            }

            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
            
            for (var i = 0; i < numberOfUniforms; i++)
            {
                var key = GL.GetActiveUniform(Handle, i, out _, out _);
                var location = GL.GetUniformLocation(Handle, key);
                UniformLocations.Add(key, location);
            }

            projectionMatrixUniformIndex = this.UniformLocations[projectionUniformName];
            viewMatrixUniformIndex = this.UniformLocations[viewUniformName];
            //ModelUniformIndex = this.UniformLocations[modelUniformName];
            //Uniform = new ShaderUniform(this, uniformNames);
            //Layout = new ShaderLayout(this, layoutNames);
        }

        // TODO: w złym miejscu
        public void Bind(ICamera camera)
        {
            GL.UseProgram(this.Handle);
            GL.UniformMatrix4(projectionMatrixUniformIndex, true, ref camera.Data.ProjectionMatrix);
            GL.UniformMatrix4(viewMatrixUniformIndex, true, ref camera.Data.ViewMatrix);
        }

        public void Dispose()
        {
            GL.DeleteProgram(this.Handle);
        }
    }
}
