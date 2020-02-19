using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using WindowsFormsApp1;

namespace Basics.Physics.Test.UI
{
    class VertexObjectGroup : ObjectGroup
    {
        public ILight Light;
        public int ShaderUniform_Light;
        public int ShaderLayout_Position;
        public int ShaderLayout_Normal;
        public int ShaderLayout_Color;

        public VertexObjectGroup(VertexObjectGroupDefinition definition) : base(definition)
        {
            ShaderUniform_Light = ShaderProgram.UniformLocations[definition.ShaderUniformName_Light];
            ShaderLayout_Position = GL.GetAttribLocation(ShaderProgram.Handle, definition.ShaderLayoutName_Position);
            ShaderLayout_Normal = GL.GetAttribLocation(ShaderProgram.Handle, definition.ShaderLayoutName_Normal);
            ShaderLayout_Color = GL.GetAttribLocation(ShaderProgram.Handle, definition.ShaderLayoutName_Color);
            Light = new Light(ShaderProgram, ShaderUniform_Light);
            Light.Position = new Vector3(30, 200, 400);
        }

        public void Add(RenderableModel model, IEnumerable<Sphere3d> spheres)
        {
            foreach (Sphere3d sphere in spheres)
            {
                Add(model, sphere);
            }
        }

        public void Add(RenderableModel model, Sphere3d sphere)
        {
            SceneSphere sceneSphere = new SceneSphere(model, sphere);
            Renderables.Add(sceneSphere);
        }

        public RenderableModel Add(WavefrontModel model)
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

            RenderableModel renderableModel = new RenderableModel(vertices.ToArray(), indices.ToArray(), ShaderLayout_Position, ShaderLayout_Color, ShaderLayout_Normal);
            this.Disposables.Add(renderableModel);
            return renderableModel;
        }
    }
}
