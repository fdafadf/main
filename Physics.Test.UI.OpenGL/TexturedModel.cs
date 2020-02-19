using OpenTK.Graphics.OpenGL;
using System;

namespace Basics.Physics.Test.UI
{
    class TexturedModel : IRenderableModel, IDisposable
    {
        int vertexArray;
        int vertexBuffer;
        int indicesBuffer;
        //int positionLayoutIndex;
        //int textureLayoutIndex;
        //int normalLayoutIndex;
        int vertexCount;

        public TexturedModel(TexturedVertex[] vertices, uint[] indices, int positionLayoutIndex, int textureLayoutIndex)
        {
            int stride = TexturedVertex.Size;
            int verticesSize = vertices.Length * TexturedVertex.Size;

            this.vertexCount = indices.Length;
            this.vertexArray = GL.GenVertexArray();

            GL.BindVertexArray(this.vertexArray);

            this.vertexBuffer = GL.GenBuffer();
            this.indicesBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, verticesSize, vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(positionLayoutIndex, 3, VertexAttribPointerType.Float, false, stride, 0);
            GL.VertexAttribPointer(textureLayoutIndex, 2, VertexAttribPointerType.Float, false, stride, 3 * sizeof(float));
            //GL.VertexAttribPointer(normalLayoutIndex, 3, VertexAttribPointerType.Float, true, stride, 6 * sizeof(float));

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indicesBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(textureLayoutIndex);
            GL.EnableVertexAttribArray(positionLayoutIndex);
            //GL.EnableVertexAttribArray(normalLayoutIndex);
        }

        public void Bind()
        {
            GL.BindVertexArray(this.vertexArray);
        }

        public void Render()
        {
            GL.DrawElements(PrimitiveType.Triangles, this.vertexCount, DrawElementsType.UnsignedInt, 0);
        }

        public void Dispose()
        {
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(this.vertexArray);
            GL.DeleteBuffer(this.vertexBuffer);
            GL.DeleteBuffer(this.indicesBuffer);
            GL.UseProgram(0);
        }
    }
}
