using Games.TicTacToe;
using Games.Utilities;
using System.Drawing;
using System.Windows.Forms;

namespace Demos.Forms.TicTacToe.Perceptron
{
    public class PerceptronTicTacToeBoardFieldControl : TicTacToeBoardFieldControl
    {
        public double Output;
        public string Output2;

        public PerceptronTicTacToeBoardFieldControl(BoardCoordinates coordinates) : base(coordinates)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (FieldState == FieldState.Empty)
            {
                e.Graphics.DrawString($"{Output:F6}", SystemFonts.CaptionFont, Brushes.Black, 0, 0);
                e.Graphics.DrawString($"{Output2:F6}", SystemFonts.CaptionFont, Brushes.Black, 0, 20);
            }
        }
    }
}
