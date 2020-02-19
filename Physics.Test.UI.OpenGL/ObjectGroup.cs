using System;
using System.Collections.Generic;

namespace Basics.Physics.Test.UI
{
    abstract class ObjectGroup : IDisposable
    {
        public ShaderProgram ShaderProgram;
        public int ShaderUniform_Model;
        public int ShaderUniform_Projection;
        public int ShaderUniform_View;
        public List<IRenderable> Renderables = new List<IRenderable>();
        protected List<IDisposable> Disposables = new List<IDisposable>();

        public ObjectGroup(ObjectGroupDefinition definition)
        {
            ShaderProgram = ShaderProgram.Load(definition.ShaderCode_Vertex, definition.ShaderCode_Fragment, definition.ShaderUniformName_Projection, definition.ShaderUniformName_View);
            ShaderUniform_Model = ShaderProgram.UniformLocations[definition.ShaderUniformName_Model];
            ShaderUniform_Projection = ShaderProgram.UniformLocations[definition.ShaderUniformName_Projection];
            ShaderUniform_View = ShaderProgram.UniformLocations[definition.ShaderUniformName_View];
        }

        public void Render(ICamera camera)
        {
            ShaderProgram.Bind(camera);

            foreach (IRenderable renderable in Renderables)
            {
                renderable.Render(ShaderUniform_Model);
            }
        }

        public void Dispose()
        {
            ShaderProgram.Dispose();

            foreach (IDisposable disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
