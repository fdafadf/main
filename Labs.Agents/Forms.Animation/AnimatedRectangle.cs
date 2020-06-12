using System;
using System.Drawing;

namespace Labs.Agents
{
    public class AnimatedRectangle : AnimatedShape
    {
        public Brush Color = Brushes.Blue;

        public AnimatedRectangle(float startX, float startY, float targetX, float targetY, int duration) : base(startX, startY, targetX, targetY, duration)
        {
        }

        public override void Paint(Graphics graphics)
        {
            graphics.FillRectangle(Color, Position.X, Position.Y, 1, 1);
        }
    }
}
