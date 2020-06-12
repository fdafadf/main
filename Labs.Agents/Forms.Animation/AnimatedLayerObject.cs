using System.Drawing;

namespace Labs.Agents
{
    public abstract class AnimatedLayerObject
    {
        public PointF Position;

        public virtual void Paint(Graphics graphics)
        {
        }
    }
}
