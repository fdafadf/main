using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Basics.Physics.Test.UI
{

    class TestModelLoader
    {
        public void Test1()
        {

            TexturedVertex[] tvs = new TexturedVertex[4];
            tvs[0] = new TexturedVertex(new Vector3(-10, -0.2f, -10), new Vector2(0, 0));
            tvs[1] = new TexturedVertex(new Vector3(-10, -0.2f, +10), new Vector2(0, 255.0f));
            tvs[2] = new TexturedVertex(new Vector3(+10, -0.2f, +10), new Vector2(255.0f, 255.0f));
            tvs[3] = new TexturedVertex(new Vector3(+10, -0.2f, -10), new Vector2(255.0f, 0));
            uint[] tis = { 0, 1, 2, 0, 3, 2 };

        }

        static void Mul(ref WindowsFormsApp1.Matrix3d m, ref Vector3 v)
        {
            WindowsFormsApp1.Vector3d v2 = new WindowsFormsApp1.Vector3d(v.X, v.Y, v.Z);
            v2.Mul(m);
            v.X = v2.X;
            v.Y = v2.Y;
            v.Z = v2.Z;
        }

        public void LoadTrack(TexturedObjectGroup texturedGroup, Texture texture)
        {
            WindowsFormsApp1.TestModel testModel = WindowsFormsApp1.Form1.CreateTestModel2();
            List<TexturedVertex> trackVertices = new List<TexturedVertex>();
            var minX = testModel.Vertices.Min(v => v.X);
            var maxX = testModel.Vertices.Max(v => v.X);
            var minZ = testModel.Vertices.Min(v => v.Z);
            var maxZ = testModel.Vertices.Max(v => v.Z);

            for (int i = 0; i < testModel.Vertices.Count; i++)
            {
                var vertex = testModel.Vertices[i];
                float u = ((i % 4) & 1) != 0 ? 255 : 0;
                float v = ((i % 4) & 2) != 0 ? 255 : 0;
                Vector3 position = new Vector3(vertex.X, vertex.Y, vertex.Z);
                TexturedVertex texturedVertex = new TexturedVertex(position, new Vector2(u, v));
                trackVertices.Add(texturedVertex);
            }

            TexturedModel texturedModel = texturedGroup.Add(trackVertices.ToArray(), testModel.Indices.ToArray());
            texturedGroup.Add(texturedModel, texture.Handle, new Vector3());

                /*
            List<TexturedVertex> trackVertices = new List<TexturedVertex>();
            List<uint> trackIndices = new List<uint>();
            Random random = new Random((int)DateTime.Now.Ticks);
            TexturedVertex a = new TexturedVertex(new Vector3(-2, -0.2f, 0), new Vector2(0, 0));
            TexturedVertex b = new TexturedVertex(new Vector3(2, -0.2f, 0), new Vector2(255, 0));
            trackVertices.Add(a);
            trackVertices.Add(b);
            WindowsFormsApp1.Vector3d delta = new WindowsFormsApp1.Vector3d(0, 0, 10);
            WindowsFormsApp1.Matrix3d matrix = WindowsFormsApp1.Matrix3d.RotationY(5 / 57.29577f);

            for (uint i = 0; i < 1; i++)
            {
                uint v = (i + 1) * 120 % 255;
                delta.Mul(matrix);

                Vector3 d = new Vector3(b.Position.X + delta.X, b.Position.Y + delta.Y, b.Position.Z + delta.Z);
                TexturedVertex texturedVertexD = new TexturedVertex(d, new Vector2(255, v));
                PointF dp = new PointF(b.Position.X, b.Position.Z);
                PointF dv = new PointF(b.Position.X + delta.Z, b.Position.Z - delta.X);
                PointF ap = new PointF(a.Position.X, a.Position.Z);
                PointF av = new PointF(a.Position.X + delta.X, a.Position.Z + delta.Z);
                PointF p = MathHelper2.Intersection(dp, dv, ap, av);
                Vector3 c = new Vector3(p.X, a.Position.Y, p.Y);
                TexturedVertex texturedVertexC = new TexturedVertex(c, new Vector2(0, v));
                trackVertices.Add(texturedVertexC);
                trackVertices.Add(texturedVertexD);
                trackIndices.Add(2 + i * 2);
                trackIndices.Add(0 + i * 2);
                trackIndices.Add(1 + i * 2);
                trackIndices.Add(2 + i * 2);
                trackIndices.Add(1 + i * 2);
                trackIndices.Add(3 + i * 2);
                a = texturedVertexC;
                b = texturedVertexD;
            }

            TexturedModel texturedModel = texturedGroup.Add(trackVertices.ToArray(), trackIndices.ToArray());
            texturedGroup.Add(texturedModel, texture.Handle, new Vector3());
            */
        }
    }
}
