using Labs.Agents.Forms;
using System.Collections.Generic;

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

    public interface ISimulation<TAgent>
    {
        IEnumerable<TAgent> Agents { get; }
    }

    public interface IShufflable
    {
        void Shuffle();
    }
}
