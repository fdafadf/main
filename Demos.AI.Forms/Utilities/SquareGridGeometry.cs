using Games;
using Games.Utilities;
using System;
using System.Drawing;

namespace Demos.Forms.Utilities
{
    public class SquareGridGeometry
    {
        public int PaddingLeft { get; private set; }
        public int PaddingTop { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CellSize { get; private set; }
        public Rectangle[,] Cells { get; private set; }

        public SquareGridGeometry(int rows, int columns, int width, int height, int padding)
        {
            int cellWidth = (width - padding * 2) / columns;
            int cellHeight = (height - padding * 2) / rows;
            this.CellSize = Math.Min(cellWidth, cellHeight);
            this.Width = this.CellSize * columns;
            this.Height = this.CellSize * rows;
            this.PaddingLeft = (width - this.Width) / 2;
            this.PaddingTop = (height - this.Height) / 2;
            this.Cells = new Rectangle[width, height];
            int cellY = this.PaddingTop;

            for (int y = 0; y < height; y++)
            {
                int cellX = this.PaddingLeft;

                for (int x = 0; x < width; x++)
                {
                    this.Cells[x, y] = new Rectangle(cellX, cellY, this.CellSize, this.CellSize);
                    cellX += this.CellSize;
                }

                cellY += this.CellSize;
            }
        }

        public BoardCoordinates? GetCellCoordinates(int x, int y)
        {
            if (x > this.PaddingLeft && y > this.PaddingTop)
            {
                x = x - this.PaddingLeft;
                y = y - this.PaddingTop;

                if (x < this.Width && y < this.Height)
                {
                    return new BoardCoordinates((ushort)(x / this.CellSize), (ushort)(y / this.CellSize));
                }
            }

            return null;
        }
    }
}
