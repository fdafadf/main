using System;
using System.Drawing;

namespace Labs.Agents
{
    public abstract class AnimatedShape : AnimatedLayerObject, IAnimated
    {
        public PointF StartPosition;
        public PointF TargetPosition;
        public int Duration;

        public AnimatedShape(float startX, float startY, float targetX, float targetY, int duration)
        {
            StartPosition = new PointF(startX, startY);
            TargetPosition = new PointF(targetX, targetY);
            Position = StartPosition;
            Duration = duration;
        }

        public virtual bool Update(int currentFrame)
        {
            bool animationCompleted = currentFrame >= Duration;
            Position.X = StartPosition.X + (TargetPosition.X - StartPosition.X) / Duration * currentFrame;
            Position.Y = StartPosition.Y + (TargetPosition.Y - StartPosition.Y) / Duration * currentFrame;
            //Console.WriteLine($"{currentFrame}/{Duration}  {StartPosition.X:f2}  {StartPosition.Y:f2}  {TargetPosition.X:f2}  {TargetPosition.Y:f2}  {Position.X:f2}  {Position.Y:f2}");
            return animationCompleted;
        }
    }
}
