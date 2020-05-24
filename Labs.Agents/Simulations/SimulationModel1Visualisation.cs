using Labs.Agents.Forms;
using System.Linq;

namespace Labs.Agents
{
    public class SimulationModel1Visualisation<TPlugin, TAgent> : ISimulationViualisation<SimulationModel1<TPlugin, TAgent>>, ISimulationViualisation
        where TPlugin : SimulationPlugin<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> // IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructible, IGoalAgent
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
    {
        public SimulationModel1<TPlugin, TAgent> Simulation { get; }
        ISimulation ISimulationViualisation.Simulation => Simulation;
        int AnimationInterval;

        public SimulationModel1Visualisation(SimulationModel1<TPlugin, TAgent> simulation, int animationInterval)
        {
            Simulation = simulation;
            AnimationInterval = animationInterval;
        }

        public SimulationResults Show()
        {
            var painter = new Painter(Simulation.Space.InteractiveSpace);
            var environmentPanel = new BufferedPanel();
            var form = new SimulationForm();
            form.EnvironmentControl = environmentPanel;
            form.Simulation = Simulation;
            form.SimulationWorker.Interval = AnimationInterval;
            environmentPanel.Paint += (s, e) => {
                painter.PaintObstacles(e.Graphics);
                painter.PaintAgents(e.Graphics, Simulation.Agents);
                painter.PaintGoals(e.Graphics, Simulation.Agents.Where(agent => agent.Fitness.IsDestroyed == false));
            };
            form.ShowDialog();
            return Simulation.Results;
        }
    }
}
