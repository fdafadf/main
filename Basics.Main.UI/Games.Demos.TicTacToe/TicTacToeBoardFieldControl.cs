using Basics.Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Basics.Games.Demos.TicTacToe
{
    public class TicTacToeBoardFieldControl : UserControl
    {
        private static Dictionary<FieldState, FieldState> NextState = new Dictionary<FieldState, FieldState>();

        static TicTacToeBoardFieldControl()
        {
            TicTacToeBoardFieldControl.NextState.Add(FieldState.Empty, FieldState.Nought);
            TicTacToeBoardFieldControl.NextState.Add(FieldState.Nought, FieldState.Cross);
            TicTacToeBoardFieldControl.NextState.Add(FieldState.Cross, FieldState.Empty);
        }

        public BoardCoordinates Coordinates { get; }
        private FieldState fieldState;

        public FieldState FieldState
        {
            get
            {
                return this.fieldState;
            }
            set
            {
                this.fieldState = value;
                this.Refresh();
            }
        }

        public TicTacToeBoardFieldControl(BoardCoordinates coordinates)
        {
            this.Coordinates = coordinates;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Width = 10;
            this.Height = 10;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (FieldState)
            {
                case FieldState.Cross:
                    DrawCross(e);
                    break;
                case FieldState.Nought:
                    DrawNought(e);
                    break;
            }
        }

        private void DrawNought(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawEllipse(System.Drawing.Pens.Blue, 0, 0, this.Width - this.Margin.Right, this.Height - this.Margin.Bottom);
        }

        private void DrawCross(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawLine(System.Drawing.Pens.Magenta, 0, 0, this.Width - this.Margin.Right, this.Height - this.Margin.Bottom);
            e.Graphics.DrawLine(System.Drawing.Pens.Magenta, this.Width - this.Margin.Right, 0, 0, this.Height - this.Margin.Bottom);
        }
    }
}
