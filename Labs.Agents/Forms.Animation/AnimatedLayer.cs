using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labs.Agents
{
    public class AnimatedLayer : PaintableLayer, IAnimated
    {
        public List<AnimatedLayerObject> Objects = new List<AnimatedLayerObject>();
        int Width;
        int Height;

        public AnimatedLayer(AnimatedLayersControl parent, int width, int height) : base(parent)
        {
            Width = width;
            Height = height;
        }

        public override void Paint(Graphics graphics)
        {
            graphics.ResetTransform();
            int scale = Math.Min(Parent.Width / Width, Parent.Height / Height);
            graphics.ScaleTransform(scale, scale);

            foreach (AnimatedLayerObject obj in Objects)
            {
                obj.Paint(graphics);
            }
        }

        public bool Update(int currentFrame)
        {
            bool animationCompleted = true;

            foreach (var obj in Objects.OfType<IAnimated>())
            {
                animationCompleted &= obj.Update(currentFrame);
            }

            return animationCompleted;
        }
    }
}
