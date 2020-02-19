namespace Basics.Physics.Test.UI
{
    class VertexObjectGroupDefinition : ObjectGroupDefinition
    {
        public string ShaderUniformName_Light = "lightPos";
        public string ShaderLayoutName_Position = "aPosition";
        public string ShaderLayoutName_Normal = "vertexNormal";
        public string ShaderLayoutName_Color = "vertexColor";

        public VertexObjectGroupDefinition(string shaderCode_Vertex, string shaderCode_Fragment) : base(shaderCode_Vertex, shaderCode_Fragment)
        {
        }
    }          
}
