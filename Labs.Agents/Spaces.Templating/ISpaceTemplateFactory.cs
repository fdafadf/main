namespace Labs.Agents
{
    public interface ISpaceTemplateFactory
    {
        string Name { get; }
        SpaceTemplate CreateSpaceTemplate();
    }
}
