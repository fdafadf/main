using System;
using System.Drawing;

namespace Labs.Agents
{
    public class BitmapLayer : PaintableLayer
    {
        public static Bitmap CreateBuffer(bool[,] bitmap, int cellSize)
        {
            int width = bitmap.GetLength(0);
            int height = bitmap.GetLength(1);
            Bitmap buffer = new Bitmap(width * cellSize, height * cellSize);
        
            using (Graphics bufferGraphics = Graphics.FromImage(buffer))
            {
                bufferGraphics.PaintMap(Brushes.DarkGray, width, height, cellSize, (x, y) => bitmap[x, y]);
            }
        
            return buffer;
        }

        Bitmap Buffer;
        bool[,] Bitmap;

        public BitmapLayer(AnimatedLayersControl parent, bool[,] bitmap) : base(parent)
        {
            Bitmap = bitmap;
            Parent.Resize += OnParentResize;
            Buffer = CreateBuffer(Bitmap, CellSize);
        }

        public override void Paint(Graphics graphics)
        {
            if (Buffer != null)
            {
                graphics.DrawImage(Buffer, 0, 0);
            }
        }

        private int CellSize => Math.Min(Parent.Width / Bitmap.GetLength(0), Parent.Height / Bitmap.GetLength(1));

        private void OnParentResize(object sender, EventArgs e)
        {
            int cellSize = CellSize;

            if (Buffer == null || Buffer.Width != Bitmap.GetLength(0) * cellSize)
            {
                Buffer = CreateBuffer(Bitmap, cellSize);
            }
        }
    }
}
