using System.Drawing;
using System.Linq;

namespace Labs.Agents.NeuralNetworks
{
    public class AgentViewLayer : PaintableLayer
    {
        static readonly Pen Pen = new Pen(Color.FromArgb(127, 0, 0, 255));
        static readonly Brush Brush = new SolidBrush(Color.FromArgb(64, 0, 0, 255));

        static AgentViewLayer()
        {
            Pen.ScaleTransform(0.1f, 0.1f);
        }

        ISimulation<NeuralAgent> Simulation;
        int ViewRadius;

        public AgentViewLayer(AnimatedLayersControl parent, ISimulation<NeuralAgent> simulation, int viewRadius) : base(parent)
        {
            Simulation = simulation;
            ViewRadius = viewRadius;
        }

        public override void Paint(Graphics graphics)
        {
            var agent = Simulation.Agents.FirstOrDefault();

            if (agent != null)
            {
                ISpaceField field = agent.Interaction.From ?? agent.Anchor.Field;
                float size = ViewRadius * 2 + 1;
                graphics.FillRectangle(Brush, field.X - ViewRadius, field.Y - ViewRadius, size, size);
            }
        }
    }
}
