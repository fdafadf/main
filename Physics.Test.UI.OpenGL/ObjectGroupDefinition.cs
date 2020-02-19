namespace Basics.Physics.Test.UI
{
    class ObjectGroupDefinition
    {
        public string ShaderCode_Vertex;
        public string ShaderCode_Fragment;
        public string ShaderUniformName_Projection = "projection";
        public string ShaderUniformName_View = "view";
        public string ShaderUniformName_Model = "model";

        public ObjectGroupDefinition(string shaderCode_Vertex, string shaderCode_Fragment)
        {
            ShaderCode_Vertex = shaderCode_Vertex;
            ShaderCode_Fragment = shaderCode_Fragment;
        }
    }
}
