using OpenTK;

namespace Basics.Physics.Test.UI
{
    class Octahedron
    {
        static Vector3 Down { get { return -Vector3.UnitY; } }
        static Vector3 Forward { get { return Vector3.UnitZ; } }
        static Vector3 Left { get { return -Vector3.UnitX; } }
        static Vector3 Back { get { return -Vector3.UnitZ; } }
        static Vector3 Right { get { return Vector3.UnitX; } }
        static Vector3 Up { get { return Vector3.UnitY; } }

        public static void CreateOctahedron(Vector3[] vertices, int[] triangles, int resolution)
        {
            int CreateVertexLine(Vector3 from, Vector3 to, int steps, int vl)
            {
                for (int i = 1; i <= steps; i++)
                {
                    vertices[vl++] = Vector3.Lerp(from, to, (float)i / steps);
                }

                return vl;
            }

            int CreateLowerStrip(int steps, int vTop, int vBottom, int t)
            {
                for (int i = 1; i < steps; i++)
                {
                    triangles[t++] = vBottom;
                    triangles[t++] = vTop - 1;
                    triangles[t++] = vTop;

                    triangles[t++] = vBottom++;
                    triangles[t++] = vTop++;
                    triangles[t++] = vBottom;
                }
                triangles[t++] = vBottom;
                triangles[t++] = vTop - 1;
                triangles[t++] = vTop;
                return t;
            }

            int v = 0, vBottom2 = 0, t1 = 0;

            for (int i = 0; i < 4; i++)
            {
                vertices[v++] = -Vector3.UnitY;
            }
            
            Vector3[] directions = {
                Left,
                Back,
                Right,
                Forward
            };

            for (int i = 1; i <= resolution; i++)
            {
                float progress = (float)i / resolution;
                Vector3 from, to;
                vertices[v++] = from = Vector3.Lerp(Down, Forward, progress);

                for (int d = 0; d < 4; d++)
                {
                    to = Vector3.Lerp(Down, directions[d], progress);
                    t1 = CreateLowerStrip(i, v, vBottom2, t1);
                    v = CreateVertexLine(from, to, i, v);
                    vBottom2 += i > 1 ? (i - 1) : 1;
                }

                vBottom2 = v - 1 - i * 4;
            }

        }

        public static RenderableModel Create(float radius, int positionLayoutIndex, int colorLayoutIndex, int normalLayoutIndex)
        {
            Vector3[] vertices = {
                Down,
                Forward,
                Left,
                Back,
                Right,
                Up
            };
            
            uint[] triangles = {
                0, 1, 2,
                0, 2, 3,
                0, 3, 4,
                0, 4, 1,
            
                5, 2, 1,
                5, 3, 2,
                5, 4, 3,
                5, 1, 4
            };
            //Vector3[] vertices = {
            //    -Vector3.UnitX,
            //    Vector3.UnitY,
            //    Vector3.UnitX,
            //};
            //
            //uint[] triangles =
            //{
            //    1, 2, 3
            //};

            if (radius != 1f)
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    //vertices[i] *= radius;
                }
            }

            Vertex[] vertices2 = new Vertex[vertices.Length];
            Vector3 color = new Vector3(0, 1, 0);

            for (int i = 0; i < vertices2.Length; i++)
            {
                vertices2[i] = new Vertex(vertices[i], color, vertices[i]);
            }
            //Mesh mesh = new Mesh();
            //mesh.name = "Octahedron Sphere";
            //mesh.vertices = vertices;
            //mesh.triangles = triangles;
            return new RenderableModel(vertices2, triangles, positionLayoutIndex, colorLayoutIndex, normalLayoutIndex);
        }
    }
}
