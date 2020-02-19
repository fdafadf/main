using OpenTK;
using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Basics.Physics.Test.UI
{
    class WavefrontModelLoader
    {
        // http://www.martinreddy.net/gfx/3d/OBJ.spec
        //
        // - normal vector
        // nv i j k
        // - texture
        // vt u v w
        // - vertex
        // v x y z w
        // - face
        // f  v1/vt1/vn1   v2/vt2/vn2   v3/vt3/vn3 . . .
        // 
        static Regex VectorPattern = new Regex(@"^\S+\s+(\S+)\s+(\S+)\s+(\S+)\s*$");
        static Regex FacePattern = new Regex(@"^f\s+(\S+)/(\S+)/(\S+)\s+(\S+)/(\S+)/(\S+)\s+(\S+)/(\S+)/(\S+)\s*$");

        public WavefrontModel Load(FileInfo file)
        {
            return this.Load(File.ReadAllLines(file.FullName));
        }

        public WavefrontModel Load(string[] lines)
        {
            WavefrontModel result = new WavefrontModel();
            //List<Vector3> vertices = new List<Vector3>();
            //List<Vector3> normals = new List<Vector3>();

            foreach (string line in lines)
            {
                if (line.StartsWith("v "))
                {
                    result.Vertices.Add(LineToVector(line));
                }
                else if (line.StartsWith("vn "))
                {
                    result.Normals.Add(LineToVector(line));
                }
                else if (line.StartsWith("f "))
                {
                    Match match = FacePattern.Match(line);

                    if (match.Success)
                    {
                        int a1 = int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                        int a2 = int.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                        int a3 = int.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                        int b1 = int.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);
                        int b2 = int.Parse(match.Groups[5].Value, CultureInfo.InvariantCulture);
                        int b3 = int.Parse(match.Groups[6].Value, CultureInfo.InvariantCulture);
                        int c1 = int.Parse(match.Groups[7].Value, CultureInfo.InvariantCulture);
                        int c2 = int.Parse(match.Groups[8].Value, CultureInfo.InvariantCulture);
                        int c3 = int.Parse(match.Groups[9].Value, CultureInfo.InvariantCulture);

                        var face = new WavefrontModel.Face();
                        face.a.v = a1;
                        face.a.t = a2;
                        face.a.n = a3;
                        result.Faces.Add(face);
                        face = new WavefrontModel.Face();
                        face.a.v = b1;
                        face.a.t = b2;
                        face.a.n = b3;
                        result.Faces.Add(face);
                        face = new WavefrontModel.Face();
                        face.a.v = c1;
                        face.a.t = c2;
                        face.a.n = c3;
                        result.Faces.Add(face);
                        //resultIndices.Add((uint)resultVertices.Count);
                        //resultVertices.Add(new Vertex(vertices[a1 - 1], normals[a3 - 1], normals[a3 - 1]));
                        //resultIndices.Add((uint)resultVertices.Count);
                        //resultVertices.Add(new Vertex(vertices[b1 - 1], normals[b3 - 1], normals[b3 - 1]));
                        //resultIndices.Add((uint)resultVertices.Count);
                        //resultVertices.Add(new Vertex(vertices[c1 - 1], normals[c3 - 1], normals[c3 - 1]));

                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (line.StartsWith("vn "))
                {
                }
            }

            return result;
        }

        private Vector3 LineToVector(string line)
        {
            Match match = VectorPattern.Match(line);

            if (match.Success)
            {
                float x = float.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                float y = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                float z = float.Parse(match.Groups[3].Value, CultureInfo.InvariantCulture);
                return new Vector3(x, y, z);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
