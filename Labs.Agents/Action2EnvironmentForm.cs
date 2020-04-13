using System.Collections.Generic;
using System.Windows.Forms;

namespace Labs.Agents
{
    public class Action2EnvironmentForm<TSimulation, TEnvironment, TAgent, TState, TInteraction> : EnvironmentForm
        where TSimulation : Simulation<TEnvironment, TAgent, TState>
        where TEnvironment : Action2Environment<TEnvironment, TAgent, TState, TInteraction> 
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState2<TEnvironment, TAgent, TState>
        where TInteraction : AgentInteraction<TAgent, Action2, InteractionResult>
    {
        protected int iterationNumber;
        protected TSimulation Simulation { get; }

        public Action2EnvironmentForm(TSimulation simulation)
        {
            Simulation = simulation;
        }

        protected override void SimulationStep()
        {
            iterationNumber++;
            Simulation.Step();
            this.InvokeAction(() => { Text = $"Iteration: {iterationNumber}"; });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int scale = 3;
            var environment = Simulation.Environment;
            //EnvironmentPainter.DrawObstacles(e.Graphics, environment, scale);
            //DrawGoals(e.Graphics, environment, scale);
            //EnvironmentPainter.DrawAgents<TEnvironment, TAgent, TState>(e.Graphics, environment.Agents, scale);
        }
    }
}
