using System.Windows.Forms;

namespace Labs.Agents
{
    public interface ISimulationViualisation<TSimulation> where TSimulation : ISimulation
    {
        TSimulation Simulation { get; }
    }

    public interface ISimulationViualisation
    {
        ISimulation Simulation { get; }
        SimulationResults Show();
    }
}
