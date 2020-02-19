using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basics.Main.UI.Games.Demos.TicTacToe
{
    public partial class TicTacToeGameSettingsForm : Form
    {
        public TicTacToeGameSettingsForm()
        {
            InitializeComponent();
            noughtPlayerControl.SelectedIndex = 0;
            crossPlayerControl.SelectedIndex = 1;
        }

        public string Nought => noughtPlayerControl.SelectedItem as string;
        public string Cross => crossPlayerControl.SelectedItem as string;
    }
}
