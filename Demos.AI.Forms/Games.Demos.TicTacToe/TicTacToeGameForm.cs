using Basics.AI.NeuralNetworks;
using Basics.AI.NeuralNetworks.Demos.NeuralTicTacToe;
using Basics.Games.TicTacToe;
using Basics.Main.UI;
using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Basics.Games.Demos.TicTacToe
{
    public partial class TicTacToeGameForm : Form
    {
        GameState currentState = new GameState();
        readonly Func<GameState, GameAction> NoughtAI;
        readonly Func<GameState, GameAction> CrossAI;

        public TicTacToeGameForm(Func<GameState, GameAction> noughtAI, Func<GameState, GameAction> crossAI) : this()
        {
            NoughtAI = noughtAI;
            CrossAI = crossAI;
        }

        public TicTacToeGameForm()
        {
            InitializeComponent();
        }

        private void ticTacToeBoardControl1_OnAction(GameAction action)
        {
            if (CurrentAI == null)
            {
                if (currentState.IsFinal)
                {
                    MessageBox.Show("Game is already finished");
                }
                else
                {
                    GameState nextState = TicTacToeGame.Instance.Play(currentState, action);

                    if (nextState == null)
                    {
                        MessageBox.Show("It action is not allowed");
                    }
                    else
                    {
                        this.boardControl.BoardState = nextState;
                        currentState = nextState;
                        MovePlayed();
                    }
                }
            }
            else
            {
                MessageBox.Show("It is not your turn.");
            }
        }

        private void TicTacToeForm_Load(object sender, System.EventArgs e)
        {
            TryToPlayAsAI();
        }

        private void MovePlayed()
        {
            if (currentState.IsFinal)
            {
                Player winner = currentState.GetWinner();

                if (winner == null)
                {
                    MessageBox.Show($"Game has been finished with draw");
                }
                else
                {
                    MessageBox.Show($"Game winner is {winner.FieldState}");
                }
            }
            else
            {
                TryToPlayAsAI();
            }
        }

        private Func<GameState, GameAction> CurrentAI
        {
            get
            {
                if (currentState.CurrentPlayer.FieldState == FieldState.Cross)
                {
                    return CrossAI;
                }
                else
                {
                    return NoughtAI;
                }
            }
        }

        private void TryToPlayAsAI()
        {
            Func<GameState, GameAction> currentAI = CurrentAI;

            if (currentAI != null)
            {
                if (currentState.IsFinal == false)
                {
                    GameAction gameAction = currentAI(currentState);
                    GameState nextState = TicTacToeGame.Instance.Play(currentState, gameAction);

                    if (nextState == null)
                    {
                        MessageBox.Show($"The {currentState.CurrentPlayer.FieldState} lost by illegal move.");
                    }
                    else
                    {
                        this.boardControl.BoardState = nextState;
                        currentState = nextState;
                        MovePlayed();
                    }
                }
            }
        }
    }
}
