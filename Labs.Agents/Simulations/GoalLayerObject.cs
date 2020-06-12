using System.Drawing;

namespace Labs.Agents
{
    public class GoalLayerObject<TAgent> : AnimatedLayerObject
        where TAgent : IAnchoredAgent<TAgent>, IInteractiveAgent<CardinalMovement, InteractionResult>, IGoalAgent
    {
        static readonly Pen Pen = new Pen(Color.GreenYellow);
        static readonly Brush Brush = Brushes.GreenYellow;

        static GoalLayerObject()
        {
            Pen.ScaleTransform(0.1f, 0.1f);
        }

        public readonly AgentLayerObject<TAgent> Agent;

        public GoalLayerObject(AgentLayerObject<TAgent> agent)
        {
            Agent = agent;
        }

        public override void Paint(Graphics graphics)
        {
            graphics.DrawLine(Pen, Agent.Position.X + 0.5f, Agent.Position.Y + 0.5f, Agent.Agent.Goal.Position.X + 0.5f, Agent.Agent.Goal.Position.Y + 0.5f);
            graphics.FillEllipse(Brush, Agent.Agent.Goal.Position.X, Agent.Agent.Goal.Position.Y, 1, 1);
        }
    }
}
