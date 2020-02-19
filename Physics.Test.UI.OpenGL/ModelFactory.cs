using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Basics.Physics.Test.UI
{/*
    class RenderableModelCollection //: IDisposable
    {


        //public Model Test()
        //{
        //    float[] vertices = new float[] {
        //        -0.8f, 0.8f, 0.4f, 1.0f, 0.0f, 0.6f, 0, 0, 1.0f,
        //        -0.6f, -0.4f, 0.4f, -0.2f, 0.5f, 1.0f, 0, 0, 1.0f,
        //        1.0f, 0.2f, 0.4f, -1.0f, 0.0f, 1.0f, 0, 0, 1.0f
        //    };
        //    uint[] indices = new uint[] { 0, 1, 2 };
        //
        //    return new Model(vertices, indices, this.positionLayoutIndex, this.colorLayoutIndex, this.normalLayoutIndex);
        //}

        //public Model CreateOctahedron(int subdivisions)
        //{
        //    int resolution = 1 << subdivisions;
        //    Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1) * 4 - (resolution * 2 - 1) * 3];
        //    int[] triangles = new int[(1 << (subdivisions * 2 + 3)) * 3];
        //    Octahedron.CreateOctahedron(vertices, triangles, resolution);
        //    Vertex[] vertices2 = new Vertex[vertices.Length];
        //    Vector3 color = new Vector3(1, 1, 0);
        //
        //    for (int i = 0; i < vertices2.Length; i++)
        //    {
        //        vertices2[i] = new Vertex(vertices[i], color, vertices[i]);
        //    }
        //
        //    uint[] indices = triangles.Select(t => (uint)t).ToArray();
        //
        //    return new Model(vertices2, indices, this.positionLayoutIndex, this.colorLayoutIndex, this.normalLayoutIndex);
        //}

        public RenderableModel Add(WavefrontModel model)
        {
        }
    }

    class RenderableVertexModelFactory 
    {
        int positionLayoutIndex;
        int colorLayoutIndex;
        int normalLayoutIndex;

        public RenderableVertexModelFactory(ShaderProgram shaderProgram)
        {
            this.positionLayoutIndex = shaderProgram.Layout.Position.Value;
            this.colorLayoutIndex = shaderProgram.Layout.Color.Value;
            this.normalLayoutIndex = shaderProgram.Layout.Normal.Value;
        }

        public RenderableVertexModelFactory(int positionLayoutIndex, int colorLayoutIndex, int normalLayoutIndex)
        {
            this.positionLayoutIndex = positionLayoutIndex;
            this.colorLayoutIndex = colorLayoutIndex;
            this.normalLayoutIndex = normalLayoutIndex;
        }

        public RenderableModel Create(WavefrontModel model)
        {
            List<Vertex> vertices = new List<Vertex>();
            List<uint> indices = new List<uint>();

            foreach (var face in model.Faces)
            {
                indices.Add((uint)indices.Count);
                Vector3 color = new Vector3(model.Normals[face.a.n - 1]);
                color.X = Math.Abs(color.X);
                color.Y = Math.Abs(color.Y);
                color.Z = Math.Abs(color.Z);
                vertices.Add(new Vertex(model.Vertices[face.a.v - 1], color, model.Normals[face.a.n - 1]));
            }

            return new RenderableModel(vertices.ToArray(), indices.ToArray(), this.positionLayoutIndex, this.colorLayoutIndex, this.normalLayoutIndex);
        }
    }*/
}
