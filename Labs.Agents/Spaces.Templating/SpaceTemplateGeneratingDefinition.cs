namespace Labs.Agents
{
    public class SpaceTemplateGeneratingDefinition : ISpaceTemplateFactory
    {
        public string Name => GeneratorProperties.Name;
        public SpaceTemplateGeneratorProperties GeneratorProperties { get; private set; }

        public SpaceTemplateGeneratingDefinition(SpaceTemplateGeneratorProperties generatorProperties) 
        {
            GeneratorProperties = generatorProperties;
        }

        public SpaceTemplate CreateSpaceTemplate()
        {
            return SpaceTemplateGenerator.Generate(GeneratorProperties);
        }
    }
}
