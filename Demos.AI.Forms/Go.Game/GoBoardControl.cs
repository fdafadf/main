using AI.MonteCarlo;
using Games.Go;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GoMcts = AI.MonteCarlo.MCTreeSearch<Games.Go.GoGame, Games.Go.GameState, Games.Go.FieldCoordinates, Games.Go.Stone>;

namespace Demos.Forms.Go.Game
{
    public partial class GoBoardControl : UserControl
    {
        public ushort BoardSize { get; }
        GoBoardControlGeometry geometry;
        public event EventHandler<Point> OnAction;
        public GoBoardControlFieldFeatures[,] Fields { get; }
        public Pen[] FieldBorders = new Pen[] { Pens.Red, Pens.Blue, Pens.Magenta };

        public GoBoardControl()
        {
            InitializeComponent();
            BoardSize = 9;
            Fields = new GoBoardControlFieldFeatures[BoardSize, BoardSize];

            for (int y = 0; y < BoardSize; y++)
            {
                for (int x = 0; x < BoardSize; x++)
                {
                    Fields[x, y] = new GoBoardControlFieldFeatures();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            geometry.Update(ClientSize, BoardSize);

            base.OnPaint(e);

            int boardSize = 9;
            int fy = geometry.FieldHalfWidth;
            int fx = geometry.FieldWidth * boardSize - geometry.FieldHalfWidth - 1;

            for (int y = 0; y < boardSize; y++)
            {
                e.Graphics.DrawLine(Pens.Black, geometry.FieldHalfWidth, fy, fx, fy);
                fy += geometry.FieldWidth;
            }

            fx = geometry.FieldHalfWidth;
            fy = geometry.FieldWidth * boardSize - geometry.FieldHalfWidth - 1;

            for (int x = 0; x < boardSize; x++)
            {
                e.Graphics.DrawLine(Pens.Black, fx, geometry.FieldHalfWidth, fx, fy);
                fx += geometry.FieldWidth;
            }

            fy = 0;

            for (uint y = 0; y < boardSize; y++)
            {
                fx = 0;

                for (uint x = 0; x < boardSize; x++)
                {
                    PaintField(e, fx, fy, x, y);
                    fx += geometry.FieldWidth;
                }

                fy += geometry.FieldWidth;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void PaintField(PaintEventArgs e, int fx, int fy, uint x, uint y)
        {
            FieldState fieldState = Fields[x, y].State;

            if (fieldState == FieldState.White)
            {
                e.Graphics.FillEllipse(Brushes.White, fx, fy, geometry.FieldWidth, geometry.FieldWidth);
                e.Graphics.DrawEllipse(Pens.Black, fx, fy, geometry.FieldWidth, geometry.FieldWidth);
            }
            else if (fieldState == FieldState.Black)
            {
                e.Graphics.FillEllipse(Brushes.Black, fx, fy, geometry.FieldWidth, geometry.FieldWidth);
            }

            for (int i = 0; i < 4; i++)
            {
                if (Fields[x, y].Borders[i])
                {
                    e.Graphics.DrawRectangle(FieldBorders[i], fx + i, fy + i, geometry.FieldWidth, geometry.FieldWidth);
                }
            }

            Fields[x, y].Label.Paint(e.Graphics, geometry, fx, fy);

            int fyd = 0;

            foreach (var label in Fields[x, y].Labels)
            {
                e.Graphics.DrawString(label.Value, Font, label.Key, fx, fy + fyd);
                fyd += 12;
            }
        }

        Point mouseField = new Point(-1, -1);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //base.OnMouseMove(e);
            Point clientPoint = e.Location;// PointToClient(e.Location);
            geometry.IsPointOnBoard(clientPoint);

            if (mouseField != geometry.PointOnBoard)
            {
                mouseField = geometry.PointOnBoard;
                OnMouseFieldEnter(mouseField.X, mouseField.Y);
            }
            //if (clientPoint.X > 0 && clientPoint.Y > 0)
            //{
            //    int fx = clientPoint.X / geometry.FieldWidth;
            //    int fy = clientPoint.Y / geometry.FieldWidth;
            //
            //    if (mouseField.X != fx || mouseField.Y != fy)
            //    {
            //        OnMouseFieldEnter(fx, fy);
            //    }
            //}
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (geometry.IsPointOnBoard(e.Location))
            {
                OnAction?.Invoke(this, geometry.PointOnBoard);
            }
        }

        protected virtual void OnMouseFieldEnter(int x, int y)
        {
            using (Graphics graphics = CreateGraphics())
            {
                //int fx = geometry.FieldWidth * x;
                //int fy = geometry.FieldWidth * y;
                //graphics.FillRectangle(Brushes.BlanchedAlmond, fx, fy, geometry.FieldWidth, geometry.FieldWidth);
            }
        }

        protected virtual void OnMouseFieldLeave(int x, int y)
        {

        }
    }
}
