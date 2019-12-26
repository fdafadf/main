using Basics.Games.Forms;
using Basics.Games.TicTacToe;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Basics.Games.Demos.TicTacToe
{
    public class TicTacToeBoardControl : TicTacToeBoardControl<TicTacToeBoardFieldControl>
    {
    }

    public class TicTacToeBoardControl<TFieldControl> : BoardControl<GameState, GameAction> where TFieldControl : TicTacToeBoardFieldControl
    {
        private const int BoardSize = 3;

        private SquaresGridLayoutPanel<TFieldControl> layoutPanel;

        public TicTacToeBoardControl()
        {
            TFieldControl[,] fieldControls = new TFieldControl[BoardSize, BoardSize];

            for (ushort y = 0; y < BoardSize; y++)
            {
                for (ushort x = 0; x < BoardSize; x++)
                {
                    TFieldControl fieldControl = Activator.CreateInstance(typeof(TFieldControl), new BoardCoordinates(x, y)) as TFieldControl;
                    fieldControl.Tag = new GameAction { X = x, Y = y };
                    fieldControl.MouseUp += FieldControl_MouseUp;
                    fieldControls[x, y] = fieldControl;
                }
            }

            this.layoutPanel = new SquaresGridLayoutPanel<TFieldControl>(fieldControls);
            this.SuspendLayout();
            // 
            // ticTacToeBoardControl1
            // 
            this.layoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.Padding = new Padding(0);
            this.layoutPanel.TabIndex = 0;
            this.layoutPanel.Dock = DockStyle.Fill;
            // 
            // TicTacToeBoardControl
            // 
            this.Controls.Add(this.layoutPanel);

            this.Name = "TicTacToeBoardControl";
            this.Text = "TicTacToeBoardControl";
            this.BorderStyle = BorderStyle.FixedSingle;
            this.ResumeLayout(false);
        }

        public override GameState BoardState
        {
            set
            {
                foreach (TicTacToeBoardFieldControl fieldControl in this.Fields)
                {
                    fieldControl.FieldState = value[fieldControl.Coordinates.X, fieldControl.Coordinates.Y];
                }
            }
        }

        private void FieldControl_MouseUp(object sender, MouseEventArgs e)
        {
            TicTacToeBoardFieldControl fieldControl = sender as TicTacToeBoardFieldControl;

            if (fieldControl != null)
            {
                GameAction gameAction = fieldControl.Tag as GameAction;

                if (gameAction != null)
                {
                    this.RaiseFieldSelect(gameAction);
                }
            }
        }

        private TFieldControl this[ushort x, ushort y]
        {
            get
            {
                return this.layoutPanel.Controls[y * 3 + x] as TFieldControl;
            }
        }

        protected IEnumerable<TFieldControl> Fields
        {
            get
            {
                for (ushort y = 0; y < 3; y++)
                {
                    for (ushort x = 0; x < 3; x++)
                    {
                        yield return this[x, y];
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            var geometry = new SquareGridGeometry(BoardSize, BoardSize, this.Width, this.Height, 5);
            var cell = geometry.GetCellCoordinates(e.X, e.Y);

            if (cell.HasValue)
            {
                this.RaiseFieldSelect(cell.Value.ToGameAction());
            }
        }
    }
}
