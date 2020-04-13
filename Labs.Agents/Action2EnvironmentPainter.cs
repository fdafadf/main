using System.Drawing;

namespace Labs.Agents
{
    public class Action2EnvironmentPainter<TEnvironment, TAgent, TState, TInteraction> : EnvironmentPainter<TEnvironment>
        where TEnvironment : Action2Environment<TEnvironment, TAgent, TState, TInteraction>
        where TAgent : IAgent<TEnvironment, TAgent, TState>
        where TState : AgentState2<TEnvironment, TAgent, TState>
        where TInteraction : AgentInteraction<TAgent, Action2, InteractionResult>
    {
        public Action2EnvironmentPainter(TEnvironment environment) : base(environment)
        {
        }

        public override void Paint(Graphics graphics)
        {
            PaintObstacles(graphics);
            PaintAgents(graphics);
            PaintGoals(graphics);
        }

        protected void PaintAgents(Graphics graphics)
        {
            foreach (TAgent agent in Environment.Agents)
            {
                var agentState = agent.State;
                var x = agentState.Field.X;
                var y = agentState.Field.Y;
                Brush brush = agentState.IsDestroyed ? Brushes.Red : Brushes.Black;
                graphics.FillRectangle(brush, x * Scale, y * Scale, Scale, Scale);
            }
        }

        protected void PaintGoals(Graphics graphics)
        {
            foreach (TAgent agent in Environment.Agents)
            {
                TState state = agent.State;

                if (state.Goal != Point.Empty)
                {
                    var ax = state.Field.X;
                    var ay = state.Field.Y;
                    var bx = state.Goal.X;
                    var by = state.Goal.Y;
                    graphics.DrawLine(Pens.LightBlue, ax * Scale, ay * Scale, bx * Scale, by * Scale);
                }
            }
        }
    }
}
