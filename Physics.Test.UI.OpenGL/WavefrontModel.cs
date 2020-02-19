using OpenTK;
using System.Collections.Generic;

namespace Basics.Physics.Test.UI
{
    class WavefrontModel
    {
        public class Face
        {
            public FaceElement a;
            public FaceElement b;
            public FaceElement c;
        }

        public struct FaceElement
        {
            public int v;
            public int t;
            public int n;
        }

        public List<Vector3> Vertices = new List<Vector3>();
        public List<Vector3> Normals = new List<Vector3>();
        public List<Face> Faces = new List<Face>();
    }
}
