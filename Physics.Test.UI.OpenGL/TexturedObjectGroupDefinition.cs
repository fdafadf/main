namespace Basics.Physics.Test.UI
{
    class TexturedObjectGroupDefinition : ObjectGroupDefinition
    {
        public string ShaderLayoutName_Position = "position";
        public string ShaderLayoutName_Texture = "textureCoordinate";

        public TexturedObjectGroupDefinition(string shaderCode_Vertex, string shaderCode_Fragment) : base(shaderCode_Vertex, shaderCode_Fragment)
        {
        }
    }
}
