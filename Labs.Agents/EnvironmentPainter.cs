using System.Drawing;

namespace Labs.Agents
{
    public abstract class EnvironmentPainter<TEnvironment> : IPainter
        where TEnvironment : IEnvironment
    {
        protected TEnvironment Environment { get; }
        protected readonly int Scale = 3;

        Bitmap obstaclesBuffer;

        public EnvironmentPainter(TEnvironment environment)
        {
            Environment = environment;
            obstaclesBuffer = new Bitmap(Environment.Width * Scale, Environment.Height * Scale);
            int width = Environment.Width;
            int height = Environment.Height;

            using (Graphics bufferGraphics = Graphics.FromImage(obstaclesBuffer))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var field = Environment[x, y];

                        if (field.IsObstacle)
                        {
                            bufferGraphics.FillRectangle(Brushes.DarkGray, x * Scale, y * Scale, Scale, Scale);
                        }
                    }
                }
            }
        }

        public abstract void Paint(Graphics graphics);

        protected void PaintObstacles(Graphics graphics)
        {
            graphics.DrawImage(obstaclesBuffer, 0, 0);
        }
    }
}
