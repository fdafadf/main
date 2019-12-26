using Basics.Games;
using Basics.Games.Demos.TicTacToe;
using Basics.Games.TicTacToe;
using System.Drawing;
using System.Windows.Forms;

namespace Basics.AI.NeuralNetworks.Demos.PerceptronTicTacToe
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
