using OpenTK;
using System;
using System.Collections.Generic;

namespace Basics.Physics.Test.UI
{
    class SphereModel
    {
        private static Vector3[] ComputeIcosahedronVertices(double radius)
        {
            //const double PI = 3.1415926f;
            double H_ANGLE = Math.PI / 180 * 72;    // 72 degree = 360 / 5
            double V_ANGLE = Math.Atan(1.0f / 2);  // elevation = 26.565 degree

            double[] vertices = new double[12 * 3];    // 12 vertices
            int i1, i2;                             // indices
            double z, xy;                            // coords
            double hAngle1 = -Math.PI / 2 - H_ANGLE / 2;  // start from -126 deg at 2nd row
            double hAngle2 = -Math.PI / 2;                // start from -90 deg at 3rd row

            // the first top vertex (0, 0, r)
            vertices[0] = 0;
            vertices[1] = 0;
            vertices[2] = radius;

            // 10 vertices at 2nd and 3rd rows
            for (int i = 1; i <= 5; ++i)
            {
                i1 = i * 3;         // for 2nd row
                i2 = (i + 5) * 3;   // for 3rd row

                z = radius * Math.Sin(V_ANGLE);              // elevaton
                xy = radius * Math.Cos(V_ANGLE);

                vertices[i1] = xy * Math.Cos(hAngle1);      // x
                vertices[i2] = xy * Math.Cos(hAngle2);
                vertices[i1 + 1] = xy * Math.Sin(hAngle1);  // y
                vertices[i2 + 1] = xy * Math.Sin(hAngle2);
                vertices[i1 + 2] = z;                   // z
                vertices[i2 + 2] = -z;

                // next horizontal angles
                hAngle1 += H_ANGLE;
                hAngle2 += H_ANGLE;
            }

            // the last bottom vertex (0, 0, -r)
            i1 = 11 * 3;
            vertices[i1] = 0;
            vertices[i1 + 1] = 0;
            vertices[i1 + 2] = -radius;

            Vector3[] result = new Vector3[12];

            for (int i = 0; i < 12; i++)
            {
                result[i] = new Vector3((float)vertices[i * 3], (float)vertices[i * 3 + 1], (float)vertices[i * 3 + 2]);
            }

            return result;
        }

        public static RenderableModel Create2(int positionLayoutIndex, int colorLayoutIndex, int normalLayoutIndex)
        {
            Vector3 color = new Vector3(0.2f, 0.8f, 0.2f);
            List<Vertex> vertices = new List<Vertex>();
            //List<Vector3> vertices = new List<Vector3>();
            //List<Vector3> normals = new List<Vector3>();
            List<float> texCoords;
            List<uint> indices = new List<uint>();
            List<uint> lineIndices;

            // compute 12 vertices of icosahedron
            Vector3[] tmpVertices = ComputeIcosahedronVertices(1);

            void addVertices(int i1, int i2, int i3, Vector3 n1, Vector3 n2, Vector3 n3)
            {
                vertices.Add(new Vertex(tmpVertices[i1], color, n1));
                vertices.Add(new Vertex(tmpVertices[i2], color, n2));
                vertices.Add(new Vertex(tmpVertices[i3], color, n3));
                //vertices.Add(tmpVertices[i1]);
                //vertices.Add(tmpVertices[i1]);
                //vertices.Add(tmpVertices[i1]);
            }

            //void addNormals(Vector3 n1, Vector3 n2, Vector3 n3)
            //{
            //    normals.Add(n1);
            //    normals.Add(n2);
            //    normals.Add(n3);
            //}

            void addIndices(uint i1, uint i2, uint i3)
            {
                indices.Add(i1);
                indices.Add(i2);
                indices.Add(i3);
            }

            //const float S_STEP = 1 / 11.0f;         // horizontal texture step
            //const float T_STEP = 1 / 3.0f;          // vertical texture step
            const float S_STEP = 186 / 2048.0f;     // horizontal texture step
            const float T_STEP = 322 / 1024.0f;     // vertical texture step

            //// clear memory of prev arrays
            //std::vector<float>().swap(vertices);
            //std::vector<float>().swap(normals);
            //std::vector<float>().swap(texCoords);
            //std::vector < unsigned int> ().swap(indices);
            //std::vector < unsigned int> ().swap(lineIndices);

            int v0, v1, v2, v3, v4, v11;                // vertex positions
            Vector3 n = new Vector3();                                         // face normal
            //float t0[2], t1[2], t2[2], t3[2], t4[2], t11[2];    // texCoords
            uint index = 0;

            // compute and add 20 tiangles
            v0 = 0;       // 1st vertex
            v11 = 11 * 3; // 12th vertex

            for (int i = 1; i <= 5; ++i)
            {
                // 4 vertices in the 2nd row
                v1 = i;

                if (i < 5)
                    v2 = (i + 1);
                else
                    v2 = 1;

                v3 = (i + 5);
                if ((i + 5) < 10)
                    v4 = (i + 6);
                else
                    v4 = 6;

                //// texture coords
                //t0[0] = (2 * i - 1) * S_STEP; t0[1] = 0;
                //t1[0] = (2 * i - 2) * S_STEP; t1[1] = T_STEP;
                //t2[0] = (2 * i - 0) * S_STEP; t2[1] = T_STEP;
                //t3[0] = (2 * i - 1) * S_STEP; t3[1] = T_STEP * 2;
                //t4[0] = (2 * i + 1) * S_STEP; t4[1] = T_STEP * 2;
                //t11[0] = 2 * i * S_STEP; t11[1] = T_STEP * 3;

                // add a triangle in 1st row
                ComputeFaceNormal(ref tmpVertices[v0], ref tmpVertices[v1], ref tmpVertices[v2], ref n);
                addVertices(v0, v1, v2, n, n, n);
                //addTexCoords(t0, t1, t2);
                addIndices(index, index + 1, index + 2);

                // add 2 triangles in 2nd row
                ComputeFaceNormal(ref tmpVertices[v1], ref tmpVertices[v3], ref tmpVertices[v2], ref n);
                addVertices(v1, v3, v2, n, n, n);
                //addTexCoords(t1, t3, t2);
                addIndices(index + 3, index + 4, index + 5);

                ComputeFaceNormal(ref tmpVertices[v2], ref tmpVertices[v3], ref tmpVertices[v4], ref n);
                addVertices(v2, v3, v4, n, n, n);
                //addTexCoords(t2, t3, t4);
                addIndices(index + 6, index + 7, index + 8);

                // add a triangle in 3rd row
                ComputeFaceNormal(ref tmpVertices[v3], ref tmpVertices[v11], ref tmpVertices[v4], ref n);
                addVertices(v3, v11, v4, n, n, n);
                //addTexCoords(t3, t11, t4);
                addIndices(index + 9, index + 10, index + 11);

                // add 6 edge lines per iteration
                //addLineIndices(index);

                // next index
                index += 12;
            }

            // generate interleaved vertex array as well
            //buildInterleavedVertices();


            return new RenderableModel(vertices.ToArray(), indices.ToArray(), positionLayoutIndex, colorLayoutIndex, normalLayoutIndex);
        }

        static void ComputeFaceNormal(ref Vector3 v1, ref Vector3 v2, ref Vector3 v3, ref Vector3 n)
        {
            const float EPSILON = 0.000001f;

            // default return value (0, 0, 0)
            n[0] = n[1] = n[2] = 0;

            // find 2 edge vectors: v1-v2, v1-v3
            float ex1 = v2[0] - v1[0];
            float ey1 = v2[1] - v1[1];
            float ez1 = v2[2] - v1[2];
            float ex2 = v3[0] - v1[0];
            float ey2 = v3[1] - v1[1];
            float ez2 = v3[2] - v1[2];

            // cross product: e1 x e2
            float nx, ny, nz;
            nx = ey1 * ez2 - ez1 * ey2;
            ny = ez1 * ex2 - ex1 * ez2;
            nz = ex1 * ey2 - ey1 * ex2;

            // normalize only if the length is > 0
            double length = Math.Sqrt(nx * nx + ny * ny + nz * nz);

            if (length > EPSILON)
            {
                // normalize
                float lengthInv = 1.0f / (float)length;
                n[0] = nx * lengthInv;
                n[1] = ny * lengthInv;
                n[2] = nz * lengthInv;
            }
        }

        public static RenderableModel Create(int positionLayoutIndex, int colorLayoutIndex, int normalLayoutIndex)
        {
            Vector3 p1 = new Vector3(1, 1, 1);
            Vector3 p2 = new Vector3(-1, -1, 1);
            Vector3 p3 = new Vector3(-1, 1, -1);
            Vector3 p4 = new Vector3(1, -1, -1);
            Vector3 c1 = new Vector3(1, 1, 0);
            Vector3 c2 = new Vector3(0, 1, 1);
            Vector3 c3 = new Vector3(1, 0, 1);
            Vector3 c4 = new Vector3(1, 1, 1);
            List<Vertex> vertices = new List<Vertex>();
            vertices.Add(new Vertex(p1, c1, p1.Normalized()));
            vertices.Add(new Vertex(p2, c2, p2.Normalized()));
            vertices.Add(new Vertex(p3, c3, p3.Normalized()));
            vertices.Add(new Vertex(p4, c4, p4.Normalized()));
            List<uint> indices = new List<uint>();
            indices.AddRange(new uint[] { 1, 2, 3 });
            return new RenderableModel(vertices.ToArray(), indices.ToArray(), positionLayoutIndex, colorLayoutIndex, normalLayoutIndex);
        }
    }
}
