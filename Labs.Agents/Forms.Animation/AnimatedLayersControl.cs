using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Labs.Agents
{
    public class AnimatedLayersControl : BufferedPanel
    {
        public List<PaintableLayer> Layers = new List<PaintableLayer>();
        public Timer AnimationTimer;
        public int CurrentFrame;
        EventWaitHandle waitHandle = new EventWaitHandle(true, EventResetMode.ManualReset);

        public AnimatedLayersControl()
        {
            AnimationTimer = new Timer();
            AnimationTimer.Interval = 20;
            AnimationTimer.Tick += AnimationTick;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var layer in Layers)
            {
                layer.Paint(e.Graphics);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            bool animationCompleted = Update(++CurrentFrame);
            Refresh();

            if (animationCompleted)
            {
                AnimationTimer.Stop();
                waitHandle.Set();
            }
        }

        private bool Update(int currentFrame)
        {
            bool animationCompleted = true;

            foreach (var layer in Layers.OfType<IAnimated>())
            {
                animationCompleted &= layer.Update(currentFrame);
            }

            return animationCompleted;
        }

        public void Start()
        {
            waitHandle.WaitOne();
            waitHandle.Reset();
            Update(CurrentFrame = 0);
            FindForm().InvokeAction(AnimationTimer.Start);
        }
    }
}
