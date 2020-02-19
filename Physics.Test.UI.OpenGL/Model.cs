using OpenTK.Graphics.OpenGL;
using System;

namespace Basics.Physics.Test.UI
{

    abstract class VertexArrayModel
    {

    }

    class RenderableModel : IRenderableModel, IDisposable
    {
        int vertexArray;
        int vertexBuffer;
        int indicesBuffer;
        int positionLayoutIndex;
        int colorLayoutIndex;
        int normalLayoutIndex;
        int vertexCount;

        public RenderableModel(Vertex[] vertices, uint[] indices, int positionLayoutIndex, int colorLayoutIndex, int normalLayoutIndex)
        {
            this.positionLayoutIndex = positionLayoutIndex;
            this.colorLayoutIndex = colorLayoutIndex;
            this.normalLayoutIndex = normalLayoutIndex;
            this.vertexCount = indices.Length;
            int stride = Vertex.Size;
            int verticesSize = vertices.Length * Vertex.Size;
            this.Initialize(vertices, verticesSize, indices, stride);
        }

        public RenderableModel(float[] vertices, uint[] indices, int positionLayoutIndex, int colorLayoutIndex, int normalLayoutIndex)
        {
            this.positionLayoutIndex = positionLayoutIndex;
            this.colorLayoutIndex = colorLayoutIndex;
            this.normalLayoutIndex = normalLayoutIndex;
            this.vertexCount = 3;
            int stride = 6 * sizeof(float);
            int verticesSize = vertices.Length * sizeof(float);
            this.Initialize(vertices, verticesSize, indices, stride);
        }

        private void Initialize<T>(T[] vertices, int verticesSize, uint[] indices, int stride) where T : struct
        {
            this.vertexArray = GL.GenVertexArray();

            GL.BindVertexArray(this.vertexArray);

            this.vertexBuffer = GL.GenBuffer();
            this.indicesBuffer = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, verticesSize, vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(positionLayoutIndex, 3, VertexAttribPointerType.Float, false, stride, 0);
            GL.VertexAttribPointer(colorLayoutIndex, 3, VertexAttribPointerType.Float, true, stride, 3 * sizeof(float));
            GL.VertexAttribPointer(normalLayoutIndex, 3, VertexAttribPointerType.Float, true, stride, 6 * sizeof(float));

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, this.indicesBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(colorLayoutIndex);
            GL.EnableVertexAttribArray(positionLayoutIndex);
            GL.EnableVertexAttribArray(normalLayoutIndex);
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
