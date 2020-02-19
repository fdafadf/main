using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.Main.UI.Games.Demos.TicTacToe
{
    public partial class UserControl1 : Basics.Games.Forms.BoardControl1<Basics.Games.TicTacToe.GameState, Basics.Games.TicTacToe.GameAction>
    {
        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
