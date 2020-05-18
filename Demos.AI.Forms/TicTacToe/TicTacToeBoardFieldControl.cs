using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Demos.Forms.TicTacToe
{
    public class TicTacToeBoardFieldControl : UserControl
    {
        private static Dictionary<FieldState, FieldState> NextState = new Dictionary<FieldState, FieldState>();

        static TicTacToeBoardFieldControl()
        {
            NextState.Add(FieldState.Empty, FieldState.Nought);
            NextState.Add(FieldState.Nought, FieldState.Cross);
            NextState.Add(FieldState.Cross, FieldState.Empty);
        }

        public BoardCoordinates Coordinates { get; }
        private FieldState fieldState;

        public FieldState FieldState
        {
            get
            {
                return fieldState;
            }
            set
            {
                fieldState = value;
                Refresh();
            }
        }

        public TicTacToeBoardFieldControl(BoardCoordinates coordinates)
        {
            Coordinates = coordinates;
            BorderStyle = BorderStyle.FixedSingle;
            Width = 10;
            Height = 10;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Refresh();
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

            if (string.IsNullOrWhiteSpace(Text) == false)
            {
                e.Graphics.DrawString(Text, SystemFonts.CaptionFont, Brushes.Black, 0, 0);
            }
        }

        private void DrawNought(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawEllipse(Pens.Blue, 0, 0, Width - Margin.Right, Height - Margin.Bottom);
        }

        private void DrawCross(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawLine(Pens.Magenta, 0, 0, Width - Margin.Right, Height - Margin.Bottom);
            e.Graphics.DrawLine(Pens.Magenta, Width - Margin.Right, 0, 0, Height - Margin.Bottom);
        }
    }
}
