using System;
using System.Windows.Forms;

namespace Basics.Games.Forms
{
    public class SquaresGridLayoutPanel<TFieldControl> : TableLayoutPanel where TFieldControl : Control
    {
        public SquaresGridLayoutPanel(TFieldControl[,] fieldControls)
        {
            int width = fieldControls.GetLength(0);
            int height = fieldControls.GetLength(1);
            this.ColumnCount = width + 2;
            this.RowCount = height + 2;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));

            for (int x = 1; x <= width; x++)
            {
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1));
            }

            this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10));
            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));

            for (int y = 1; y <= width; y++)
            {
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 1));
            }

            this.RowStyles.Add(new RowStyle(SizeType.Absolute, 10));

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Control fieldControl = fieldControls[x, y];
                    fieldControl.Dock = DockStyle.Fill;
                    this.Controls.Add(fieldControl, x + 1, y + 1);
                }
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            var geometry = new SquareGridGeometry(this.ColumnCount, this.RowCount, this.Width, this.Height, 0);
            //this.ColumnStyles[0].Width = geometry.PaddingLeft;
            this.Padding = new Padding(geometry.PaddingLeft, geometry.PaddingTop, geometry.PaddingLeft, geometry.PaddingTop);
        }
    }
}
