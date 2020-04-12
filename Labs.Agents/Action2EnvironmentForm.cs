using System.Drawing;
using System.Windows.Forms;

namespace Labs.Agents
{
    public abstract class Action2EnvironmentForm<TSimulation, TEnvironment, TAgent, TState, TInteraction> : SimulationForm
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
            DrawObstacles<TEnvironment, TAgent, TState>(e.Graphics, environment, scale);
            DrawGoals(e.Graphics, environment, scale);
            DrawAgents<TEnvironment, TAgent, TState>(e.Graphics, environment, scale);
        }

        private void DrawGoals(Graphics graphics, TEnvironment environment, int scale)
        {
            foreach (var agent in environment.Agents)
            {
                var agentState = agent.State;

                if (agentState.Goal != Point.Empty)
                {
                    var ax = agentState.Field.X;
                    var ay = agentState.Field.Y;
                    var bx = agentState.Goal.X;
                    var by = agentState.Goal.Y;
                    graphics.DrawLine(Pens.LightBlue, ax * scale, ay * scale, bx * scale, by * scale);
                }
            }
        }
    }
}
