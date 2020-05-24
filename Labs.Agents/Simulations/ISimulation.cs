namespace Labs.Agents
{
    public interface ISimulation
    {
        bool Iterate();
        void Complete();
        void Pause();
    }
}
