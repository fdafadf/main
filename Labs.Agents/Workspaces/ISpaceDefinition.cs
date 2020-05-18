namespace Labs.Agents
{
    public interface ISpaceDefinition
    {
        string Name { get; }
        SpaceTemplate CreateSpaceTemplate();
    }
}
