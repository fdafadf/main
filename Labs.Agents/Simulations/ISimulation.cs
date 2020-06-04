using Labs.Agents.Forms;

namespace Labs.Agents
{
    public interface ISimulation
    {
        int Iteration { get; }
        int TotalReachedGoals { get; }
        SimulationResults Results { get; }
        bool Iterate();
        void Initialise();
        void Complete();
        void Pause();
    }
}
