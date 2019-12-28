using System.Windows.Forms;

namespace Demos.Forms.TicTacToe.Game
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
