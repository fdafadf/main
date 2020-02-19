using Basics.Games.Forms;
using Basics.Games.TicTacToe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Basics.Games.Demos.TicTacToe
{
    public class TicTacToeBoardGenericControl2 : UserControl //where TFieldControl : TicTacToeBoardFieldControl
    {
        private const int BoardSize = 3;

        public event GameActionHandler<GameAction> OnAction;
        TicTacToeBoardFieldControlCollection<TicTacToeBoardFieldControl> Fields;
        private SquaresGridLayoutPanel<TicTacToeBoardFieldControl> layoutPanel;

        public TicTacToeBoardGenericControl2()
        {
            Fields = InitializeFieldControls();

            //for (ushort y = 0; y < BoardSize; y++)
            //{
            //    for (ushort x = 0; x < BoardSize; x++)
            //    {
            //        //TicTacToeBoardFieldControl fieldControl = Activator.CreateInstance(typeof(TicTacToeBoardFieldControl), new BoardCoordinates(x, y)) as TicTacToeBoardFieldControl;
            //        TicTacToeBoardFieldControl fieldControl = fieldControls[x, y];
            //        fieldControl.Tag = new GameAction { X = x, Y = y };
            //        fieldControl.MouseUp += FieldControl_MouseUp;
            //        //fieldControls[x, y] = fieldControl;
            //    }
            //}

            this.layoutPanel = new SquaresGridLayoutPanel<TicTacToeBoardFieldControl>(Fields.Controls);
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

        public virtual GameState BoardState
        {
            set
            {
                foreach (TicTacToeBoardFieldControl fieldControl in Fields)
                {
                    fieldControl.FieldState = value[fieldControl.Coordinates.X, fieldControl.Coordinates.Y];
                }
            }
        }

        protected virtual TicTacToeBoardFieldControlCollection<TicTacToeBoardFieldControl> InitializeFieldControls()
        {
            return CreateFieldControls<TicTacToeBoardFieldControl>();
        }

        protected virtual TicTacToeBoardFieldControlCollection<TFieldControl> CreateFieldControls<TFieldControl>() where TFieldControl : TicTacToeBoardFieldControl
        {
            TFieldControl[,] fieldControls = new TFieldControl[BoardSize, BoardSize];

            for (ushort y = 0; y < BoardSize; y++)
            {
                for (ushort x = 0; x < BoardSize; x++)
                {
                    fieldControls[x, y] = CreateFieldControl<TFieldControl>(x, y);
                }
            }

            return new TicTacToeBoardFieldControlCollection<TFieldControl>(fieldControls);
        }

        protected TFieldControl CreateFieldControl<TFieldControl>(ushort x, ushort y) where TFieldControl : TicTacToeBoardFieldControl
        {
            return Activator.CreateInstance(typeof(TFieldControl), new BoardCoordinates(x, y)) as TFieldControl;
        }

        private void FieldControl_MouseUp(object sender, MouseEventArgs e)
        {
            TicTacToeBoardFieldControl fieldControl = sender as TicTacToeBoardFieldControl;

            if (fieldControl != null)
            {
                GameAction gameAction = fieldControl.Tag as GameAction;

                if (gameAction != null)
                {
                    this.OnAction?.Invoke(gameAction);
                }
            }
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            var geometry = new SquareGridGeometry(BoardSize, BoardSize, this.Width, this.Height, 5);
            var cell = geometry.GetCellCoordinates(e.X, e.Y);

            if (cell.HasValue)
            {
                this.OnAction?.Invoke(new GameAction(cell.Value));
            }
        }
    }

    //public class TicTacToeBoardFieldControlCollection : IEnumerable<TicTacToeBoardFieldControl>
    //{
    //    public readonly TicTacToeBoardFieldControl[,] Controls;
    //
    //    public TicTacToeBoardFieldControlCollection(TicTacToeBoardFieldControl[,] controls)
    //    {
    //        Controls = controls;
    //    }
    //
    //    public IEnumerator<TicTacToeBoardFieldControl> GetEnumerator()
    //    {
    //        for (ushort y = 0; y < 3; y++)
    //        {
    //            for (ushort x = 0; x < 3; x++)
    //            {
    //                yield return Controls[x, y];
    //            }
    //        }
    //    }
    //
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return Controls.GetEnumerator();
    //    }
    //}

    public class TicTacToeBoardFieldControlCollection<TFieldControl> : IEnumerable<TFieldControl> where TFieldControl : TicTacToeBoardFieldControl
    {
        public readonly TFieldControl[,] Controls;

        public TicTacToeBoardFieldControlCollection(TFieldControl[,] controls)
        {
            Controls = controls;
        }

        private TFieldControl this[ushort x, ushort y]
        {
            get
            {
                return Controls[x, y];
                //return this.layoutPanel.Controls[y * 3 + x] as TFieldControl;
            }
        }

        public TicTacToeBoardFieldControlCollection<T> Cast<T>() where T : TicTacToeBoardFieldControl
        {
            return new TicTacToeBoardFieldControlCollection<T>(Controls as T[,]);
        }

        public IEnumerator<TFieldControl> GetEnumerator()
        {
            for (ushort y = 0; y < 3; y++)
            {
                for (ushort x = 0; x < 3; x++)
                {
                    yield return this[x, y];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
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
}
