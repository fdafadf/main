using OpenTK.Graphics.OpenGL;
using System;

namespace Basics.Physics.Test.UI
{
    //class ColoredShader : Shader
    //{
    //
    //    public ColoredShader(string source)
    //    {
    //        Model = this.shaderProgram.UniformLocations["model"];
    //        Light = this.shaderProgram.UniformLocations["lightPos"];
    //        Position = GL.GetAttribLocation(this.shaderProgram.Handle, "aPosition");
    //        Color = GL.GetAttribLocation(this.shaderProgram.Handle, "vertexColor");
    //        Normal = GL.GetAttribLocation(this.shaderProgram.Handle, "vertexNormal");
    //    }
    //}

    class ShaderProgramDefinition
    {
    }

    class ShaderUniformNames
    {
        public string Model;
        public string Light;
        public string Projection;
        public string View;
    }

    class ShaderUniform
    {
        public int? Model;
        public int? Light;
        public int? Projection;
        public int? View;

        public ShaderUniform(ShaderProgram shaderProgram, ShaderUniformNames names)
        {
            if (names.Model != null)
            {
                Model = shaderProgram.UniformLocations[names.Model];
            }

            if (names.Light != null)
            {
                Light = shaderProgram.UniformLocations[names.Light];
            }

            if (names.Projection != null)
            {
                Projection = shaderProgram.UniformLocations[names.Projection];
            }

            if (names.View != null)
            {
                View = shaderProgram.UniformLocations[names.View];
            }
        }
    }

    class ShaderLayoutNames
    {
        public string Position;
        public string Color;
        public string Normal;
    }

    class ShaderLayout
    {
        public int? Position;
        public int? Color;
        public int? Normal;

        public ShaderLayout(ShaderProgram shaderProgram, ShaderLayoutNames names)
        {
            if (names.Position != null)
            {
                Position = shaderProgram.UniformLocations[names.Position];
            }

            if (names.Color != null)
            {
                Color = shaderProgram.UniformLocations[names.Color];
            }

            if (names.Normal != null)
            {
                Normal = shaderProgram.UniformLocations[names.Normal];
            }
        }
    }

    class Shader : IDisposable
    {
        public readonly int Handle;

        public Shader(ShaderType type, string source)
        {
            this.Handle = GL.CreateShader(type);
            GL.ShaderSource(this.Handle, source);
            GL.CompileShader(this.Handle);
            GL.GetShader(this.Handle, ShaderParameter.CompileStatus, out var code);

            if (code != (int)All.True)
            {
                // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
                throw new Exception($"Error occurred whilst compiling Shader ({this.Handle})");
            }
        }

        public void Dispose()
        {
            GL.DeleteShader(this.Handle);
        }
    }
}
