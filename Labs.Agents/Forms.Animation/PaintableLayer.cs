using System.Drawing;

namespace Labs.Agents
{
    public abstract class PaintableLayer : IPaintable
    {
        protected AnimatedLayersControl Parent;

        public PaintableLayer(AnimatedLayersControl parent)
        {
            Parent = parent;
            Parent.Layers.Add(this);
        }

        public virtual void Paint(Graphics graphics)
        {
        }
    }
}
