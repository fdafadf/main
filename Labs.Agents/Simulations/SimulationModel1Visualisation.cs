using Labs.Agents.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class SimulationModel1Visualisation<TDriver, TAgent> : ISimulationViualisation<SimulationModel1<TDriver, TAgent>>, ISimulationViualisation
        where TDriver : SimulationAgentDriver<DestructibleInteractiveSpace<CardinalMovementSpace<TAgent>, TAgent>, TAgent> // IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructible, IGoalAgent
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IDestructibleAgent, IGoalAgent
    {
        public SimulationModel1<TDriver, TAgent> Simulation { get; }
        ISimulation ISimulationViualisation.Simulation => Simulation;

        public SimulationModel1Visualisation(SimulationModel1<TDriver, TAgent> simulation)
        {
            Simulation = simulation;
        }

        public SimulationResults Show()
        {
            var painter = new Painter(Simulation.Space.InteractiveSpace);
            var environmentPanel = new BufferedPanel();
            var form = new SimulationForm();
            form.EnvironmentControl = environmentPanel;
            form.Simulation = Simulation;
            environmentPanel.Paint += (s, e) => {
                painter.PaintObstacles(e.Graphics);
                painter.PaintAgents(e.Graphics, Simulation.Agents);
                painter.PaintGoals(e.Graphics, Simulation.Agents);
            };
            form.ShowDialog();
            return Simulation.Results;
        }
    }
}
