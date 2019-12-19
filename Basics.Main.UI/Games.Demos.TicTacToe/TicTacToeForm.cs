using Basics.Games.TicTacToe;
using System.Windows.Forms;

namespace Basics.Games.Demos.TicTacToe
{
    public partial class TicTacToeForm : Form
    {
        TicTacToeGame game = new TicTacToeGame();
        GameState currentState = new GameState();

        public TicTacToeForm()
        {
            InitializeComponent();
        }

        private void ticTacToeBoardControl1_OnAction(GameAction action)
        {
            GameState nextState = game.Play(currentState, action);

            if (nextState == null)
            {
                MessageBox.Show("Not allowed");
            }
            else
            {
                this.ticTacToeBoardControl1.BoardState = nextState;
                currentState = nextState;

                if (currentState.IsFinal)
                {
                    Player winner = currentState.GetWinner();

                    if (winner == null)
                    {
                        MessageBox.Show($"Draw");
                    }
                    else
                    {
                        MessageBox.Show($"{winner.FieldState} won");
                    }
                }
            }
        }
    }
}
