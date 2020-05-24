namespace Labs.Agents
{
    public class SpaceTemplateGeneratorDefinition : ISpaceTemplateFactory
    {
        public string Name => GeneratorProperties.Name;
        public SpaceTemplateGeneratorProperties GeneratorProperties { get; private set; }

        public SpaceTemplateGeneratorDefinition(SpaceTemplateGeneratorProperties generatorProperties) 
        {
            GeneratorProperties = generatorProperties;
        }

        public SpaceTemplate CreateSpaceTemplate()
        {
            return SpaceTemplateGenerator.Generate(GeneratorProperties);
        }
    }
}
