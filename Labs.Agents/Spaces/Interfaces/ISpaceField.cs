namespace Labs.Agents
{
    public interface ISpaceField : IPoint
    {
        ISpace Space { get; }
        bool IsOutside { get; }
        bool IsEmpty { get; }
        bool IsObstacle { get; }
        bool IsAgent { get; }
    }
}
