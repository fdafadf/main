using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Basics.Games.Forms;
using Basics.Games.TicTacToe;

namespace Basics.Games.Demos.TicTacToe
{
    public partial class TicTacToeBoardGenericControl : UserControl
    {
        public event GameActionHandler<GameAction> OnAction;

        public TicTacToeBoardGenericControl()
        {
            InitializeComponent();
        }

        public virtual GameState BoardState
        {
            set
            {
            }
        }
    }
}
